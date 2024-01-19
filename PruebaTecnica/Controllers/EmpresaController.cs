using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.ViewModels.Requests;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresa _empresa;

        public EmpresaController(IEmpresa empresa)
        {
            _empresa = empresa;
        }

        [HttpGet]
        public object ListEmpresas()
        {
            return _empresa.Get();
        }
        
        [HttpGet("{id}")]
        public object GetEmpresa(int id)
        {
            return _empresa.GetEmpresa(id);
        }

        [HttpPost]
        public object PostEmpresas(NuevaEmpresaRequest nuevaEmpresa)
        {
            return _empresa.Create(nuevaEmpresa);
        }

        [HttpPost("RegistrarEmpleado")]
        public object RegistrarEmpleado([FromBody] NuevoEmpleadoRequest nuevoEmpleado)
        {
            return _empresa.RegistrarEmpleado(nuevoEmpleado);
        }
    }
}
    