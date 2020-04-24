using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qnomyex1.Data;

namespace qnomyex1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApppointmentsController : ControllerBase
    {
        private readonly qnomyex1Context _context;

        public ApppointmentsController(qnomyex1Context context)
        {
            _context = context;
        }

        // GET: api/Apppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetApppointment()
        {
            List<Appointment> allAppointments = await _context.Appointment.ToListAsync();
            allAppointments = allAppointments.Where(item => item.isTreated != 2).ToList();
            allAppointments = allAppointments.OrderBy(x => x.Id).ToList();
            return allAppointments;
        }

        // GET: api/Apppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetApppointment(string id)
        {
            var apppointment = await _context.Appointment.FindAsync(id);

            if (apppointment == null)
            {
                return NotFound();
            }

            return apppointment;
        }

        // PUT: api/Apppointments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> PutApppointment(int id, Appointment apppointment)
        {
            if (id != apppointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(apppointment).State = EntityState.Modified;

            // update waited que
            
            var result = _context.Appointment.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                result.isTreated = 2;
                // _context.SaveChanges();
            }
            

            List<Appointment> allAppointments = await _context.Appointment.ToListAsync();
            allAppointments = allAppointments.Where(item => item.isTreated != 2).ToList();
            allAppointments = allAppointments.OrderBy(x => x.Id).ToList();

            Appointment nextAppointment = allAppointments.FirstOrDefault(b => b.isTreated == 0);

            if (nextAppointment != null)
            {
                result = _context.Appointment.SingleOrDefault(b => b.Id == nextAppointment.Id);
                if (result != null)
                {
                    string c = result.ClientName;
                    result.isTreated = 1;
                    result.ClientName = c;
                    // db.SaveChanges();
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            allAppointments = await _context.Appointment.ToListAsync();
            allAppointments = allAppointments.Where(item => item.isTreated != 2).ToList();
            allAppointments = allAppointments.OrderBy(x => x.Id).ToList();

            return allAppointments;
        }

        // POST: api/Apppointments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Appointment>>> PostApppointment(Appointment apppointment)
        {
            apppointment.appointmentDate = DateTime.Now.AddHours(1);

            _context.Appointment.Add(apppointment);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetApppointment", new { id = apppointment.Id }, apppointment);
            List<Appointment> allAppointments = await _context.Appointment.ToListAsync();
            allAppointments = allAppointments.Where(item => item.isTreated != 2).ToList();
            allAppointments = allAppointments.OrderBy(x => x.Id).ToList();
            return allAppointments;
        }

        // DELETE: api/Apppointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteApppointment(string id)
        {
            var apppointment = await _context.Appointment.FindAsync(id);
            if (apppointment == null)
            {
                return NotFound();
            }

            _context.Appointment.Remove(apppointment);
            await _context.SaveChangesAsync();

            return apppointment;
        }

        private bool ApppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
    }
}
