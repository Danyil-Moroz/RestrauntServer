namespace RestrauntServer.Models
{
    using RestrauntServer.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order : BaseModel 
    {
        public int ClientId { get; set; }

        public Status Status { get; set; }

        public string Address { get; set; }

        public int? TableNumber { get; set; }

        public bool IsDelivery { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public IList<DishPunkt> dishPunkts { get; set; }

        public Order()
        {
            dishPunkts = new List<DishPunkt>();
        }

    }
}