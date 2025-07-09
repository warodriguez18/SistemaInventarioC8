using Microsoft.EntityFrameworkCore;
using SistemaInventarioC8.AccesoDatos.Data;
using SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioC8.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        //public async Task Actualizar(T entidad)
        //{
        //    await Task.Run(() => 
        //    {
        //        _db.Entry(entidad).State = EntityState.Modified; //Actualizar el estado de la entidad a modificada
        //    });
        //}

        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); //Insertar una nueva entidad en la base de datos
        }

        public async Task<T> Obtener(long id)
        {
            return await dbSet.FindAsync(id); //Buscar una entidad por su ID
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); //Aplicar el filtro si se proporciona
            }
            if (!isTracking)
            {
                query = query.AsNoTracking(); //Deshabilitar el seguimiento de cambios si es necesario
            }
            if (!string.IsNullOrEmpty(incluirPropiedades))
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad); //Incluir propiedades relacionadas
                }
            }

            return await query.FirstOrDefaultAsync(); //Devolver el primer resultado que coincida con el filtro
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordenamiento = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); //Aplicar el filtro si se proporciona
            }
            if (!isTracking)
            {
                query = query.AsNoTracking(); //Deshabilitar el seguimiento de cambios si es necesario
            }
            if (!string.IsNullOrEmpty(incluirPropiedades))
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad); //Incluir propiedades relacionadas
                }
            }
            if (ordenamiento != null)
            {
                query = ordenamiento(query); //Aplicar el ordenamiento si se proporciona
            }
            return await query.ToListAsync(); //Devolver todos los resultados
        }

        public async Task Remover(long id)
        {
            await Task.Run(() => 
            {
                T entidad = dbSet.Find(id); //Buscar la entidad por su ID
                if (entidad != null)
                {
                    dbSet.Remove(entidad); //Eliminar la entidad de la base de datos
                }
            });
        }

        public async Task RemoverRango(IEnumerable<T> entidad)
        {
            await Task.Run(() => 
            {
                dbSet.RemoveRange(entidad); //Eliminar un rango de entidades de la base de datos
            });
        }
    }
}
