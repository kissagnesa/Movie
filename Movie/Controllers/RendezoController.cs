using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;

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
        public async Task<IActionResult> Put([FromForm] string Json)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Rendezo rendezo = JsonConvert.DeserializeObject<Rendezo>(Json);

                    var lRendezo = await context.Rendezos.FindAsync(rendezo.Id);

                    if (lRendezo != null)
                    {
                        lRendezo.Nev = rendezo.Nev;
                        lRendezo.Nemzetiseg = rendezo.Nemzetiseg;
                        lRendezo.SzulDatum = rendezo.SzulDatum;

                        context.Rendezos.Update(lRendezo);
                        await context.SaveChangesAsync();

                        return Ok("A rendező adatainak módosítása sikeresen megtörtént.");
                    }
                    else
                    {
                        return NotFound("A megadott ID-val nem találtam rendezőt.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }

        [HttpPost]
        //http?://localhost:xxxx/api/film

        public async Task<IActionResult> Post([FromForm] string Json)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    Rendezo rendezo = JsonConvert.DeserializeObject<Rendezo>(Json); using (var ms = new MemoryStream())

                        rendezo.Id = 0;
                    context.Rendezos.Add(rendezo);
                    await context.SaveChangesAsync();
                    return Ok("Az új rendező felvétele sikeresen megtörtént.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


        }

    }

}

    


