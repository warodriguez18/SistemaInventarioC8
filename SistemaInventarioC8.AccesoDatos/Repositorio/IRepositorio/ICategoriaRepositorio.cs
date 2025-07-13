using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio: IRepositorio<Modelos.Categoria>
    {
        void Actualizar(Modelos.Categoria categoria);
       
    }
}
