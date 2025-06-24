using Microsoft.AspNetCore.Mvc;
using Servidor.Models;
using Servidor.Service;

namespace Servidor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly IBancoService _service;

        public TransferenciasController(IBancoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Transferencia>> GetAll() => _service.ObtenerTransferencias();

        [HttpPost("Transferir")]
        public IActionResult Transferir(Transferencia t)
        {
            try
            {
                if (_service.Transferir(t))
                    return Ok();
                return BadRequest("Transferencia fallida.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Transferencia t)
        {
            _service.CrearTransferencia(t);
            return Ok();
        }

        [HttpDelete("{numero}")]
        public IActionResult Delete(int numero)
        {
            _service.EliminarTransferencia(numero);
            return Ok();
        }
    }
}
