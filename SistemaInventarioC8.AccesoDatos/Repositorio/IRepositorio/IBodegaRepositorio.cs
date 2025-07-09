using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioC8.AccesoDatos.Repositorio.IRepositorio
{
    public interface IBodegaRepositorio: IRepositorio<Modelos.Bodega>
    {
        void Actualizar(Modelos.Bodega bodega);
       
    }
}
