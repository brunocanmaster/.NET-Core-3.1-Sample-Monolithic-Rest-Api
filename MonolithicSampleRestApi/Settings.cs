using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonolithicSampleRestApi
{
    public  class MonolithicSampleRestApiSdkSettings
    {
        public string ApiHost { get; set; }

        public string BasicToken { get; set; }

        public string Version = "v1";
    }


    public static class Settings
    {
        
        public static MonolithicSampleRestApiSdkSettings MonolithicSampleRestApiSdkSettings { get; set; }

    }
}
