namespace RestrauntServer.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestrauntServer.Models;

    public class RestrauntDb : DbContext
    {
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<DishPunkt> DishPunkts{ get; set; }
        public DbSet<Category> Category { get; set; }

        public RestrauntDb (DbContextOptions<RestrauntDb> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<DishPunkt>()
                .HasOne(x=>x.Dish)
                .WithMany(x => x.dishPunkts)
                .HasForeignKey(x => x.DishId)
                .IsRequired(true);

            model.Entity<DishPunkt>()
               .HasOne<Order>()
               .WithMany(x => x.dishPunkts)
               .HasForeignKey(x => x.OrderId)
               .IsRequired(true);

            model.Entity<Order>()
                .HasOne<Client>()
                .WithMany(x => x.orders)
                .HasForeignKey(x => x.ClientId)
                .IsRequired(true);

            model.Entity<Dish>()
                .HasOne<Category>(x => x.Category)
                .WithOne(x => x.Dish)
                .HasForeignKey<Dish>(x => x.CategoryId)
                .IsRequired(true);             

        }
       
    }
}
