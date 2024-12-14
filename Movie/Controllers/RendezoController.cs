using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Newtonsoft.Json;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendezoController : ControllerBase
    {
        [HttpDelete("{id}")]
        //http?://localhost:xxxx/api/rendezo/id
        public IActionResult Delete(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Rendezo rendezo = new Rendezo { Id = id };
                    context.Rendezos.Remove(rendezo);
                    context.SaveChanges();
                    return Ok("A rendező sikeresen törölve.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        //http?://localhost:xxxx/api/rendezo
        public async Task<IActionResult> Put([FromForm] string Json, [FromForm] IFormFile indexKep, [FromForm] IFormFile kep)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Rendezo rendezo = JsonConvert.DeserializeObject<Rendezo>(Json);
                 
                    context.Rendezos.Update(rendezo);
                    await context.SaveChangesAsync();
                    return Ok("A rendező adatainak a módosítása sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        

        }

    }


