using kpz_test4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace kpz_test4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [ApiExplorerSettings(GroupName = "v1")]
    public class jewelryController : ControllerBase
    {
        private  Context _context;
        public jewelryController(Context context) {
            _context = context;
        }


        /// <summary>
        /// Отримати список всіх прикрас
        /// </summary>
        /// <response code="200">Повертає список всіх прикрас</response>
        /// <response code="404">Якщо список прикрас порожній</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Jewelry>>> GetJewelry()
        {
            if (_context.jewelry == null)
            {
                return NotFound();
            }
            return await _context.jewelry.ToListAsync();
        }

        /// <summary>
        /// Отримати прикрасу за її ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор прикраси</param>
        /// <response code="200">Повертає прикрасу з вказаним ідентифікатором</response>
        /// <response code="404">Якщо прикрасу не знайдено за вказаним ідентифікатором</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Jewelry>> GetJewelryById(int id)
        {
            if (_context.jewelry == null)
            {
                return NotFound();
            }
            var jewelry =await _context.jewelry.FindAsync(id);
            if (jewelry == null)
            {
                return NotFound();
            }
            return jewelry;
        }


        /// <summary>
        /// Додати нову прикрасу
        /// </summary>
        /// <remarks>
        /// Приклад запиту:
        ///
        ///     POST
        ///     {
        ///        "id": 0,
        ///         "jewelry_name": "NameJewelry",
        ///          "price": 100,
        ///         "discount_id": 2,
        ///           "category_id": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Повертає щойно створену прикрасу</response>
        /// <response code="400">Якщо елемент нульовий</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Jewelry>> PostJewelry(Jewelry jewelry)
        {
            _context.jewelry.Add(jewelry);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJewelry), new { id = jewelry.id }, jewelry);
        }

        /// <summary>
        /// Оновити інформацію про прикрасу за її ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор прикраси</param>
        /// <param name="jewelry">Об'єкт прикраси з новою інформацією</param>
        /// <response code="200">Повертається при успішному оновленні інформації про прикрасу</response>
        /// <response code="400">Якщо ідентифікатор прикраси не збігається з id у тілі запиту</response>
        /// <response code="404">Якщо прикрасу з вказаним ідентифікатором не знайдено</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<Jewelry>> PutJewelry(int id, Jewelry jewelry)
        {
            if (jewelry.id != id)
            {
                return BadRequest();
            }
            _context.Entry(jewelry).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewelryAvailable(id))
                {
                    return NotFound();
                }
                else throw;
            }
            return Ok();

        }
        private bool JewelryAvailable(int id)
        {
            return (_context.jewelry?.Any(x => x.id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Видалити прикрасу за її ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор прикраси, яку слід видалити</param>
        /// <response code="200">Повертається при успішному видаленні прикраси</response>
        /// <response code="404">Якщо прикрасу з вказаним ідентифікатором не знайдено</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteJewelry(int id)
        {
            if (_context.jewelry == null)
            {
                return NotFound();
            }
            var jewelry = await _context.jewelry.FindAsync(id);
            if (jewelry == null)
            {
                return NotFound();
            }
            _context.jewelry.Remove(jewelry);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
