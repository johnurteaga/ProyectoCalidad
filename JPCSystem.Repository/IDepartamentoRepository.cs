using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface IDepartamentoRepository
    {
        IEnumerable<Departamento> GetDepartamentos();
        Departamento GetDepartamento(int id);
    }
}
