using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFNET.Models
{
    public class OutputData
    {
        public ListOutput Results { get; set; }

        public Output GetResponse() => Results.output1.First();
    }

    public class ListOutput
    {
        public List<Output> output1 { get; set; }
    }

    public class Output
    {
        [JsonProperty("Scored Labels")]
        public string ScoredLabels { get; set; }
        [JsonProperty("Scored Probabilities")]
        public string ScoredProbabilities { get; set; }
    }
}