using SistemaInventarioC8.AccesoDatos.Data;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio
{
    public class MarcaRepositorio : Repositorio<Modelos.Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Modelos.Marca marca)
        {
            var marcaDb = _db.Marcas.FirstOrDefault(b => b.Id == marca.Id);
            if (marcaDb != null)
            {
                marcaDb.Nombre = marca.Nombre;
                marcaDb.Descripcion = marca.Descripcion;
                marcaDb.Activa = marca.Activa;
                // No es necesario actualizar FechaModificacion, se maneja en el DbContext
                _db.SaveChanges(); // Guardar los cambios en la base de datos
            }
        }
    }
}
