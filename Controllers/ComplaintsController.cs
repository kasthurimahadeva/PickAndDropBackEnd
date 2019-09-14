using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickAndDropBackEnd.Data;
using PickAndDropBackEnd.Models;

namespace PickAndDropBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly ComplaintsDbContext _context;

        public ComplaintsController(ComplaintsDbContext context)
        {
            _context = context;
        }

            // GET: api/Complaints
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Complaints>>> GetComplaintsList()
        {
            return await _context.Complaints.ToListAsync();
        }

//        // GET: api/Complaints/5
//        [HttpGet("{id}", Name = "Get")]
//        public string Get(int id)
//        {
//            return "value";
//        }

        // POST: api/Complaints
        [HttpPost]
        public async Task<ActionResult<Complaints>> SaveComplaint(Complaints complaints)
        {
            _context.Complaints.Add(complaints);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplaintsList", new { id = complaints.ComplaintId }, complaints);
        }

        // PUT: api/Complaints/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutComplaint(long id, Complaints complaints)
        {
            if(id!=complaints.ComplaintId)
            {
                return BadRequest();
            }
            _context.Entry(complaints).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintExists(id))
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


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Complaints>> DeleteComplaint(long id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();

            return complaint;
        }

 	private bool ComplaintExists(long id)
        {
            return _context.Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
