namespace BHNASAapp.Models
{
    using Newtonsoft.Json;
    public class BHModel
    {
        [JsonProperty("copyright")]
        public string? CopyRight { get; set; }//copyright
        [JsonProperty("date")]
        public string? Date { get; set; }//for date
        [JsonProperty("explanation")]
        public string? Explanation { get; set; }//explanation
        [JsonProperty("hdurl")]
        public string? HDUrl { get; set; }//hdurl
        [JsonProperty("media_type")]
        public string? MediaType { get; set; } //media_type
        [JsonProperty("service_version")]
        public string? ServiceVersion { get; set; } //service_version
        [JsonProperty("title")]
        public string? Title { get; set; }//title
        [JsonProperty("url")]
        public string? Url { get; set; }//url
    //Brendan Hannon CPS-3330-01 Spring2023 HW7

    }
}
