using FoodDiary.WebAPI.Models;

namespace FoodDiary.WebAPI.DAL.Interfaces
{
    public interface IMealDAO
    {
        public Task AddAsync(Meal meal);
        public Task UpdateAsync(Meal meal);
        public Task DeleteAsync(Meal meal);
        public Task<Meal> FindAsync(int id);
        public Task<IEnumerable<MealDTO>> FindAllAsync();
    }
}
