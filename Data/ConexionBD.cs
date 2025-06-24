using Microsoft.Extensions.Options;

namespace Servidor.Data
{
    public static class ConexionBD
    {
        public static string Cadena { get; set; }

        // Este método se llamará al inicio de la app
        public static void Inicializar(IOptions<ConnectionOptions> options)
        {
            Cadena = options.Value.Cadena;
        }
    }
}
