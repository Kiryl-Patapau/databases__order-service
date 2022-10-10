using Newtonsoft.Json;

namespace OrderService.Models;

public class Item
{
    [JsonProperty(Required = Required.Always)]
    public int Id { get; set; }

    [JsonProperty(Required = Required.Always)]
    public int Quantity { get; set; }
}
