using SistemaInventarioC8.AccesoDatos.Data;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Modelos.Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Modelos.Categoria categoria)
        {
            var categoriaDb = _db.Categorias.FirstOrDefault(b => b.Id == categoria.Id);
            if (categoriaDb != null)
            {
                categoriaDb.Nombre = categoria.Nombre;
                categoriaDb.Descripcion = categoria.Descripcion;
                categoriaDb.Activa = categoria.Activa;
                // No es necesario actualizar FechaModificacion, se maneja en el DbContext
                _db.SaveChanges(); // Guardar los cambios en la base de datos
            }

        }
    }
}
