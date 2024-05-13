using System.Text.Json.Serialization;

namespace Zadanie.Models
{
    public class DataModelDbo
    {
        [JsonPropertyName("productId")]
        public string ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }
    }
}
