using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Mvc
{
    public static class GlobalVariablecs
    {
        public static HttpClient WebApiClient = new HttpClient();

        static  GlobalVariablecs()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:64896/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}