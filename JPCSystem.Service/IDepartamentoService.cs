using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Service
{
    public interface IDepartamentoService
    {
        IEnumerable<Departamento> GetDepartamentos();
        Departamento GetDepartamento(int id);
    }
}
