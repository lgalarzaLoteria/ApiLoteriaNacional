using ApiLoteriaNacional.Data;
using Microsoft.AspNetCore.Mvc;
using static LoteriaNacionalDominio.ComprobanteExternoAdmDTO;

namespace ApiLoteriaNacional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprobanteController : Controller
    {
        private readonly ComprobanteData _ComproExtAdm;

        public ComprobanteController(ComprobanteData comprobanteData)
        {
            _ComproExtAdm = comprobanteData ?? throw new ArgumentNullException(nameof(comprobanteData));
        }
        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}
        [HttpGet("ObtenerComprobanteExternoAdm")]
        public async Task<IActionResult> ObtenerComprobanteExternoAdm()
        {
            return Ok(await _ComproExtAdm.ObtenerComprobanteExternoAdm());
        }

        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}
        [HttpGet("ConfirnarEnvioComprobantesExternos")]
        public async Task<IActionResult> ConfirnarEnvioComprobantesExternos(string IdEnvioTrama, bool TramaConfirmada)
        {
            return Ok(await _ComproExtAdm.ConfirnarEnvioComprobantesExternos(IdEnvioTrama, TramaConfirmada));
        }

    }
}
