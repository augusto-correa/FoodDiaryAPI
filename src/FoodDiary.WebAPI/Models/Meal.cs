namespace FoodDiary.WebAPI.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Food> Foods { get; set; }
    }
}