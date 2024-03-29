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
    //[Authorize(Roles="Admin,Doctor")]
    [Route("api/[controller]")]//[controller]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly MedicalSystemContext _context;
        //private IWebHostEnvironment _environment;
        public DoctorController(MedicalSystemContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            // _environment = webHostEnvironment;
        }

        // GET: api/Doctors
        [Authorize(Roles = "admin,patient")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("NotBlocked")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetNotBlockedDoctors()
        {
            return await _context.Doctors.Where(a => !_context.Blocked.Any(p2 => p2.email == a.email)).ToListAsync();
        }

        // GET: api/Doctor/get/Dentist
        [Authorize(Roles="admin,patient")]
        [HttpGet("get/{category}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsPerCategory(string category)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            return await _context.Doctors.Where(a => a.category.Contains(category)).ToListAsync();
        }

        [Authorize(Roles = "admin,patient")]
        [HttpGet("getCategories")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCategories()
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            //return await _context.Doctors.Select(cat => new { category = cat.category }).Distinct().ToListAsync();
            return await _context.Doctors.Select(Doctor => Doctor.category).Distinct().ToListAsync();
        }

        // GET: api/Doctors/5
        [Authorize(Roles="admin,patient,doctor")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // GET: api/Doctors/getDoctor/Doctor1
        [Authorize(Roles = "admin,patient")]
        [HttpGet("getDoctor/{name}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorbyName(string name)
        {
            if (_context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.Where(d => d.Fname.Equals(name) || d.Lname.Equals(name)).ToListAsync();

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles="admin,doctor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.ID)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // PUT: api/Doctors/change/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "doctor")]
        [HttpPut("change/{id}")]
        public async Task<IActionResult> ChangePassword(int id, dynamic doctor)
        {
            string oldPass = doctor.oldPass;
            string newPass = doctor.newPass;

            var dr = await _context.Doctors.FindAsync(id);
            if (dr == null)
                return NotFound();

            // Hash the old password
            oldPass = AccountUser.hashPassword(oldPass);
            if (dr.password != oldPass)
                return BadRequest(" Your old password is incorrect!");

            // Hash the new password
            newPass = AccountUser.hashPassword(newPass);
            if (dr.password == newPass)
                return BadRequest("you haven't changed your password");

            dr.password = newPass;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            var drMail = _context.Doctors.Where(a => a.email == doctor.email).FirstOrDefault();
            if (drMail != null)
                return BadRequest("This email already exists !");

            var drPhone = _context.Doctors.Where(a => a.phone == doctor.phone).FirstOrDefault();
            if (drPhone != null)
                return BadRequest("This phone already exists !");

            doctor.password = AccountUser.hashPassword(doctor.password);
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.ID }, doctor);
        }
       
        // DELETE: api/Doctors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDoctor(int id)
        //{
        //    var doctor = await _context.Doctors.FindAsync(id);
        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Doctors.Remove(doctor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.ID == id);
        }
    }
}
