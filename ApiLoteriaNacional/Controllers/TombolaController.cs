using ApiLoteriaNacional.Data;
using Microsoft.AspNetCore.Mvc;
using static LoteriaNacionalDominio.TombolaDTO;

namespace ApiLoteriaNacional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TombolaController : Controller
    {
        private readonly TombolaData _tombola;

        public TombolaController(TombolaData tombolaData)
        {
            _tombola = tombolaData ?? throw new ArgumentNullException(nameof(tombolaData));
        }
        #region DisenoPremioWebTombola

        [HttpGet("ObtenerDisenoPremioWebTombola")]
        public async Task<IActionResult> ObtenerDisenoPremioWebTombola()
        {
            return Ok(await _tombola.ObtenerDisenoPremioWebTombola());
        }
        
        [HttpGet("ObtenerDisenoPremioWebTombolaUltimaJugada")]
        public async Task<IActionResult> ObtenerDisenoPremioWebTombolaUltimaJugada()
        {
            return Ok(await _tombola.ObtenerDisenoPremioWebTombolaUltimaJugada());
        }

        [HttpPost("ObtenerDisenoPremioWebTombolaID")]
        public async Task<IActionResult> ObtenerDisenoPremioWebTombolaID(int id)
        {
            return Ok(await _tombola.ObtenerDisenoPremioWebTombolaID(id));
        }

        [HttpPost("ObtenerDisenoPremioWebTombolaIDpremio")]
        public async Task<IActionResult> ObtenerDisenoPremioWebTombolaIDpremio(int id)
        {
            return Ok(await _tombola.ObtenerDisenoPremioWebTombolaIDpremio(id));
        }

        [HttpPost("AgregarDisenoPremioWebTombola")]
        public async Task<IActionResult> AgregarDisenoPremioWebTombola(TbDisenoPremioWebTombolaDTO dato)
        {
            return Ok(await _tombola.AgregarDisenoPremioWebTombola(dato));
        }
        [HttpPost("ActualizarDisenoPremioWebTombola")]
        public async Task<IActionResult> ActualizarDisenoPremioWebTombola(TbDisenoPremioWebTombolaDTO dato)
        {
            return Ok(await _tombola.ActualizarDisenoPremioWebTombola(dato));
        }

        [HttpDelete("EliminarDisenoPremioWebTombola")]
        public async Task<IActionResult> EliminarDisenoPremioWebTombola(int id)
        {
            return Ok(await _tombola.EliminarDisenoPremioWebTombola(id));
        }
        #endregion

        #region DisenoWebTombola

        [HttpGet("ObtenerDisenoWebTombola")]
        public async Task<IActionResult> ObtenerDisenoWebTombola()
        {
            return Ok(await _tombola.ObtenerDisenoWebTombola());
        }


        [HttpPost("ObtenerrDisenoWebTombolaID")]
        public async Task<IActionResult> ObtenerrDisenoWebTombolaID(int id)
        {
            return Ok(await _tombola.ObtenerrDisenoWebTombolaID(id));
        }
        [HttpGet("ObtenerDisenoWebTombolaUltimaJugada")]
        public async Task<IActionResult> ObtenerDisenoWebTombolaUltimaJugada()
        {
            return Ok(await _tombola.ObtenerDisenoWebTombolaUltimaJugada());
        }
        [HttpPost("AgregarDisenoWebTombola")]
        public async Task<IActionResult> AgregarDisenoWebTombola(TbDisenoWebTombolaDTO dato)
        {
            return Ok(await _tombola.AgregarDisenoWebTombola(dato));
        }
        [HttpPost("ActualizarDisenoWebTombola")]
        public async Task<IActionResult> ActualizarDisenoWebTombola(TbDisenoWebTombolaDTO dato)
        {
            return Ok(await _tombola.ActualizarDisenoWebTombola(dato));
        }

        [HttpDelete("EliminarDisenoWebTombola")]
        public async Task<IActionResult> EliminarDisenoWebTombola(int id)
        {
            return Ok(await _tombola.EliminarDisenoWebTombola(id));
        }
        #endregion

        #region JuegoTombola

        [HttpGet("ObtenerJuegoTombola")]
        public async Task<IActionResult> ObtenerJuegoTombola()
        {
            return Ok(await _tombola.ObtenerJuegoTombola());
        }

        [HttpGet("ObtenerJuegoTombolaEnCurso")]
        public async Task<IActionResult> ObtenerJuegoTombolaEnCurso()
        {
            return Ok(await _tombola.ObtenerJuegoTombolaEnCurso());
        }

        [HttpPost("ObtenerJuegoTombolaID")]
        public async Task<IActionResult> ObtenerJuegoTombolaID(int id)
        {
            return Ok(await _tombola.ObtenerJuegoTombolaID(id));
        }
 
        [HttpPost("ActualizarEstadoTombolaPorID")]
        public async Task<IActionResult> ActualizarEstadoTombolaPorID(int id, string estadoTombola)
        {
            return Ok(await _tombola.ActualizarEstadoTombolaPorID(id,estadoTombola));
        }

        [HttpPost("AgregarJuegoTombola")]
        public async Task<IActionResult> AgregarJuegoTombola(TbJuegoWebTombolaDTO dato)
        {
            return Ok(await _tombola.AgregarJuegoTombola(dato));
        }
        [HttpPost("ActualizarJuegoTombola")]
        public async Task<IActionResult> ActualizarJuegoTombola(TbJuegoWebTombolaDTO dato)
        {
            return Ok(await _tombola.ActualizarJuegoTombola(dato));
        }

        [HttpDelete("EliminarJuegoTombola")]
        public async Task<IActionResult> EliminarJuegoTombola(int id)
        {
            return Ok(await _tombola.EliminarJuegoTombola(id));
        }

        #endregion

        #region PremioTombola

        [HttpGet("ObtenerPremios")]
        public async Task<IActionResult> ObtenerPremios()
        {
            return Ok(await _tombola.ObtenerPremios());
        }
        [HttpPost("ObtenerPremiosID")]
        public async Task<IActionResult> ObtenerPremiosID(int id)
        {
            return Ok(await _tombola.ObtenerPremiosID(id));
        }
        [HttpPost("AgregarPremio")]
        public async Task<IActionResult> AgregarPremio(TbPremioWebTombolaDTO dato)
        {
            return Ok(await _tombola.AgregarPremio(dato));
        }
        [HttpPost("ActualizarPremio")]
        public async Task<IActionResult> ActualizarPremio(TbPremioWebTombolaDTO dato)
        {
            return Ok(await _tombola.ActualizarPremio(dato));
        }

        [HttpDelete("EliminarPremio")]
        public async Task<IActionResult> EliminarPremio(int id)
        {
            return Ok(await _tombola.EliminarPremio(id));
        }

        #endregion
    }
}
