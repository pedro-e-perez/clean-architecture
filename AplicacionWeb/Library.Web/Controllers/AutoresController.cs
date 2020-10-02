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
    public class AutoresController : Controller
    {
        private readonly IRepository _context;

        public AutoresController(IRepository repository)
        {
            _context = repository;
        }

        // GET: Autores
        public  IActionResult Index()
        {
            return View( _context.List<Autores>());
        }

        // GET: Autores/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = _context.GetById<Autores>((int)id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nombre,Apellidos,Active")] Autores autores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autores);
               
                return RedirectToAction(nameof(Index));
            }
            return View(autores);
        }

        // GET: Autores/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores =  _context.GetById<Autores>((int)id);
            if (autores == null)
            {
                return NotFound();
            }
            return View(autores);
        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Nombre,Apellidos,Id,Active")] Autores autores)
        {
            if (id != autores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autores);
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoresExists(autores.Id))
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
            return View(autores);
        }

        // GET: Autores/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = _context.GetById<Autores>((int)id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var autores = _context.GetById<Autores>((int)id);
                _context.Delete(autores);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return NotFound();
            }
           
        }

        private bool AutoresExists(int id)
        {
            return _context.List<Autores>().Any(e => e.Id == id);
        }
    }
}
