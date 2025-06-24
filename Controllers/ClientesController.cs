using Microsoft.AspNetCore.Mvc;
using Servidor.Models;
using Servidor.Service;

namespace Servidor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IBancoService _service;

        public ClientesController(IBancoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> GetAll() => _service.ObtenerClientes();

        [HttpGet("{cedula}")]
        public ActionResult<Cliente> Get(string cedula)
        {
            var cli = _service.ObtenerCliente(cedula);
            if (cli == null) return NotFound();
            return cli;
        }

        [HttpPost]
        public IActionResult Post(Cliente c)
        {
            _service.CrearCliente(c);
            return Ok();
        }

        [HttpPut("{cedula}")]
        public IActionResult Put(string cedula, Cliente c)
        {
            _service.ActualizarCliente(cedula, c);
            return Ok();
        }

        [HttpDelete("{cedula}")]
        public IActionResult Delete(string cedula)
        {
            _service.EliminarCliente(cedula);
            return Ok();
        }
    }
}
