using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.ViewModels.Requests;

namespace PruebaTecnica.Interfaces
{
    public interface IEmpresa
    {
        object Get();
        object GetEmpresa(int id);
        object Create(NuevaEmpresaRequest nuevaEmpresa);
        object RegistrarEmpleado(NuevoEmpleadoRequest nuevoEmpleado);
        object Update();
    }
}
