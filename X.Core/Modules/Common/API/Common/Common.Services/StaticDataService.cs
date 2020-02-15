using Common.Model.SearchObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Validation;

namespace Common.Services
{
    partial class StaticDataService
    {
        public virtual async Task<dynamic> Content(StaticDataSearchObject search)
        {
            var first = await GetFirstOrDefaultForSearchObjectAsync(search);
            if (first == null)
            {
                throw new UserException($"Nothing found for given parameters");
            }

            //TODO: Implement merging with parent here

            var content = first.Content;

            dynamic json = JsonConvert.DeserializeObject(content);
            return json;
        }
    }
}
