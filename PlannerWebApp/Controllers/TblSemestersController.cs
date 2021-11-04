using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannerLibrary.DbModels;
using PlannerLibrary.Models;

namespace PlannerWebApp.Controllers
{
    public class TblSemestersController : Controller
    {
        private readonly PlannerContext _context;

        public TblSemestersController(PlannerContext context)
        {
            _context = context;
        }

        // GET: TblSemesters
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblSemesters.ToListAsync());
        }

        // GET: TblSemesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSemester = await _context.TblSemesters
                .FirstOrDefaultAsync(m => m.SemesterId == id);
            if (tblSemester == null)
            {
                return NotFound();
            }

            return View(tblSemester);
        }

        // GET: TblSemesters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblSemesters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,NumberOfWeeks")] Semster semester)
        {
            if (ModelState.IsValid)
            {
                TblSemester tblSemester = new TblSemester();
                tblSemester.StartDate = semester.StartDate;
                tblSemester.NumberOfWeeks = semester.NumberOfWeeks;
                _context.Add(tblSemester);

                Global.StartDate = semester.StartDate;
                Global.NoOfWeeks = (int)semester.NumberOfWeeks;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semester);
        }

        // GET: TblSemesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSemester = await _context.TblSemesters.FindAsync(id);
            if (tblSemester == null)
            {
                return NotFound();
            }
            return View(tblSemester);
        }

        // POST: TblSemesters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemesterId,StartDate,NumberOfWeeks")] TblSemester tblSemester)
        {
            if (id != tblSemester.SemesterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblSemester);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSemesterExists(tblSemester.SemesterId))
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
            return View(tblSemester);
        }

        // GET: TblSemesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSemester = await _context.TblSemesters
                .FirstOrDefaultAsync(m => m.SemesterId == id);
            if (tblSemester == null)
            {
                return NotFound();
            }

            return View(tblSemester);
        }

        // POST: TblSemesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblSemester = await _context.TblSemesters.FindAsync(id);
            _context.TblSemesters.Remove(tblSemester);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSemesterExists(int id)
        {
            return _context.TblSemesters.Any(e => e.SemesterId == id);
        }
    }
}
