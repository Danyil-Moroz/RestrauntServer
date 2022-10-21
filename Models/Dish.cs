namespace RestrauntServer.Models
{
    using RestrauntServer.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Dish : BaseModel
    {
        public string DishName { get; set; }

        public double PricePerPortion { get; set; }

        public int Portion { get; set; }

        public Portion PortionVariant { get; set; }

        public string Ingredients { get; set; }

        public int Rating { get; set; }

        public int OrderedCount { get; set; }
        public IList<DishPunkt> dishPunkts { get; set; }

        public Dish()
        {
            dishPunkts = new List<DishPunkt>();
        }
    }
}