using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MonolithicSampleRestApi.Models.Models.ApiModels
{
    public class ResponseApiModel<T>
    {
        public T Response { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, string> ValidationErrors { get; set; } = new Dictionary<string, string>();

        public bool ThereAreErrors
        {
            get
            {
                return ValidationErrors.Count > 0;
            }
        }
    }
}
