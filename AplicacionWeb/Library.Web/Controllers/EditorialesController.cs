using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Core.Entities;
using Library.Infrastructure.Data;
using Library.Core.Interfaces;

namespace Library.Web.Controllers
{
    public class EditorialesController : Controller
    {
        private readonly IRepository _context;

        public EditorialesController(IRepository repository)
        {
            _context = repository;
        }

        // GET: Editoriales
        public IActionResult Index()
        {
            return View(_context.List<Editoriales>());
        }

        // GET: Editoriales/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriales = _context.GetById<Editoriales>((int)id);
            if (editoriales == null)
            {
                return NotFound();
            }

            return View(editoriales);
        }

        // GET: Editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nombre,Sede,Active")] Editoriales editoriales)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(editoriales);
                    return RedirectToAction(nameof(Index));
                }
                return View(editoriales);
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        // GET: Editoriales/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriales = _context.GetById<Editoriales>((int)id);
            if (editoriales == null)
            {
                return NotFound();
            }
            return View(editoriales);
        }

        // POST: Editoriales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Nombre,Sede,Id,Active")] Editoriales editoriales)
        {
            if (id != editoriales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editoriales);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialesExists(editoriales.Id))
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
            return View(editoriales);
        }

        // GET: Editoriales/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriales =  _context.GetById<Editoriales>((int)id);
            if (editoriales == null)
            {
                return NotFound();
            }

            return View(editoriales);
        }

        // POST: Editoriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var editoriales = _context.GetById<Editoriales>((int)id);
                _context.Delete(editoriales);
            }
            catch (Exception)
            {

                return NotFound();
            }
           
            return RedirectToAction(nameof(Index));
        }

        private bool EditorialesExists(int id)
        {
            return _context.List<Editoriales>().Any(e => e.Id == id);
        }
    }
}
