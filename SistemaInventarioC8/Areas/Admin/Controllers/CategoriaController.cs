using Microsoft.AspNetCore.Mvc;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioC8.Modelos;
using SistemaInventarioC8.Utilidades;

namespace SistemaInventarioC8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            if (id == null || id == 0)
            {
                // Crear una nueva categoria
                categoria.Activa = true; // Por defecto, una nueva categoria está activa
                return View(categoria);
            }
            else
            {
                // Editar una categoria existente
                categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
                if (categoria == null)
                {
                    return NotFound();
                }
                return View(categoria);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    // Crear una nueva categoria
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoria creada exitosamente";
                }
                else
                {
                    // Actualizar una categoria existente
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            // Si el modelo no es válido, volver a mostrar la vista con los datos ingresados
            TempData[DS.Error] = "Error al guardar la categoria. Por favor, revise los datos ingresados.";
            return View(categoria);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todosCategorias = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todosCategorias });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var categoria = await _unidadTrabajo.Categoria.Obtener(id);
            if (categoria == null)
            {
                return Json(new { success = false, message = "Error al borrar la categoria" });
            }
            await _unidadTrabajo.Categoria.Remover(categoria);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria borrada correctamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, long? id)
        {
            var categoria = await _unidadTrabajo.Categoria.ObtenerTodos();
            if (id == null)
            {
                // Validar si el nombre ya existe para una nueva categoria
                if (categoria.Any(b => b.Nombre.ToLower() == nombre.ToLower()))
                {
                    return Json(new {data = true});
                }
            }
            else
            {
                // Validar si el nombre ya existe para una categoria existente, excluyendo la actual
                if (categoria.Any(b => b.Nombre.ToLower() == nombre.ToLower() && b.Id != id))
                {
                    return Json(new {data = true});
                }
            }
            return Json(new {data = false});
        }

        #endregion
    }
}
