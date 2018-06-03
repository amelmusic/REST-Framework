using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using A.Core.Extensions;
using A.Core.Interface;
using A.Core.Model;
using A.Core.Validation;
using Flurl.Http;
using Flurl.Http.Content;
using Flurl.Util;

namespace $rootnamespace$.Core //REPLACED
{
    public static class FlurlExtension
    {
        public static IFlurlRequest WithStandardHeaders(this string url, IActionContext context)
        {
            context.Data.TryGetValue("AuthorizationToken", out var token);
            context.Data.TryGetValue("Language", out var language);
            context.Data.TryGetValue("RequestId", out var requestId);

            var request = url.WithOAuthBearerToken(token?.ToString());
            request = request.WithHeader("Accept-Language", language?.ToString());
            request = request.WithHeader("RequestId", requestId?.ToString());
            return request;
        }

        public static string ToQueryStringInternal(string url, object model)
        {
            if (model != null && !url.EndsWith("?"))
            {
                url += "?"; //append querystring delmiter
            }

            var nameValueCollection = model?.ToKeyValue();
            if (nameValueCollection != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (KeyValuePair<string, string> nameValue in nameValueCollection)
                {
                    if (stringBuilder.Length > 0)
                        stringBuilder.Append('&');
                    stringBuilder.Append(nameValue.Key);
                    stringBuilder.Append('=');
                    stringBuilder.Append(nameValue.Value);
                }
                url += stringBuilder.ToString();
            }

            return url;
        }

        public static async Task<T> GetAsync<T>(this string url, IActionContext context, object queryString = null)
        {
            try
            {
                var request = await ToQueryStringInternal(url, queryString).WithStandardHeaders(context).GetJsonAsync<T>();

                return request;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }

        public static async Task<T> PostAsync<T>(this string url, IActionContext context, object queryString = null, object model = null)
        {
            try
            {
                var request = await ToQueryStringInternal(url, queryString).WithStandardHeaders(context)
                    .PostJsonAsync(model).ReceiveJson<T>();

                return request;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }

        public static async Task<T> PutAsync<T>(this string url, IActionContext context, object queryString = null, object model = null)
        {
            try
            {
                var request = await ToQueryStringInternal(url, queryString).WithStandardHeaders(context)
                    .PutJsonAsync(model).ReceiveJson<T>();

                return request;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }

        public static async Task<T> PatchAsync<T>(this string url, IActionContext context, object queryString = null, object model = null)
        {
            try
            {
                var request = await ToQueryStringInternal(url, queryString).WithStandardHeaders(context)
                    .PatchJsonAsync(model).ReceiveJson<T>();

                return request;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }

        public static async Task<T> DeleteAsync<T>(this string url, IActionContext context, object queryString = null, object model = null)
        {
            try
            {
                var request = await ToQueryStringInternal(url, queryString).WithStandardHeaders(context)
                    .DeleteAsync().ReceiveJson<T>();

                return request;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }

        public static async Task<DownloadRequest> DownloadAsync<T>(this string url, IActionContext context, object queryString = null, object model = null, bool isMethodGet = true)
        {
            try
            {
                if (queryString != null && !url.EndsWith("?"))
                {
                    url += "?"; //append querystring delmiter
                }
                DownloadRequest download = new DownloadRequest();

                var request = ToQueryStringInternal(url, queryString).WithStandardHeaders(context);
                var httpMethod = HttpMethod.Get;
                if (!isMethodGet)
                {
                    httpMethod = HttpMethod.Post;
                }
                CapturedJsonContent capturedJsonContent = new CapturedJsonContent(request.Settings.JsonSerializer.Serialize(model));

                HttpResponseMessage httpResponseMessage = await request.SendAsync(httpMethod, isMethodGet == true ? null : capturedJsonContent,
                    new CancellationToken?(), HttpCompletionOption.ResponseHeadersRead);
                HttpContent content = httpResponseMessage.Content;


                if (content.Headers.ContentDisposition != null)
                {
                    download.FileName = content.Headers.ContentDisposition.FileName.StripQuotes();
                    download.ContentType = content.Headers.ContentType.MediaType;
                    download.DispositionType = content.Headers.ContentDisposition.DispositionType;
                }


                download.PushStreamFunction = async (stream) =>
                {
                    using (var downloadStream = await httpResponseMessage.Content.ReadAsStreamAsync())
                    using (var reader = new StreamContent(downloadStream))
                    using (stream) //this has to be called in order to signal caller that we have finished
                    {
                        await reader.CopyToAsync(stream);
                    }
                };
                return download;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }

        public static async Task<T> UploadAsync<T>(this string url, IActionContext context, Stream stream, string fileName, string mediaType, object queryString = null)
        {
            try
            {
                if (queryString != null && !url.EndsWith("?"))
                {
                    url += "?"; //append querystring delmiter
                }

                var request = await ToQueryStringInternal(url, queryString).WithStandardHeaders(context)
                    .PostMultipartAsync(mp => mp
                        .AddFile("file", stream, fileName, mediaType: mediaType))
                    .ReceiveJson<T>();

                return request;
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call?.HttpStatus == HttpStatusCode.BadRequest)
                {
                    var validationResult = ex.GetResponseJson<ValidationResult>();
                    if (validationResult != null)
                    {
                        throw new ValidationException(validationResult);
                    }
                }
                throw;
            }
        }
    }
}
