﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Data;
using MedicalSystem.Models;

namespace MedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public VisitController(MedicalSystemContext context)
        {
            _context = context;
        }

        // GET: api/Visits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visit>>> GetVisits()
        {
            return await _context.Visits.ToListAsync();
        }

        // GET: api/Visits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visit>> GetVisit(int id)
        {
            var visit = await _context.Visits.FindAsync(id);

            if (visit == null)
            {
                return NotFound();
            }

            return visit;
        }

        //GET: api/get/visit/{doctor_id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IEnumerable<Visit>>> GetVisitsByDoctorID(int id)
        {
            if (_context.Visits == null)
            {
                return NotFound();
            }
            return await _context.Visits.Where(e => e.DID == id).OrderBy(a=>a.appointment_time).ToListAsync();
        }

        //GET: api/get/visit/{doctor_id}
        [HttpGet("get/{did}/{date}")]
        public async Task<ActionResult<int>> GetPatientsNo(int did,DateTime date)
        {
            if (_context.Visits == null)
            {
                return NotFound();
            }
            return await _context.Visits.Where(e => e.DID == did && e.appointment_time == date).CountAsync();
        }

        // PUT: api/Visits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisit(int id, Visit visit)
        {
            if (id != visit.PID)
            {
                return BadRequest();
            }

            _context.Entry(visit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(id))
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

        // POST: api/Visits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visit>> PostVisit(Visit visit)
        {
            await _context.Procedures.Insert_VisitAsync(visit.PID,visit.DID,visit.appointment_time);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VisitExists(visit.PID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVisit", new { id = visit.PID }, visit);
        }

        // DELETE: api/Visits/5
        [HttpDelete("{pid}/{did}/{date}")]
        public async Task<IActionResult> DeleteVisit(int pid,int did,DateTime date)
        {
            var visit = await _context.Visits.Where(e => e.DID == did && e.PID==pid && e.appointment_time==date).FirstOrDefaultAsync();
            if (visit == null)
            {
                return NotFound();
            }

            await _context.Procedures.Delete_VisitAsync(visit.PID, visit.DID, visit.appointment_time);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //api/Visit/pid/did/date
        [HttpPut("{pid}/{did}/{date}")]
        public async Task<IActionResult> VisitTests(int pid, int did, DateTime date, Visit visit)
        {
            if (pid != visit.PID)
            {
                return BadRequest();
            }

            Visit Visit = await _context.Visits.Where(v => v.DID == did && v.PID == pid && v.appointment_time == date).FirstOrDefaultAsync();
            if (Visit != null)
            {
                await _context.Procedures.Update_VisitAsync(pid, did,date, visit.appointment_time);
            }
            else
            {
                await _context.Procedures.Insert_VisitAsync(pid, did, date);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VisitExists(@visit.PID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.PID == id);
        }
        
    }
}
