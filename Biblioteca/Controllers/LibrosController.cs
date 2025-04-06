using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaBiblioteca.Models;

namespace Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {            
            var libros = _context.Libros
                .Select(libro => new
                {
                    libro.Id,
                    libro.Titulo,
                    libro.Autor,
                    libro.AnioPublicacion,
                    EstadoDescripcion = _context.Estado
                        .Where(estado => estado.id == libro.EstadoId)
                        .Select(estado => estado.descripcion)
                        .FirstOrDefault() 
                })
                .ToList();
            
            return View(libros);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var estados = _context.Estado.ToList();


            if (estados != null && estados.Any())
            {
                ViewBag.Estados = new SelectList(estados, "id", "descripcion");
            }
            else
            {
                ViewBag.Estados = new SelectList(new List<Estado>());
            }

            return View();
        }

        [HttpPost]
        public IActionResult Crear(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Estados = new SelectList(_context.Estado.ToList(), "id", "Descripcion");
            return View(libro);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            var estados = _context.Estado.ToList();            
            ViewBag.Estados = new SelectList(estados, "id", "descripcion", libro.EstadoId);

            return View(libro);
        }

        [HttpPost]
        public IActionResult Editar(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Update(libro);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

           
            var estados = _context.Estado.ToList();
            ViewBag.Estados = new SelectList(estados, "id", "descripcion", libro.EstadoId);

            return View(libro);
        }



        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return NotFound(); 
            }
            return View(libro); 
        }

        
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return NotFound(); 
            }

            _context.Libros.Remove(libro); 
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index)); 
        }

       



    }
}
