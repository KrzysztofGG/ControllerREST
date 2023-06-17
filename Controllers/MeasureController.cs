using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllerREST.Models;

namespace ControllerREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly ContextDb _context;

        public MeasureController(ContextDb context)
        {
            _context = context;
        }

        // GET: api/Measure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measure>>> GetMeasure()
        {
          if (_context.Measure == null)
          {
              return NotFound();
          }
            return await _context.Measure.ToListAsync();
        }

        // GET: api/Measure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Measure>> GetMeasure(int id)
        {
          if (_context.Measure == null)
          {
              return NotFound();
          }
            var measure = await _context.Measure.FindAsync(id);

            if (measure == null)
            {
                return NotFound();
            }

            return measure;
        }

        // PUT: api/Measure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasure(int id, Measure measure)
        {
            if (id != measure.Id)
            {
                return BadRequest();
            }

            _context.Entry(measure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Measure
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Measure>> PostMeasure(Measure measure)
        {
          if (_context.Measure == null)
          {
              return Problem("Entity set 'ContextDb.Measure'  is null.");
          }
            _context.Measure.Add(measure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeasure", new { id = measure.Id }, measure);
        }

        // DELETE: api/Measure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasure(int id)
        {
            if (_context.Measure == null)
            {
                return NotFound();
            }
            var measure = await _context.Measure.FindAsync(id);
            if (measure == null)
            {
                return NotFound();
            }

            _context.Measure.Remove(measure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeasureExists(int id)
        {
            return (_context.Measure?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
