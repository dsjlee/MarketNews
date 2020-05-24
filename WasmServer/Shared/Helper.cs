using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace WasmServer.Shared
{
    public class Helper : IHelper
    {
        public Helper(IConfiguration configuration)
        {
            Configuration = configuration;
            FinnHubApiKey = Configuration["FinnHub:ApiKey"];
        }
        public IConfiguration Configuration { get; }
        public string FinnHubApiKey { get; }
    }

    public struct BaseAddress
    {
        public static readonly string Finnhub = "https://finnhub.io/api/v1/";
    }

    public struct HttpClientName
    {
        public static readonly string Host = "Host";
        public static readonly string FinnHub = "FinnHub";
    }
}
