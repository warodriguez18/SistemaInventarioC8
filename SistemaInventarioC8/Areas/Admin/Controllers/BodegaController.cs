using Microsoft.AspNetCore.Mvc;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;

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

        #region 
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todosBodegas = await _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todosBodegas });
        }

        #endregion
    }
}
