using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio
{
    public interface IMarcaRepositorio: IRepositorio<Modelos.Marca>
    {
        void Actualizar(Modelos.Marca marca);
       
    }
}
