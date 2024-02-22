using ApiLoteriaNacional.Data;
using LoteriaNacionalDominio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using static LoteriaNacionalDominio.MantenimientoDTO;
using static LoteriaNacionalDominio.SeguridadDTO;
using static LoteriaNacionalDominio.StoreCheckDTO;

namespace ApiLoteriaNacional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreCheckController : Controller
    {
        private readonly StoreCheckData _storeCheck;
        public StoreCheckController(StoreCheckData storeCheckData)
        {
            _storeCheck = storeCheckData ?? throw new ArgumentNullException(nameof(storeCheckData));
        }

        [HttpPost("LlenarFormulario")]
        public async Task<IActionResult> LlenarFormulario([FromBody] RegistroDTO data)
        {
            return Ok(await _storeCheck.LlenarFormulario(data));

        }

        [HttpPost("ActualizarFormulario")]
        public async Task<IActionResult> ActualizarFormulario([FromBody] RegistroDTO data)
        {
            return Ok(await _storeCheck.ActualizarFormulario(data));

        }

        [HttpPost("ConsultarFormularioIngresado")]
        public async Task<IActionResult> ConsultarFormularioIngresado([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormularioIngresado(data));

        }

        [HttpPost("ObtieneZonasPorSupervisor")]
        public async Task<IActionResult> ObtieneZonasPorSupervisor([FromBody] LoginDTO data)
        {
            return Ok(await _storeCheck.ObtieneZonasPorSupervisor(data));

        }

        [HttpPost("ConsultarFormulariosporPOS")]
        public async Task<IActionResult> ConsultarFormulariosporPOS([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormulariosporPOS(data));

        }

        [HttpPost("RevisarFormularioSupervisor")]
        public async Task<IActionResult> RevisarFormularioSupervisor([FromBody] RegistroDTO data)
        {
            return Ok(await _storeCheck.RevisarFormularioSupervisor(data));

        }

        [HttpPost("ConsultarFormularioRevisado")]
        public async Task<IActionResult> ConsultarFormularioRevisado([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormularioRevisado(data));

        }

        [HttpPost("ObtieneResumenGerencialZonas")]
        public async Task<IActionResult> ObtieneResumenGerencialZonas()
        {
            return Ok(await _storeCheck.ObtieneResumenGerencialZonas());

        }

        [HttpPost("ConsultarFormulariosRevisadosporPOS")]
        public async Task<IActionResult> ConsultarFormulariosRevisadosporPOS([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormulariosRevisadosporPOS(data));

        }

        [HttpPost("ConsultarFormularioConNovedades")]
        public async Task<IActionResult> ConsultarFormularioConNovedades([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormularioConNovedades(data));

        }

        [HttpPost("ObtieneRankingPDS")]
        public async Task<IActionResult> ObtieneRankingPDS()
        {
            return Ok(await _storeCheck.ObtieneRankingPDS());

        }

        [HttpPost("ObtienePDSPorRangoCumplimiento")]
        public async Task<IActionResult> ObtienePDSPorRangoCumplimiento([FromBody] RankingCumplimientoPDSDTO data)
        {
            return Ok(await _storeCheck.ObtienePDSPorRangoCumplimiento(data));

        }

        [HttpPost("ObtieneCalificacioCustionariosporPDS")]
        public async Task<IActionResult> ObtieneCalificacioCustionariosporPDS([FromBody] CalificacionCuestionariosPDSDTO data)
        {
            return Ok(await _storeCheck.ObtieneCalificacioCustionariosporPDS(data));

        }

        [HttpPost("ObtieneRankingPDSPorSupervisor")]
        public async Task<IActionResult> ObtieneRankingPDSPorSupervisor(ResumenGerencialZonasDTO dato)
        {
            return Ok(await _storeCheck.ObtieneRankingPDSPorSupervisor(dato));

        }

        [HttpPost("ObtieneFormulariosRevisadosPDSPorSupervisor")]
        public async Task<IActionResult> ObtieneFormulariosRevisadosPDSPorSupervisor([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ObtieneFormulariosRevisadosPDSPorSupervisor(data));

        }
        [HttpPost("ObtieneFormulariosRevisadosPDSPorJefe")]
        public async Task<IActionResult> ObtieneFormulariosRevisadosPDSPorJefe()
        {
            return Ok(await _storeCheck.ObtieneFormulariosRevisadosPDSPorJefe());

        }

    }
}
