using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interface;
using Newtonsoft.Json;

namespace A.Core.RESTClient.Core
{
    public abstract class BaseService : IService
    {
        public Lazy<IActionContext> ActionContext { get; set; }

        public bool BeginTransaction()
        {
            return false;
        }

        public void CommitTransaction()
        {

        }

        public void DisposeTransaction()
        {

        }

        public void RollbackTransaction()
        {

        }

        public T Clone<T>(T source)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            var serialized = JsonConvert.SerializeObject(source, settings);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
