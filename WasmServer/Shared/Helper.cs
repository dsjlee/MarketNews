using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace WasmServer.Shared
{
    public class Helper
    {
        public static string FinnHubApiKey;
        public static bool IsNewCaching;
    }

    public struct BaseAddress
    {
        public static readonly string Finnhub = "https://finnhub.io/";
    }

    public struct HttpClientName
    {
        public static readonly string Host = "Host";
        public static readonly string FinnHub = "FinnHub";
    }

    public struct CacheKey
    {
        public static readonly string MarketNews = "MarketNews";
    }

    public struct NewsCategory
    {
        public static readonly string All = "All";
    }
}
