using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrderService.Models;

public class Order
{
    [JsonProperty(Required = Required.Always)]
    public string ShippingAddress { get; set; }

    [JsonProperty(Required = Required.Always)]
    public IEnumerable<Item> Items { get; set; }

    [JsonProperty(Required = Required.Always)]
    public decimal FinalPrice { get; set; }
}
