using Microsoft.AspNetCore.Mvc;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioC8.Modelos;
using SistemaInventarioC8.Utilidades;

namespace SistemaInventarioC8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Bodega bodega = new Bodega();
            if (id == null || id == 0)
            {
                // Crear una nueva bodega
                bodega.Activa = true; // Por defecto, una nueva bodega está activa
                return View(bodega);
            }
            else
            {
                // Editar una bodega existente
                bodega = await _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
                if (bodega == null)
                {
                    return NotFound();
                }
                return View(bodega);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                if (bodega.Id == 0)
                {
                    // Crear una nueva bodega
                    await _unidadTrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "Bodega creada exitosamente";
                }
                else
                {
                    // Actualizar una bodega existente
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "Bodega actualizada exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            // Si el modelo no es válido, volver a mostrar la vista con los datos ingresados
            TempData[DS.Error] = "Error al guardar la bodega. Por favor, revise los datos ingresados.";
            return View(bodega);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todosBodegas = await _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todosBodegas });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var bodega = await _unidadTrabajo.Bodega.Obtener(id);
            if (bodega == null)
            {
                return Json(new { success = false, message = "Error al borrar la bodega" });
            }
            await _unidadTrabajo.Bodega.Remover(bodega);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega borrada correctamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, long? id)
        {
            var bodega = await _unidadTrabajo.Bodega.ObtenerTodos();
            if (id == null)
            {
                // Validar si el nombre ya existe para una nueva bodega
                if (bodega.Any(b => b.Nombre.ToLower() == nombre.ToLower()))
                {
                    return Json(new {data = true});
                }
            }
            else
            {
                // Validar si el nombre ya existe para una bodega existente, excluyendo la actual
                if (bodega.Any(b => b.Nombre.ToLower() == nombre.ToLower() && b.Id != id))
                {
                    return Json(new {data = true});
                }
            }
            return Json(new {data = false});
        }

        #endregion
    }
}
