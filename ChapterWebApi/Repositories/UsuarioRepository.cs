using ChapterWebApi.Contexts;
using ChapterWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChapterWebApi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ChapterContext _context;

        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }
        public List<Usuarios> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public Usuarios BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void Cadastrar(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);

            _context.SaveChanges();
        }

        public void Atualizar(int id, Usuarios usuario)
        {
            Usuarios usuarioBuscado = _context.Usuarios.Find(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;

                _context.Usuarios.Update(usuarioBuscado);

                _context.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            Usuarios usuarioBuscado = _context.Usuarios.Find(id);

            _context.Usuarios.Remove(usuarioBuscado);

            _context.SaveChanges();
        }
        public Usuarios Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}
