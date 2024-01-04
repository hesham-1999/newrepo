using api1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class counteryController : ControllerBase
    {
        private readonly appcontext _context;

        public counteryController(appcontext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var allCountry =await _context.countries.ToListAsync();
            return Ok(allCountry);
        }
        [HttpPost]
        public async Task<IActionResult> create(Country country)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            int count = _context.countries.Where(c => c.Name.ToLower() == country.Name.ToLower()).ToList().Count();
            if(count > 0)
            {
                return BadRequest($"Countery nmae {country.Name} exists");
            }
            try
            {
                await _context.countries.AddAsync(country);
                await _context.SaveChangesAsync();
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
           
        }
    }
}
