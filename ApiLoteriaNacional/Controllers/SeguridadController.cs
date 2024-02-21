using ApiLoteriaNacional.Data;
using LoteriaNacionalDominio;
using Microsoft.AspNetCore.Mvc;
using static LoteriaNacionalDominio.SeguridadDTO;


namespace ApiLoteriaNacional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly SeguridadData _seguridad;

        public SeguridadController(SeguridadData seguridadData)
        {
            _seguridad = seguridadData ?? throw new ArgumentNullException(nameof(seguridadData));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            return Ok(await _seguridad.LoginActiveDirectory(login));
           

        }

        [HttpPost("ObtieneMenuUsuario")]
        public async Task<IActionResult> ObtieneMenuUsuario(LoginDTO login)
        {
            return Ok(await _seguridad.obtieneMenuUsuario(login.UserName));
            
        }
        [HttpPost("ObtieneRolUsuario")]
        public async Task<IActionResult> ObtieneRolUsuario(LoginDTO login)
        {
            return Ok(await _seguridad.obtieneRolUsuario(login.UserName));

        }
    }
}
