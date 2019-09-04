using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace A.Core.Extensions
{
    public static class ObjectExtension
    {
        public static JsonSerializer JsonSerializerSettings { get; set; }

        static ObjectExtension()
        {
            JsonSerializerSettings = new JsonSerializer();
            JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            
            JToken token = metaToken as JToken;
            if (token == null)
            {
                try
                {
                    return ToKeyValue(JObject.FromObject(metaToken, JsonSerializerSettings));
                }
                catch (System.ArgumentException ex)
                {
                    var dict = new Dictionary<string, string>();
                    dict.Add("id", metaToken.ToString());
                    return dict;
                }
                
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                            .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            string value = null;
            if (jValue?.Type == JTokenType.Date && jValue != null)
            {
                var date = jValue.ToString("o");
                var parsed = DateTime.Parse(date);
                if (parsed.Kind == DateTimeKind.Local)
                {
                    value = parsed.ToUniversalTime().ToString("u");
                }
                else if (parsed.Kind == DateTimeKind.Unspecified)
                {
                    value = parsed.ToUniversalTime().ToString("u");
                }
                else
                {
                    value = parsed.ToString("u");
                }
            }
            else
            {
                value = jValue?.ToString(CultureInfo.InvariantCulture);
            }
            //var value = jValue?.Type == JTokenType.Date ?
            //    jValue?.ToString("u", CultureInfo.InvariantCulture) :
            //    jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }

        public static async Task<string> ToQueryString(this object metaToken)
        {
            var keyValueContent = metaToken.ToKeyValue();
            var formUrlEncodedContent = new FormUrlEncodedContent(keyValueContent);
            var urlEncodedString = await formUrlEncodedContent.ReadAsStringAsync();

            return urlEncodedString;
        }

        public static async Task<string> WithQueryString(this string url, object metaToken)
        {
            if (metaToken != null)
            {
                url += await metaToken.ToQueryString();
            }

            return url;
        }



    }
}
