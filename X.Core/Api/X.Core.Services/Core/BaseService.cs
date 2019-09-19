using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace X.Core.Services.Core
{
    public abstract class BaseService
    {
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
