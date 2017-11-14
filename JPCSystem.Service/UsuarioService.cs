using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;
using JPCSystem.Repository;

namespace JPCSystem.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _repository;

        //Nueva forma de realizar una conexion con 
        //el Repository aplicando dependencias 
        public UsuarioService(IUsuarioRepository usuarioService)
        {
            _repository = usuarioService;
        }

        public void AddUsuario(Usuario usuario)
        {
            _repository.AddUsuario(usuario);
        }

        public void DeleteUsuario(int usuario)
        {
            _repository.DeleteUsuario(usuario);
        }

        public Usuario GetUsuario(int id)
        {
            return _repository.GetUsuario(id);
        }

        public IEnumerable<Usuario> GetUsuarios(string criterio)
        {
            return _repository.GetUsuarios(criterio);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _repository.UpdateUsuario(usuario);
        }
    }
}
