using SistemaInventarioC8.AccesoDatos.Data;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Modelos.Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Modelos.Bodega bodega)
        {
            var bodegaDb = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
            if (bodegaDb != null)
            {
                bodegaDb.Nombre = bodega.Nombre;
                bodegaDb.Descripcion = bodega.Descripcion;
                bodegaDb.Direccion = bodega.Direccion;
                bodegaDb.Activa = bodega.Activa;
                // No es necesario actualizar FechaModificacion, se maneja en el DbContext
                _db.SaveChanges(); // Guardar los cambios en la base de datos
            }

        }
    }
}
