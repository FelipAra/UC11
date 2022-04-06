using ChapterWebApi.Models;
using ChapterWebApi.Repositories;
using ChapterWebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChapterWebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public LoginController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuarios UsuarioBuscado = _usuarioRepository.Login(login.email, login.senha);

                if(UsuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos");
                }
                else
                {
                    var minhasClains = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, UsuarioBuscado.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, UsuarioBuscado.Id.ToString()),
                        new Claim(ClaimTypes.Role, UsuarioBuscado.Tipo.ToString())
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticação"));

                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var meuToken = new JwtSecurityToken(
                        issuer: "chapter.webApi",
                        audience: "chapter.webApi",
                        claims: minhasClains,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: cred
                );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken),
                    });
                }
            }
            catch (System.Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
