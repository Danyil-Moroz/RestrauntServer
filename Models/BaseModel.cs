namespace RestrauntServer.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BaseModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
