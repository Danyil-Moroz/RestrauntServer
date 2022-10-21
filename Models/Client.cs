namespace RestrauntServer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public IList<Order> orders { get; set; }

        public Client()
        {
            orders = new List<Order>();
        }
    }
}
