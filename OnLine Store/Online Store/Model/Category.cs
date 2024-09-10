using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace WebAPI2.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual Collection<Product>? Products { get; set; }
    }
}
