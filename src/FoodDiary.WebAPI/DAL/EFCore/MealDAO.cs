using FoodDiary.WebAPI.DAL.Interfaces;
using FoodDiary.WebAPI.DatabaseContext;
using FoodDiary.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.WebAPI.DAL.EFCore
{
    public class MealDAO : IMealDAO
    {
        private readonly FoodDiaryContext _context;
        public MealDAO(FoodDiaryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Meal meal)
        {
            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Meal meal)
        {
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
        }

        public async Task<Meal> FindAsync(int id)
        {
            return await _context.Meals.Include(m => m.Foods).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MealDTO>> FindAllAsync()
        {
            return await _context.Meals.Include(m => m.Foods)
                .Select(m => MealDTO.FromMeal(m))
                .ToListAsync();
        }

        public async Task UpdateAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }
    }
}
