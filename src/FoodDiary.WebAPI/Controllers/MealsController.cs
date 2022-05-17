using FoodDiary.WebAPI.DatabaseContext;
using FoodDiary.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly FoodDiaryContext _context;

        public MealsController(FoodDiaryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        {
            return await _context.Meals
                .Select(x => MealDTO.FromMeal(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealDTO>> GetMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            return MealDTO.FromMeal(meal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(int id, MealDTO mealDTO)
        {
            if (id != mealDTO.Id)
            {
                return BadRequest();
            }

            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            meal.Name = mealDTO.Name;
            meal.Date = mealDTO.Date;
            meal.Foods = mealDTO.Foods;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MealExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MealDTO>> CreateMeal(MealDTO mealDTO)
        {
            var meal = new Meal
            {
                Name = mealDTO.Name,
                Foods = mealDTO.Foods,
                Date = mealDTO.Date
            };

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMeal),
                new { id = meal.Id },
                MealDTO.FromMeal(meal));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(long id)
        {
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool MealExists(long id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}