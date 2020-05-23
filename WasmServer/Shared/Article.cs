using System;
using System.Collections.Generic;
using System.Text;

namespace WasmServer.Shared
{
    public class Article
    {
        public string Category { get; set; }
        public double DateTime { get; set; }
        public string Headline { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Related { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }

        public DateTime Date => new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(DateTime).ToLocalTime();
    }
}
