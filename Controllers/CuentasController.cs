using Microsoft.AspNetCore.Mvc;
using Servidor.Models;
using Servidor.Service;
using System;

namespace SistemaBanco.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly IBancoService _banco;

        public CuentasController(IBancoService banco)
        {
            _banco = banco;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_banco.ObtenerTodasLasCuentas());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{numero}")]
        public IActionResult GetByNumero(string numero)
        {
            try
            {
                if (!_banco.CuentaExiste(numero))
                    return NotFound("Cuenta no existe.");
                var cuenta = _banco.ObtenerTodasLasCuentas()
                             .Find(c => c.Numero == numero);
                return Ok(cuenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{numero}/saldo")]
        public IActionResult GetSaldo(string numero)
        {
            try
            {
                if (!_banco.CuentaExiste(numero))
                    return NotFound("Cuenta no existe.");
                return Ok(new { saldo = _banco.ObtenerSaldo(numero) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cuenta c)
        {
            try
            {
                if (_banco.CuentaExiste(c.Numero))
                    return Conflict("Cuenta ya existe.");
                _banco.CrearCuenta(c);
                return CreatedAtAction(nameof(GetByNumero), new { numero = c.Numero }, c);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{numero}")]
        public IActionResult Update(string numero, [FromBody] Cuenta c)
        {
            try
            {
                if (!_banco.CuentaExiste(numero))
                    return NotFound("Cuenta no existe.");
                _banco.ActualizarCuenta(numero, c);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{numero}")]
        public IActionResult Delete(string numero)
        {
            try
            {
                if (!_banco.CuentaExiste(numero))
                    return NotFound("Cuenta no existe.");
                _banco.EliminarCuenta(numero);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
