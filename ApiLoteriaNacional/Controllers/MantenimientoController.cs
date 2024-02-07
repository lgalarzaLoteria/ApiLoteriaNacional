using ApiLoteriaNacional.Data;
using LoteriaNacionalDominio;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static LoteriaNacionalDominio.MantenimientoDTO;

namespace ApiLoteriaNacional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : Controller
    {
        private readonly MantenimientoData _mantenimiento;

        public MantenimientoController(MantenimientoData mantenimientoData)
        {
            _mantenimiento = mantenimientoData ?? throw new ArgumentNullException(nameof(mantenimientoData));
        }

        #region Secciones

        [HttpPost("MantenimientoObtenerSecciones")]
        public async Task<IActionResult> MantenimientoObtenerSecciones([FromBody] SeccionesDTO secciones)
        {
            return Ok(await _mantenimiento.mantenimientoSecciones(secciones));

        }

        [HttpPost("MantenimientoGrabarSecciones")]
        public async Task<IActionResult> MantenimientoGrabarSecciones(SeccionesDTO secciones)
        {
            return Ok(await _mantenimiento.mantenimientoGrabarSecciones(secciones));

        }

        [HttpPost("ObtenerSecciones")]
        public async Task<IActionResult> ObtenerSecciones()
        {
            return Ok(await _mantenimiento.obtenerSecciones());
        }

        [HttpPost("ObtenerSeccionesFormulario")]
        public async Task<IActionResult> ObtenerSeccionesFormulario()
        {
            return Ok(await _mantenimiento.obtenerSeccionesFormulario());
        }

        #endregion

        #region Preguntas

        [HttpPost("MantenimientoObtenerPreguntas")]
        public async Task<IActionResult> MantenimientoObtenerPreguntas([FromBody] PreguntasDTO preguntas)
        {
            return Ok(await _mantenimiento.mantenimientoPreguntas(preguntas));

        }

        [HttpPost("MantenimientoGrabarPreguntas")]
        public async Task<IActionResult> MantenimientoGrabarPreguntas(PreguntasDTO preguntas)
        {
            return Ok(await _mantenimiento.mantenimientoGrabarPreguntas(preguntas));

        }

        [HttpPost("ObtenerPreguntas")]
        public async Task<IActionResult> ObtenerPreguntas()
        {
            return Ok(await _mantenimiento.obtenerPreguntas());
        }
        #endregion

        #region Novedades

        [HttpPost("MantenimientoObtenerNovedades")]
        public async Task<IActionResult> MantenimientoObtenerNovedades([FromBody] NovedadesDTO novedades)
        {
            return Ok(await _mantenimiento.mantenimientoNovedades(novedades));

        }

        [HttpPost("MantenimientoGrabarNovedades")]
        public async Task<IActionResult> MantenimientoGrabarNovedades(NovedadesDTO novedades)
        {
            return Ok(await _mantenimiento.mantenimientoGrabarNovedades(novedades));

        }

        [HttpPost("ObtenerNovedades")]
        public async Task<IActionResult> ObtenerNovedades()
        {
            return Ok(await _mantenimiento.obtenerNovedades());
        }

        #endregion

        #region Aplicaciones
        [HttpPost("MantenimientoObtenerAplicaciones")]
        public async Task<IActionResult> MantenimientoObtenerAplicaciones([FromBody] AplicacionDTO aplicaciones)
        {
            return Ok(await _mantenimiento.mantenimientoAplicaciones(aplicaciones));

        }

        [HttpPost("MantenimientoGrabarAplicaciones")]
        public async Task<IActionResult> MantenimientoGrabarAplicaciones(AplicacionDTO aplicaciones)
        {
            return Ok(await _mantenimiento.mantenimientoGrabarAplicaciones(aplicaciones));

        }

        [HttpPost("ObtenerAplicaciones")]
        public async Task<IActionResult> ObtenerAplicaciones()
        {
            return Ok(await _mantenimiento.obtenerAplicaciones());
        }

        #endregion

        #region Procesos por Aplicacion

        #endregion
    }
}
