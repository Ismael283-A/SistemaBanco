// ✅ INTERFAZ ACTUALIZADA Y COMPLETA
using Servidor.Models;
public interface IBancoService
{
    // Cuentas
    bool CuentaExiste(string numeroCuenta);
    decimal ObtenerSaldo(string numeroCuenta);
    List<Cuenta> ObtenerTodasLasCuentas();
    void CrearCuenta(Cuenta c);
    void ActualizarCuenta(string numero, Cuenta c);
    void EliminarCuenta(string numero);

    // Clientes (Ahora en BD)
    List<Cliente> ObtenerClientes();
    Cliente? ObtenerCliente(string cedula);
    void CrearCliente(Cliente cliente);
    void ActualizarCliente(string cedula, Cliente cliente);
    void EliminarCliente(string cedula);

    // Transferencias (Ahora en BD)
    List<Transferencia> ObtenerTransferencias();
    Transferencia? ObtenerTransferencia(int numero);
    void CrearTransferencia(Transferencia transferencia);
    void ActualizarTransferencia(int numero, Transferencia transferencia);
    void EliminarTransferencia(int numero);

    // Transferencia directa lógica
    bool Transferir(Transferencia t);
}
