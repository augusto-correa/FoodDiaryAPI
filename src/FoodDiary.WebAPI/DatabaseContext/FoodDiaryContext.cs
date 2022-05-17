using Microsoft.EntityFrameworkCore;
using FoodDiary.WebAPI.Models;

namespace FoodDiary.WebAPI.DatabaseContext
{
    public class FoodDiaryContext : DbContext
    {
        public FoodDiaryContext(DbContextOptions<FoodDiaryContext> options) : base(options)
        {

        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>(meal =>
            {
                meal.HasKey(m => m.Id);
                meal.HasMany(m => m.Foods).WithOne();
            });

            modelBuilder.Entity<Food>(food =>
            {
                food.HasKey(f => f.Id);
            });



            base.OnModelCreating(modelBuilder);
        }
    }
}