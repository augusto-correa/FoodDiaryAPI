using FoodDiary.WebAPI.DAL.Interfaces;
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
        private readonly IMealDAO _mealDAO;

        public MealsController(FoodDiaryContext context, IMealDAO mealDAO)
        {
            _mealDAO = mealDAO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        {
            var meals = await _mealDAO.FindAllAsync();
            return new ActionResult<IEnumerable<MealDTO>>(meals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealDTO>> GetMeal(int id)
        {
            var meal = await _mealDAO.FindAsync(id);

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

            var meal = await _mealDAO.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            try
            {
                meal.Name = mealDTO.Name;
                meal.Date = mealDTO.Date;
                meal.Foods = mealDTO.Foods;
                await _mealDAO.UpdateAsync(meal);
            }
            catch (DbUpdateConcurrencyException) when (!MealExists(id).Result)
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

            await _mealDAO.AddAsync(meal);

            return CreatedAtAction(
                nameof(CreateMeal),
                new { id = meal.Id },
                MealDTO.FromMeal(meal));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _mealDAO.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            await _mealDAO.DeleteAsync(meal);
            return NoContent();
        }

        private async Task<bool> MealExists(long id)
        {
            var meals = await _mealDAO.FindAllAsync();
            return meals.Any(e => e.Id == id);
        }
    }
}