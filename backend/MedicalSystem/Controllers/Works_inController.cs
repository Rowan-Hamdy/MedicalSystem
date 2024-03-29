﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Data;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace MedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Works_inController : ControllerBase
    {
        private readonly MedicalSystemContext _context;

        public Works_inController(MedicalSystemContext context)
        {
            _context = context;
        }

        // GET: api/Works_in
        [Authorize(Roles = "patient,admin,doctor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Works_in>>> GetWorks_ins()
        {
            return await _context.Works_ins.ToListAsync();
        }

        // GET: api/Works_in/5
        [HttpGet("{id}")]
        [Authorize(Roles = "patient,admin,doctor")]
        public async Task<ActionResult<IEnumerable<Works_in>>> GetWorks_in(int id)
        {
            List<Works_in> works_in = await _context.Works_ins.Where(r => r.DID == id ).ToListAsync();

            if (works_in == null)
            {
                return NotFound();
            }

            return works_in;
        }

        // PUT: api/Works_in/5
        [Authorize(Roles = "admin")]
        [HttpPut("{did}/{W_ID}")]
        public async Task<IActionResult> PutWorks_in(int did,int W_ID, Works_in works_in)
        {
            //start_time = (start_time.Split("_")[0]+' '+ start_time.Split("_")[1] + ' ' + start_time.Split("_")[2] );
            
            if (did != works_in.DID || W_ID != works_in.W_ID)
            {
                return BadRequest();
            }

            var drTime = await _context.Works_ins.FindAsync(did, W_ID);
            
            if (drTime == null)
                return BadRequest("this dr don\'t have this time ");
            
            drTime.start_time = works_in.start_time;
            drTime.end_time = works_in.end_time;
            drTime.maxpatientNo = works_in.maxpatientNo;

//            _context.Entry(works_in).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Works_inExists(did))
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

        // POST: api/Works_in
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Works_in>> PostWorks_in(Works_in works_in)
        {

            _context.Works_ins.Add(works_in);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Works_inExists(works_in.DID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWorks_in", new { id = works_in.DID }, works_in);

        }

        // DELETE: api/Works_in/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{did}/{start_time}")]
        public async Task<IActionResult> DeleteWorks_in(int did,string start_time)
        {
           // var works_in = await _context.Works_ins.FindAsync(id);
            var works_in = await _context.Works_ins.Where(r => r.DID == did&& r.start_time== start_time).FirstOrDefaultAsync();
            if (works_in == null)
            {
                return NotFound();
            }

            _context.Works_ins.Remove(works_in);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Works_inExists(int id)
        {
            return _context.Works_ins.Any(e => e.DID == id);
        }


    }
}
