using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Core.Entities;
using Library.Web.Models;
using Library.Core.Interfaces;

namespace Library.Web.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IRepository _context;
        private readonly IRepositoryLibros _repositoryLibros;
        private readonly IServiceLibro _serviceLibro;

        public LibrosController(IRepository repository, IRepositoryLibros repositoryLibros, IServiceLibro serviceLibro)
        {
            _context = repository;
            _repositoryLibros = repositoryLibros;
            _serviceLibro = serviceLibro;
        }

        // GET: Libros
        public IActionResult Index()
        {
            return View(_repositoryLibros.List());
        }

        // GET: Libros/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = _repositoryLibros.GetById((int) id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            var editoriales = _context.List<Editoriales>().Select(x=>new SelectListViewModel { Id =x.Id, Nombre=$"{x.Nombre} {x.Sede}"});
            var autores = _context.List<Autores>().Select(x => new SelectListViewModel { Id = x.Id, Nombre = $"{x.Nombre} {x.Apellidos}" });
            ViewBag.editoriales = editoriales;
            ViewBag.autores = autores;
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ISBN,Titulo,Sinopsis,NPaginas,Active,EditorialesId,AutoresIds")] LibroViewModel libroViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                  var libros = libroViewModel.MapData(_context);

                  _serviceLibro.GuardarLibro(libros);
                   
                    return RedirectToAction(nameof(Index));
                }
                return View(libroViewModel);

            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // GET: Libros/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = _repositoryLibros.GetById((int)id);
            var librosViewModel = LibroViewModel.ToMap(libros);
            var editoriales = _context.List<Editoriales>().Select(x => new SelectListViewModel { Id = x.Id, Nombre = $"{x.Nombre} {x.Sede}" });
            var autores = _context.List<Autores>().Select(x => new SelectListViewModel { Id = x.Id, Nombre = $"{x.Nombre} {x.Apellidos}" });
            var autoresSelected = libros.AutoresHasLibros.Select(x => x.AutoresId).ToArray<int>();
            ViewBag.editoriales = editoriales;
            ViewBag.autores = autores;
            ViewBag.autoresSelected = autoresSelected;
            if (librosViewModel == null)
            {
                return NotFound();
            }
            return View(librosViewModel);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ISBN,Titulo,Sinopsis,NPaginas,Active,EditorialesId,AutoresIds")] LibroViewModel libroViewModel)
        {
            if (id != libroViewModel.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var libros = libroViewModel.MapData(_context);
                    _serviceLibro.ActualizarLibro(id,libros);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrosExists(libroViewModel.ISBN))
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
            return View(libroViewModel);
        }

        // GET: Libros/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = _repositoryLibros.GetById((int)id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libros = _repositoryLibros.GetById((int)id);
            return RedirectToAction(nameof(Index));
        }

        private bool LibrosExists(int id)
        {
            return _repositoryLibros.List().Any(e => e.ISBN == id);
        }
    }
}
