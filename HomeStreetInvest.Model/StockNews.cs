using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class Datum
    {
        public string uuid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string keywords { get; set; }
        public string snippet { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string language { get; set; }
        public string published_at { get; set; }
        public string source { get; set; }
        public object relevance_score { get; set; }
        public List<Entity> entities { get; set; }
        public List<Similar> similar { get; set; }
    }

    public class Entity
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public object exchange { get; set; }
        public object exchange_long { get; set; }
        public string country { get; set; }
        public string type { get; set; }
        public string industry { get; set; }
        public double match_score { get; set; }
        public double sentiment_score { get; set; }
        public List<Highlight> highlights { get; set; }
    }

    public class Highlight
    {
        public string highlight { get; set; }
        public double sentiment { get; set; }
        public string highlighted_in { get; set; }
    }

    public class Meta
    {
        public int found { get; set; }
        public int returned { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
    }

    public class Root
    {
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Similar
    {
        public string uuid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string keywords { get; set; }
        public string snippet { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public string language { get; set; }
        public string published_at { get; set; }
        public string source { get; set; }
        public object relevance_score { get; set; }
        public List<Entity> entities { get; set; }
    }


}
