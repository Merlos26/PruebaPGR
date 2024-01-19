using System;
using System.Net;
using ServiceStack;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.Concretes.Contexts;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

using PruebaTecnica.ViewModels.Requests;
using PruebaTecnica.ViewModels.Responses;
using Microsoft.Extensions.Logging;


namespace PruebaTecnica.Concretes
{
    public class EmpresaConcrete : IEmpresa
    {
        private readonly PruebDbContext _context;

        public EmpresaConcrete(PruebDbContext context)
        {
            _context = context;
        }

        public object Create(NuevaEmpresaRequest nuevaEmpresa)
        {
            var empresa = new Empresa()
            {
                Nombre = nuevaEmpresa.Nombre,
                Direccion = nuevaEmpresa.Direccion,
                Telefono = nuevaEmpresa.Telefono
            };

            try
            {

                _context.Empresas.Add(empresa);

                var empresaGuardada = _context.SaveChanges() > 0;

                if (!empresaGuardada)
                {
                    return new HttpError(HttpStatusCode.InternalServerError, "Ha ocurrido un error al guardar la empresa");
                }


                return new HttpResult(HttpStatusCode.OK, "Empresa Creada correctamente");

            }
            catch (Exception ex)
            {
                return new HttpError(HttpStatusCode.InternalServerError, "Ha ocurrido un error al guardar la empresa");
            }
        }

        public object Get()
        {
            return _context.Empresas.ToList();
        }

        public object GetEmpresa(int id)
        {
            var empresaEncontrada = _context.Empresas.Where(e => e.Id == id).FirstOrDefault();
            if (empresaEncontrada == null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "El empleado no existe");
            }

            return new HttpResult(empresaEncontrada);
        }

        public object Update()
        {
            throw new NotImplementedException();
        }

        public object RegistrarEmpleado(NuevoEmpleadoRequest nuevoEmpleadoReq)
        {
            var empresaExiste = _context.Empresas.Any(e => e.Id == nuevoEmpleadoReq.IdEmpresa);

            if (!empresaExiste)
            {
                return new HttpError(HttpStatusCode.BadRequest, "La empresa no existe");
            }
            var personaExiste = _context.Personas.Any(p => p.Id == nuevoEmpleadoReq.IdPersona);

            if (!personaExiste)
            {
                return new HttpError(HttpStatusCode.BadRequest, "La persona no existe");
            }

            var yaRegistrado = _context.PersonaEmpresas.Any(pe => 
            (pe.IdEmpresa == nuevoEmpleadoReq.IdEmpresa && pe.IdPersona == nuevoEmpleadoReq.IdPersona));

            if (yaRegistrado)
            {
                return new HttpError(HttpStatusCode.BadRequest, "El empleado ya está registrado en esa empresa");
            }

            var nuevoEmpleado = new PersonaEmpresa
            {
                IdEmpresa = nuevoEmpleadoReq.IdEmpresa,
                IdPersona = nuevoEmpleadoReq.IdPersona,
                FechaContrato = nuevoEmpleadoReq.fechaContrato,
                FechaFinContrato = nuevoEmpleadoReq.fechaFinContrato
            };            

            try
            {

                _context.PersonaEmpresas.Add(nuevoEmpleado);

                _context.SaveChanges();


                return new HttpResult(HttpStatusCode.OK, "Emplado registrado con éxito");

            } catch (Exception ex)
            {
                return new HttpError(HttpStatusCode.InternalServerError, $"Ha ocurrido un error inesperado {ex}");
            }


        }
    }
}
