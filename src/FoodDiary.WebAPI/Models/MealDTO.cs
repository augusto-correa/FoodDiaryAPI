namespace FoodDiary.WebAPI.Models
{
    public class MealDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Food> Foods { get; set; }


        public static MealDTO FromMeal(Meal meal) =>
            new MealDTO
            {
                Id = meal.Id,
                Name = meal.Name,
                Foods = meal.Foods,
                Date = meal.Date
            };
    }
}