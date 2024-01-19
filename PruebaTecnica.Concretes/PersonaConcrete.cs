using PruebaTecnica.Concretes.Contexts;
using PruebaTecnica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PruebaTecnica.Concretes
{
    public class PersonaConcrete : IPersona
    {
        private readonly PruebDbContext _context;
        public PersonaConcrete(PruebDbContext context)
        {
            _context = context;
        }

        public object Get()
        {
           return _context.Personas.ToList();
        }
    }
}
