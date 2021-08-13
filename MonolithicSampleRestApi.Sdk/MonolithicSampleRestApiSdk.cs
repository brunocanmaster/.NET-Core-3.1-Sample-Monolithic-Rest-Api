using MonolithicSampleRestApi.Domain.Models.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MonolithicSampleRestApi.Sdk
{
    public class MonolithicSampleRestApiSdk
    {
        private HttpClient _HttpClient { get; set; }
        private string _ApiHost { get; set; }
        private string _ApiVersion { get; set; }
        private string _BasicToken { get; set; }

        public MonolithicSampleRestApiSdk(string apiHost, string basicToken, string version = "v1")
        {
            this._ApiHost = apiHost;
            this._ApiVersion = version;
            this._BasicToken = basicToken;

            _HttpClient = new HttpClient();
            _HttpClient.DefaultRequestHeaders.Add("Authorization", _BasicToken);
        }

        private string GetUrlResource(string resource)
        {
            return this._ApiHost + "/" + resource + "/" + _ApiVersion;
        }

        private delegate void RequestValidation<TResult, TRequest>(ResponseApiModel<TResult> apiResult, TRequest apiRequest);
        private async Task<ResponseApiModel<TResult>> GetAsync<TResult>(string url)
        {
            ResponseApiModel<TResult> r = new ResponseApiModel<TResult>();

            using (var response = await _HttpClient.GetAsync(url))
            {
                r.StatusCode = response.StatusCode;
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    try
                    {
                        Dictionary<string, string> errors = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);
                        r.ValidationErrors = errors;
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            Dictionary<string, string[]> errors = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(apiResponse);
                            r.ValidationErrors = errors.ToDictionary(a => a.Key, b => b.Value.FirstOrDefault());
                        }
                        catch (Exception e2)
                        {
                            r.ValidationErrors = new Dictionary<string, string>();
                            r.ValidationErrors.Add("", "Aconteceu um erro inesperado");
                        }
                    }
                }
                else
                    r.Response = JsonConvert.DeserializeObject<TResult>(apiResponse);
            }

            return r;
        }

        private async Task<ResponseApiModel<TResult>> PostAsync<TResult, TRequest>(string url, TRequest request, RequestValidation<TResult, TRequest> validation = null)
        {
            ResponseApiModel<TResult> r = new ResponseApiModel<TResult>();

            if (validation != null)
                validation(r, request);

            if (r.ThereAreErrors)
                return r;

            _HttpClient.DefaultRequestHeaders
           .Accept
           .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            string jsonObj = JsonConvert.SerializeObject(request);

            HttpRequestMessage requestObj = new HttpRequestMessage(HttpMethod.Post, url);

            requestObj.Content = new StringContent(jsonObj,
                                                Encoding.UTF8,
                                                "application/json");//CONTENT-TYPE header

            using (var response = await _HttpClient.SendAsync(requestObj))
            {
                r.StatusCode = response.StatusCode;
                string apiResponse = await response.Content.ReadAsStringAsync();
                System.Net.HttpStatusCode[] inStatusCode = new System.Net.HttpStatusCode[] { System.Net.HttpStatusCode.Created, System.Net.HttpStatusCode.OK };
                if (!inStatusCode.Contains(response.StatusCode))
                {
                    try
                    {
                        Dictionary<string, string> errors = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);
                        r.ValidationErrors = errors;
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            Dictionary<string, string[]> errors = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(apiResponse);
                            r.ValidationErrors = errors.ToDictionary(a => a.Key, b => b.Value.FirstOrDefault());
                        }
                        catch (Exception e2)
                        {
                            r.ValidationErrors = new Dictionary<string, string>();
                            r.ValidationErrors.Add("", "Aconteceu um erro inesperado");
                        }
                    }
                }
                else
                    r.Response = JsonConvert.DeserializeObject<TResult>(apiResponse);
            }

            return r;
        }

        public async Task<ResponseApiModel<decimal>> GetInterestRate()
        {
            string url = GetUrlResource("financial") + "/interestRate";
            return await GetAsync<decimal>(url);
        }

        public async Task<ResponseApiModel<CalcInterest>> PostCalcInterest(RequestCalcInterest request)
        {
            string url = GetUrlResource("financial") + "/calcInterest";
            return await PostAsync<CalcInterest, RequestCalcInterest>(url, request, (apiResult, apiRequest) =>
            {
                if (request.InitialValue <= 0)
                    apiResult.ValidationErrors.Add("InitialValue", "Deve ser maior que 0");
                if (request.MonthQuantity <= 0)
                    apiResult.ValidationErrors.Add("MonthQuantity", "Deve ser maior que 0");

            });
        }

        public async Task<ResponseApiModel<string>> GetGitHubAppPath()
        {
            string url = GetUrlResource("github");
            return await GetAsync<string>(url);
        }
    }
}
