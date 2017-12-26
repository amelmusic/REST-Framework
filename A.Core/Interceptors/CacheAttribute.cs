using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    public class CacheAttribute : BaseInterceptorAttribute
    {
        public ExpirationType ExpirationType { get; set; }
        public string ExpirationPattern { get; set; }
        public string Prefix { get; set; }
        public bool IsUserContextAware { get; set; }
        public bool Invalidate { get; set; }

        public CacheAttribute(ExpirationType expirationType, string expirationPattern, string prefix = null, bool isUserContextAware = false)
        {
            ExpirationType = expirationType;
            ExpirationPattern = expirationPattern;
            Prefix = prefix;
            IsUserContextAware = isUserContextAware;
        }

        public CacheAttribute(bool invalidate, string prefix = null, bool isUserContextAware = false)
        {
            Invalidate = invalidate;
            Prefix = prefix;
            IsUserContextAware = isUserContextAware;
        }
    }
}
