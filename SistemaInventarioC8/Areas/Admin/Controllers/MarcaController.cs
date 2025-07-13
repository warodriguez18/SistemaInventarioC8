using Microsoft.AspNetCore.Mvc;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioC8.Modelos;
using SistemaInventarioC8.Utilidades;

namespace SistemaInventarioC8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();
            if (id == null || id == 0)
            {
                // Crear una nueva marca
                marca.Activa = true; // Por defecto, una nueva marca está activa
                return View(marca);
            }
            else
            {
                // Editar una marca existente
                marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
                if (marca == null)
                {
                    return NotFound();
                }
                return View(marca);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    // Crear una nueva marca
                    await _unidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada exitosamente";
                }
                else
                {
                    // Actualizar una marca existente
                    _unidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            // Si el modelo no es válido, volver a mostrar la vista con los datos ingresados
            TempData[DS.Error] = "Error al guardar la marca. Por favor, revise los datos ingresados.";
            return View(marca);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todosMarcas = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todosMarcas });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var marca = await _unidadTrabajo.Marca.Obtener(id);
            if (marca == null)
            {
                return Json(new { success = false, message = "Error al borrar la marca" });
            }
            await _unidadTrabajo.Marca.Remover(marca);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca borrada correctamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, long? id)
        {
            var marca = await _unidadTrabajo.Marca.ObtenerTodos();
            if (id == null)
            {
                // Validar si el nombre ya existe para una nueva marca
                if (marca.Any(b => b.Nombre.ToLower() == nombre.ToLower()))
                {
                    return Json(new {data = true});
                }
            }
            else
            {
                // Validar si el nombre ya existe para una marca existente, excluyendo la actual
                if (marca.Any(b => b.Nombre.ToLower() == nombre.ToLower() && b.Id != id))
                {
                    return Json(new {data = true});
                }
            }
            return Json(new {data = false});
        }

        #endregion
    }
}
