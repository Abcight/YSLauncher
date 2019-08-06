using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
 
namespace YSLauncher
{
    public class BlogPost
    {
        [JsonProperty("date")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("URL")]
        public string Url { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("categories")]
        public Dictionary<string, object> Categories { get; set; }

        public List<string> CategoryNames
        {
            get
            {
                return Categories.Select(x => x.Key).ToList();
            }
        }
    }
}