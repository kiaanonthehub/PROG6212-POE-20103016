using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;

namespace PlannerWebApp.Controllers
{
    public class TblStudentsController : Controller
    {
        private readonly PlannerContext _context;

        public TblStudentsController(PlannerContext context)
        {
            _context = context;
        }

        // GET: TblStudents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblStudents.ToListAsync());
        }

        // GET: TblStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents
                .FirstOrDefaultAsync(m => m.StudentNumber == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // GET: TblStudents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentNumber,StudentName,StudentSurname,StudentEmail,StudentHashPassword")] Student student)
        {
            if (ModelState.IsValid)
            {
                TblStudent tblStudent = new TblStudent();
                tblStudent.StudentNumber = student.StudentNumber;
                tblStudent.StudentName = student.StudentName;
                tblStudent.StudentSurname = student.StudentSurname;
                tblStudent.StudentEmail = student.StudentEmail;
                tblStudent.StudentHashPassword = BCrypt.Net.BCrypt.HashPassword(student.StudentHashPassword);

                Global.StudentNumber = student.StudentNumber;

                _context.Add(tblStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: TblStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents.FindAsync(id);
            if (tblStudent == null)
            {
                return NotFound();
            }
            return View(tblStudent);
        }

        // POST: TblStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentNumber,StudentName,StudentSurname,StudentEmail,StudentHashPassword")] TblStudent tblStudent)
        {
            if (id != tblStudent.StudentNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblStudentExists(tblStudent.StudentNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblStudent);
        }

        // GET: TblStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents
                .FirstOrDefaultAsync(m => m.StudentNumber == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // POST: TblStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblStudent = await _context.TblStudents.FindAsync(id);
            _context.TblStudents.Remove(tblStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblStudentExists(int id)
        {
            return _context.TblStudents.Any(e => e.StudentNumber == id);
        }
    }
}
