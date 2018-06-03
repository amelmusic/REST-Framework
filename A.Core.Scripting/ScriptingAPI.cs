using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace A.Core.Scripting
{
    // ReSharper disable once InconsistentNaming
    public class ScriptingAPI : IScriptingAPI
    {
        static readonly ConcurrentDictionary<int, object> CompiledScripts = new ConcurrentDictionary<int, object>();

        public ScriptingAPI()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual async Task<T> RunAsync<T,TGlobal>(Type caller, string script, GlobalContainer<TGlobal> scriptData, IList<Assembly> additionalAssemblies = null, params string[] imports)
        {
            if (additionalAssemblies == null)
            {
                additionalAssemblies = new List<Assembly>();
            }
            var compiledScript = CompiledScripts.GetOrAdd(("RETURN_VAL" + caller.FullName + script).GetHashCode(), i =>
            {
                ScriptOptions scriptOptions = ScriptOptions.Default
                    .AddImports(imports)
                    .AddImports("System.Dynamic")
                    .AddImports("System.Linq")
                    .AddImports("A.Core.Scripting")
                    .AddReferences(
                        Assembly.GetAssembly(typeof(IScriptingAPI)), // System.Code
                        Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)), // System.Code
                        Assembly.GetAssembly(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo)), // Microsoft.CSharp
                        Assembly.GetAssembly(typeof(System.Dynamic.ExpandoObject)) // System.Dynamic
                    ).AddReferences(caller.Assembly)
                    .AddReferences(additionalAssemblies);

                var scriptWithGlobal = CSharpScript.Create<T>(script, globalsType: typeof(GlobalContainer<TGlobal>), options: scriptOptions);

                var dlgWithGlobal = scriptWithGlobal.CreateDelegate();
                return dlgWithGlobal;
            });
            
            var castedCompiledScript = (ScriptRunner<T>) compiledScript;
            var res = await castedCompiledScript(scriptData);

            return res;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual async Task RunAsync<TGlobal>(Type caller, string script, GlobalContainer<TGlobal> scriptData, IList<Assembly> additionalAssemblies = null, params string[] imports)
        {
            if (additionalAssemblies == null)
            {
                additionalAssemblies = new List<Assembly>();
            }
            var compiledScript = CompiledScripts.GetOrAdd(("NORETURN_VAL" + caller.FullName + script).GetHashCode(), i =>
            {
                ScriptOptions scriptOptions = ScriptOptions.Default
                    .AddImports(imports)
                    .AddImports("System.Dynamic")
                    .AddImports("System.Linq")
                    .AddImports("A.Core.Scripting")
                    .AddReferences(
                        Assembly.GetAssembly(typeof(IScriptingAPI)), // System.Code
                        Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)), // System.Code
                        Assembly.GetAssembly(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo)), // Microsoft.CSharp
                        Assembly.GetAssembly(typeof(System.Dynamic.ExpandoObject)) // System.Dynamic
                    ).AddReferences(caller.Assembly)
                    .AddReferences(additionalAssemblies);

                var scriptWithGlobal = CSharpScript.Create(script, globalsType: typeof(GlobalContainer<TGlobal>), options: scriptOptions);

                var dlgWithGlobal = scriptWithGlobal.CreateDelegate();
                return dlgWithGlobal;
            });

            var castedCompiledScript = (ScriptRunner<object>)compiledScript;
            var res = await castedCompiledScript(scriptData);
        }

    }

    public class GlobalContainer<T>
    {
        /// <summary>
        /// We feed external data inside this variable
        /// </summary>
        public T Data { get; set; }
    }
}
