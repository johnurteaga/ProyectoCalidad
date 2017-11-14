using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using System.Data.Entity;

namespace JPCSystem.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private JpcSystemDbContext _context;

        public UsuarioRepository()
        {
            if (_context == null)
            {
                _context = new JpcSystemDbContext();
            }
        }

        public void AddUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void DeleteUsuario(int usuario)
        {
            var usr = _context.Usuarios.Find(usuario);

            if (usr != null)
            {
                _context.Usuarios.Remove(usr);
                _context.SaveChanges();
            }
        }

        public Usuario GetUsuario(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public IEnumerable<Usuario> GetUsuarios(string criterio)
        {
            var query = from u in _context.Usuarios
                        select u;
            return query;
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
