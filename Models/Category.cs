using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RestrauntServer.Models
{
    public class Category : BaseModel
    {
        public string CategoryName { get; set; }

        public string ImageURL { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Dish Dish { get; set; }
    }
}
