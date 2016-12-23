using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using PostSharp.Extensibility;
using System.Collections;
using System.IO;
using PostSharp;
using Microsoft.Practices.Unity;
using Foundatio.Caching;

namespace A.Core.Interceptors
{
    [Serializable]
    public class CacheInterceptorAttribute : OnMethodBoundaryAspect
    {
        private string _methodName;
        private string _className;
        private string _prefix;
        private ExpirationType _expirationType;
        private string _expirationPattern;
        private bool _isUserContextAware;
        [NonSerialized]
        private MethodInfo _getFromCacheMethodInfo;
        [NonSerialized]
        static JsonSerializerSettings jsonSerializerSettings;


        /// <summary>
        /// Initializing caching mechanism
        /// </summary>
        /// <param name="expirationType"></param>
        /// <param name="expirationPattern">Format shoud be in hh:mm:ss - for example 01:15:00</param>
        /// <param name="isUserContextAware">if [true] UserId will be added to cache key</param>
        /// <param name="prefix">if [null] fully qualified class name will be used for prefix</param>
        public CacheInterceptorAttribute(ExpirationType expirationType, string expirationPattern, string prefix = null, bool isUserContextAware = false)
        {
            _expirationType = expirationType;
            _expirationPattern = expirationPattern;
            _isUserContextAware = isUserContextAware;
            _prefix = prefix;
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _methodName = method.Name;
            _className = method.DeclaringType.FullName;
            base.CompileTimeInitialize(method, aspectInfo);
        }

        public override void RuntimeInitialize(System.Reflection.MethodBase method)
        {
            base.RuntimeInitialize(method);
            jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            var methodType = ((MethodInfo)method).ReturnType;
            _getFromCacheMethodInfo = typeof(ICacheClient).GetMethod("GetAsync")
                                         .MakeGenericMethod(new Type[] { methodType });
            
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            if (!typeof(A.Core.Interface.IService).IsAssignableFrom(method.DeclaringType))
            {
                Message.Write(MessageLocation.Of((MemberInfo)method), SeverityType.Error, "998", string.Format("Declaring class {0} has to implement IService interface", _className));
            }
            var methodInfo = method as MethodInfo;
            if (methodInfo != null)
            {
                var returnType = methodInfo.ReturnType;
                if (IsDisallowedCacheReturnType(returnType))
                {
                    Message.Write(MessageLocation.Of(method), SeverityType.Error, "998", string.Format("Methods with return type {0} can't be cached!", returnType.Name));
                    return false;
                }
            }
            return true;
        }

        private static readonly List<Type> DisallowedTypes = new List<Type>
                          {
                                  typeof (Stream),
                                  typeof (IEnumerable),
                                  typeof (IQueryable)
                          };
        private static bool IsDisallowedCacheReturnType(Type returnType)
        {
            return DisallowedTypes.Any(t => t.IsAssignableFrom(returnType));
        }

        public override async void OnEntry(MethodExecutionArgs args)
        {
            A.Core.Interface.IService instance = (A.Core.Interface.IService)args.Instance;
            var cacheClient = instance.ActionContext.Value.CurrentContainer.Resolve<ICacheClient>();

            var key = BuildCacheKey(args.Arguments, instance);
            // For non-public methods, you'll need to specify binding flags too
            
            var cacheResult = await (dynamic) _getFromCacheMethodInfo.Invoke(cacheClient, new string[] { key });

            if (cacheResult.HasValue)
            {
                args.ReturnValue = cacheResult.Value;
                args.FlowBehavior = FlowBehavior.Return;
            }
            else
            {
                args.MethodExecutionTag = key;
            }
        }

        public override async void OnSuccess(MethodExecutionArgs args)
        {
            A.Core.Interface.IService instance = (A.Core.Interface.IService)args.Instance;
            var cacheClient = instance.ActionContext.Value.CurrentContainer.Resolve<ICacheClient>();
            string key = (string)args.MethodExecutionTag;
            if(_expirationType == ExpirationType.Default)
            {
                await cacheClient.AddAsync(key, args.ReturnValue);
            }
            else if(_expirationType == ExpirationType.ExpiresIn)
            {
                TimeSpan span = TimeSpan.Parse(_expirationPattern);
                await cacheClient.AddAsync(key, args.ReturnValue, span);
            }
        }

        private string BuildCacheKey(Arguments arguments, A.Core.Interface.IService instance)
        {
            var sb = new StringBuilder();
            if(!string.IsNullOrWhiteSpace(_prefix))
            {
                sb.Append(_prefix);
            }
            else
            {
                sb.Append(_className);
                sb.Append(".");
                sb.Append(_methodName);
            }
            if (_isUserContextAware)
            {
                var userId = instance.ActionContext.Value.Data["UserId"].ToString();
                sb.Append(".u." + userId);
            }
            sb.Append(":");
            string output = JsonConvert.SerializeObject(arguments, jsonSerializerSettings);

            sb.Append(output);
            return sb.ToString();
        }
    }

    public enum ExpirationType
    {
        Default = 0,
        ExpiresIn = 1
    }
}
