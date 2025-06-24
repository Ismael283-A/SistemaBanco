using Servidor.Models;
using Servidor.Data;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Servidor.Service
{
    public class BancoService : IBancoService
    {
        public BancoService(IOptions<ConnectionOptions> options)
        {
            ConexionBD.Inicializar(options);
        }

        // ======================= CUENTAS =======================

        public bool CuentaExiste(string numeroCuenta)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT COUNT(*) FROM CUENTAS WHERE NUM_CUE = @num", con);
            cmd.Parameters.AddWithValue("@num", numeroCuenta);
            return (int)cmd.ExecuteScalar() > 0;
        }

        public decimal ObtenerSaldo(string numeroCuenta)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT SAL_CUE FROM CUENTAS WHERE NUM_CUE = @num", con);
            cmd.Parameters.AddWithValue("@num", numeroCuenta);
            var result = cmd.ExecuteScalar();
            return result == null ? 0 : Convert.ToDecimal(result);
        }

        public List<Cuenta> ObtenerTodasLasCuentas()
        {
            var cuentas = new List<Cuenta>();
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT NUM_CUE, TIPO_CUE, SAL_CUE, CED_CLI FROM CUENTAS", con);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cuentas.Add(new Cuenta
                {
                    Numero = reader.GetString(0),
                    Tipo = reader.GetString(1),
                    Saldo = reader.GetDecimal(2),
                    CedulaCliente = reader.GetString(3)
                });
            }
            return cuentas;
        }

        public void CrearCuenta(Cuenta c)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO CUENTAS (NUM_CUE, TIPO_CUE, SAL_CUE, CED_CLI) VALUES (@num, @tipo, @saldo, @ced)", con);
            cmd.Parameters.AddWithValue("@num", c.Numero);
            cmd.Parameters.AddWithValue("@tipo", c.Tipo);
            cmd.Parameters.AddWithValue("@saldo", c.Saldo);
            cmd.Parameters.AddWithValue("@ced", c.CedulaCliente);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarCuenta(string numero, Cuenta c)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("UPDATE CUENTAS SET TIPO_CUE=@tipo, SAL_CUE=@saldo, CED_CLI=@ced WHERE NUM_CUE=@num", con);
            cmd.Parameters.AddWithValue("@tipo", c.Tipo);
            cmd.Parameters.AddWithValue("@saldo", c.Saldo);
            cmd.Parameters.AddWithValue("@ced", c.CedulaCliente);
            cmd.Parameters.AddWithValue("@num", numero);
            cmd.ExecuteNonQuery();
        }

        public void EliminarCuenta(string numero)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM CUENTAS WHERE NUM_CUE=@num", con);
            cmd.Parameters.AddWithValue("@num", numero);
            cmd.ExecuteNonQuery();
        }

        // ======================= CLIENTES =======================

        public List<Cliente> ObtenerClientes()
        {
            var lista = new List<Cliente>();
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT CED_CLI, NOM_CLI, APE_CLI FROM CLIENTES", con);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Cedula = reader.GetString(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2)
                });
            }
            return lista;
        }

        public Cliente? ObtenerCliente(string cedula)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT CED_CLI, NOM_CLI, APE_CLI FROM CLIENTES WHERE CED_CLI = @ced", con);
            cmd.Parameters.AddWithValue("@ced", cedula);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
                {
                    Cedula = reader.GetString(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2)
                };
            }
            return null;
        }

        public void CrearCliente(Cliente cliente)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO CLIENTES (CED_CLI, NOM_CLI, APE_CLI) VALUES (@ced, @nom, @ape)", con);
            cmd.Parameters.AddWithValue("@ced", cliente.Cedula);
            cmd.Parameters.AddWithValue("@nom", cliente.Nombre);
            cmd.Parameters.AddWithValue("@ape", cliente.Apellido);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarCliente(string cedula, Cliente cliente)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("UPDATE CLIENTES SET NOM_CLI=@nom, APE_CLI=@ape WHERE CED_CLI=@ced", con);
            cmd.Parameters.AddWithValue("@nom", cliente.Nombre);
            cmd.Parameters.AddWithValue("@ape", cliente.Apellido);
            cmd.Parameters.AddWithValue("@ced", cedula);
            cmd.ExecuteNonQuery();
        }

        public void EliminarCliente(string cedula)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM CLIENTES WHERE CED_CLI=@ced", con);
            cmd.Parameters.AddWithValue("@ced", cedula);
            cmd.ExecuteNonQuery();
        }

        // ======================= TRANSFERENCIAS =======================

        public List<Transferencia> ObtenerTransferencias()
        {
            var lista = new List<Transferencia>();
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT NUM_TRA, FEC_TRA, VALOR_TRA, NUM_CUE_ORI, NUM_CUE_DES FROM TRANSFERENCIAS", con);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Transferencia
                {
                    Numero = reader.GetInt32(0),
                    Fecha = reader.GetDateTime(1),
                    Valor = reader.GetDecimal(2),
                    CuentaOrigen = reader.GetString(3),
                    CuentaDestino = reader.GetString(4)
                });
            }
            return lista;
        }

        public Transferencia? ObtenerTransferencia(int numero)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("SELECT NUM_TRA, FEC_TRA, VALOR_TRA, NUM_CUE_ORI, NUM_CUE_DES FROM TRANSFERENCIAS WHERE NUM_TRA = @num", con);
            cmd.Parameters.AddWithValue("@num", numero);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Transferencia
                {
                    Numero = reader.GetInt32(0),
                    Fecha = reader.GetDateTime(1),
                    Valor = reader.GetDecimal(2),
                    CuentaOrigen = reader.GetString(3),
                    CuentaDestino = reader.GetString(4)
                };
            }
            return null;
        }

        public void CrearTransferencia(Transferencia t)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO TRANSFERENCIAS (FEC_TRA, VALOR_TRA, NUM_CUE_ORI, NUM_CUE_DES) VALUES (@fec, @val, @ori, @des)", con);
            cmd.Parameters.AddWithValue("@fec", t.Fecha);
            cmd.Parameters.AddWithValue("@val", t.Valor);
            cmd.Parameters.AddWithValue("@ori", t.CuentaOrigen);
            cmd.Parameters.AddWithValue("@des", t.CuentaDestino);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarTransferencia(int numero, Transferencia t)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("UPDATE TRANSFERENCIAS SET FEC_TRA=@fec, VALOR_TRA=@val, NUM_CUE_ORI=@ori, NUM_CUE_DES=@des WHERE NUM_TRA=@num", con);
            cmd.Parameters.AddWithValue("@fec", t.Fecha);
            cmd.Parameters.AddWithValue("@val", t.Valor);
            cmd.Parameters.AddWithValue("@ori", t.CuentaOrigen);
            cmd.Parameters.AddWithValue("@des", t.CuentaDestino);
            cmd.Parameters.AddWithValue("@num", numero);
            cmd.ExecuteNonQuery();
        }

        public void EliminarTransferencia(int numero)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM TRANSFERENCIAS WHERE NUM_TRA=@num", con);
            cmd.Parameters.AddWithValue("@num", numero);
            cmd.ExecuteNonQuery();
        }

        // ======================= TRANSFERIR LÓGICO =======================

        public bool Transferir(Transferencia t)
        {
            using var con = new SqlConnection(ConexionBD.Cadena);
            con.Open();
            using var tran = con.BeginTransaction();
            try
            {
                var cmd1 = new SqlCommand("SELECT SAL_CUE FROM CUENTAS WHERE NUM_CUE=@ori", con, tran);
                cmd1.Parameters.AddWithValue("@ori", t.CuentaOrigen);
                var r1 = cmd1.ExecuteScalar();
                if (r1 == null) throw new Exception("Cuenta origen no existe.");
                decimal saldo = Convert.ToDecimal(r1);
                if (saldo < t.Valor) throw new Exception("Saldo insuficiente.");

                var cmd2 = new SqlCommand("UPDATE CUENTAS SET SAL_CUE=SAL_CUE - @val WHERE NUM_CUE=@ori", con, tran);
                cmd2.Parameters.AddWithValue("@val", t.Valor);
                cmd2.Parameters.AddWithValue("@ori", t.CuentaOrigen);
                cmd2.ExecuteNonQuery();

                var cmd3 = new SqlCommand("SELECT COUNT(*) FROM CUENTAS WHERE NUM_CUE=@des", con, tran);
                cmd3.Parameters.AddWithValue("@des", t.CuentaDestino);
                if ((int)cmd3.ExecuteScalar() == 0) throw new Exception("Cuenta destino no existe.");

                var cmd4 = new SqlCommand("UPDATE CUENTAS SET SAL_CUE=SAL_CUE + @val WHERE NUM_CUE=@des", con, tran);
                cmd4.Parameters.AddWithValue("@val", t.Valor);
                cmd4.Parameters.AddWithValue("@des", t.CuentaDestino);
                cmd4.ExecuteNonQuery();

                var cmd5 = new SqlCommand("INSERT INTO TRANSFERENCIAS (FEC_TRA, VALOR_TRA, NUM_CUE_ORI, NUM_CUE_DES) VALUES (@fec, @val, @ori, @des)", con, tran);
                cmd5.Parameters.AddWithValue("@fec", t.Fecha);
                cmd5.Parameters.AddWithValue("@val", t.Valor);
                cmd5.Parameters.AddWithValue("@ori", t.CuentaOrigen);
                cmd5.Parameters.AddWithValue("@des", t.CuentaDestino);
                cmd5.ExecuteNonQuery();

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
