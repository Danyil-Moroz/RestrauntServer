namespace RestrauntServer.Models
{
    using RestrauntServer.Enums;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    public class Dish : BaseModel
    {
        public string DishName { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public double PricePerPortion { get; set; }

        public string ImageURL { get; set; }

        public int Portion { get; set; }

        public Portion PortionVariant { get; set; }

        public string Ingredients { get; set; }

        public int Rating { get; set; }

        public int OrderedCount { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public IList<DishPunkt> dishPunkts { get; set; }

        public Dish()
        {
            dishPunkts = new List<DishPunkt>();
            Category = new Category();
        }
    }
}