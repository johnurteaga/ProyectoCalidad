using JPCSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPCSystem.Repository
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetUsuarios(string criterio);

        Usuario GetUsuario(Int32 id);

        void AddUsuario(Usuario usuario);

        void UpdateUsuario(Usuario usuario);

        void DeleteUsuario(Int32 usuario);
    }
}
