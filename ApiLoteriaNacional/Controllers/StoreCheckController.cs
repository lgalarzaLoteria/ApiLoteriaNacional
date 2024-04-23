using ApiLoteriaNacional.Data;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static LoteriaNacionalDominio.SeguridadDTO;
using static LoteriaNacionalDominio.StoreCheckDTO;

namespace ApiLoteriaNacional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreCheckController : Controller
    {
        private readonly StoreCheckData _storeCheck;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoreCheckController(StoreCheckData storeCheckData, IWebHostEnvironment webHostEnvironment)
        {
            _storeCheck = storeCheckData ?? throw new ArgumentNullException(nameof(storeCheckData));
            _webHostEnvironment = webHostEnvironment;
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

        [HttpPost("ConsultarFormularioIngresadoSupervisor")]
        public async Task<IActionResult> ConsultarFormularioIngresadoSupervisor([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormularioIngresadoSupervisor(data));

        }

        [HttpPost("ObtieneZonasPorSupervisor")]
        public async Task<IActionResult> ObtieneZonasPorSupervisor([FromBody] LoginDTO data)
        {
            return Ok(await _storeCheck.ObtieneZonasPorSupervisor(data));

        }

        [HttpPost("ConsultarFormulariosporPOS")]
        public async Task<IActionResult> ConsultarFormulariosporPOS([FromBody] ZonasPorSupervisorDTO data)
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
        public async Task<IActionResult> ObtieneRankingPDS(RegistroFormularioDTO dato)
        {
            return Ok(await _storeCheck.ObtieneRankingPDS(dato));

        }

        [HttpPost("ObtienePDSPorRangoCumplimiento")]
        public async Task<IActionResult> ObtienePDSPorRangoCumplimiento([FromBody] RankingCumplimientoPDSDTO data)
        {
            return Ok(await _storeCheck.ObtienePDSPorRangoCumplimiento(data));

        }
        
        [HttpPost("ObtieneRankingPDSPorSupervisor")]
        public async Task<IActionResult> ObtieneRankingPDSPorSupervisor([FromBody] RegistroFormularioDTO dato)
        {
            return Ok(await _storeCheck.ObtieneRankingPDSPorSupervisor(dato));

        }

        [HttpPost("ObtieneRevisadosPorSupervisor")]
        public async Task<IActionResult> ObtieneRevisadosPorSupervisor([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ObtieneRevisadosPorSupervisor(data));

        }

        [HttpPost("ObtieneCalificacioCustionariosporPDS")]
        public async Task<IActionResult> ObtieneCalificacioCustionariosporPDS([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ObtieneCalificacioCustionariosporPDS(data));

        }


        [HttpPost("ObtieneZonasPorJefeComercial")]
        public async Task<IActionResult> ObtieneZonasPorJefeComercial([FromBody] LoginDTO data)
        {
            return Ok(await _storeCheck.ObtieneZonasPorJefeComercial(data));

        }

        [HttpPost("ObtieneRevisadosPorJefeComercial")]
        public async Task<IActionResult> ObtieneRevisadosPorJefeComercial([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ObtieneRevisadosPorJefeComercial(data));

        }

        [HttpPost("ObtieneRankingPDSPorJefeComercial")]
        public async Task<IActionResult> ObtieneRankingPDSPorJefeComercial([FromBody] RegistroFormularioDTO dato)
        {
            return Ok(await _storeCheck.ObtieneRankingPDSPorJefeComercial(dato));

        }

        [HttpPost("RevisarFormularioJefeComercial")]
        public async Task<IActionResult> RevisarFormularioJefeComercial([FromBody] RegistroDTO data)
        {
            return Ok(await _storeCheck.RevisarFormularioJefeComercial(data));

        }

        [HttpPost("ConsultarFormulariosporPOSJefeComercial")]
        public async Task<IActionResult> ConsultarFormulariosporPOSJefeComercial([FromBody] ZonasPorSupervisorDTO data)
        {
            return Ok(await _storeCheck.ConsultarFormulariosporPOSJefeComercial(data));

        }

        [HttpPost("ObtieneCalificacioCustionariosporPDSJefeComercial")]
        public async Task<IActionResult> ObtieneCalificacioCustionariosporPDSJefeComercial([FromBody] RegistroFormularioDTO data)
        {
            return Ok(await _storeCheck.ObtieneCalificacioCustionariosporPDSJefeComercial(data));

        }

        [HttpPost("ObtieneZonasPorGerencia")]
        public async Task<IActionResult> ObtieneZonasPorGerencia()
        {
            return Ok(await _storeCheck.ObtieneZonasPorGerencia());

        }

        [HttpPost("ObtieneDiasRetrasoRevisionSupervisor")]
        public async Task<IActionResult> ObtieneTiempoRevisonSupervisor()
        {
            return Ok(await _storeCheck.ObtieneDiasRetrasoRevision("J"));

        }

        [HttpPost("ObtieneDiasRetrasoRevisionJefeComercial")]
        public async Task<IActionResult> ObtieneTiempoRevisonJefeComercial()
        {
            return Ok(await _storeCheck.ObtieneDiasRetrasoRevision("G"));

        }

        [HttpPost("ObtieneInformeSupervisor")]
        public async Task<IActionResult> ObtieneInformeSupervisor([FromBody] FormulariosporPOSDTO formulario)
        {
            string base64String = string.Empty;
            var dsDatosInforme = await _storeCheck.ObtieneInformeSupervisor((long)formulario.codigoFormulario);
            var dsDatosEvidenciaInforme = await _storeCheck.ObtieneEvidenciaInformeSupervisor((long)formulario.codigoFormulario);
            string mimtype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\InformeSupervisor.rdlc";
            LocalReport informeSupervisor = new LocalReport(path);
            informeSupervisor.AddDataSource("dsInformeSupervisor", dsDatosInforme.Tables[0]);
            informeSupervisor.AddDataSource("dsEvidenciaInformeSupervisor", dsDatosEvidenciaInforme.Tables[0]);
            var result = informeSupervisor.Execute(RenderType.Pdf, extension, null, mimtype);
            base64String = Convert.ToBase64String(result.MainStream, 0, result.MainStream.Length);

            return Ok(base64String);

        }

        [HttpPost("ObtieneInformeJefeVentas")]
        public async Task<IActionResult> ObtieneInformeJefeVentas([FromBody] FormulariosporPOSDTO formulario)
        {
            string base64String = string.Empty;
            var dsDatosInforme = await _storeCheck.ObtieneInformeJefeVentas((long)formulario.codigoFormulario);
            var dsDatosEvidenciaInforme = await _storeCheck.ObtieneEvidenciaInformeJefeVentas((long)formulario.codigoFormulario);
            string mimtype = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\InformeJefeVentas.rdlc";
            LocalReport informeJefeVentas = new LocalReport(path);
            informeJefeVentas.AddDataSource("dsInformeJefeVentas", dsDatosInforme.Tables[0]);
            informeJefeVentas.AddDataSource("dsEvidenciaJefeVentas", dsDatosEvidenciaInforme.Tables[0]);
            var result = informeJefeVentas.Execute(RenderType.Pdf, extension, null, mimtype);
            base64String = Convert.ToBase64String(result.MainStream, 0, result.MainStream.Length);

            return Ok(base64String);

        }

    }
}
