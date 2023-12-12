using Spoticry.Domain.Conta.Ageggates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotiticry.Repository.ContaRepo
{
    public class UsuarioRepository
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public Usuario ObterUsuario(Guid id)
        {
            return usuarios.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Usuario usuario)
        {
            Usuario usuarioOld = ObterUsuario(usuario.Id);
            UsuarioRepository.usuarios.Remove(usuarioOld);
            UsuarioRepository.usuarios.Add(usuario);
        }

        public void SalvarUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            UsuarioRepository.usuarios.Add(usuario);
        }
    }
}
