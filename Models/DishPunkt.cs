using System.ComponentModel.DataAnnotations;

namespace RestrauntServer.Models
{
    public class DishPunkt : BaseModel
    {
       public int OrderId { get; set; }

        public int DishId { get; set; }
        
        public string UsersNotes { get; set; }

        [Required]
        public int DishCount { get; set; }

    }
}
