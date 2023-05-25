using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Cliente
    {
        #region Atributos
        private string _Accion;
        private int _IdCliente;
        private string _CodCliente;
        private string _Nombre;
        private string _Apellidos;
        private string _Direccion;
        private string _Telefono;
        private string _Correo;
        private string _Usuario;
        private string _Contrasena;
        private decimal _Credito;
        private decimal _CuentaActual;
        #endregion

        #region Constructor
        public E_Cliente(string accion, int idCliente, string codCliente, string nombre, string apellidos, string direccion, string telefono, string correo, string usuario, string contrasena, int credito, decimal cuentaActual)
        {
            _Accion = accion;
            _IdCliente = idCliente;
            _CodCliente = codCliente;
            _Nombre = nombre;
            _Apellidos = apellidos;
            _Direccion = direccion;
            _Telefono = telefono;
            _Correo = correo;
            _Usuario = usuario;
            _Contrasena = contrasena;
            _Credito = credito;
            _CuentaActual = cuentaActual;
        }

        public E_Cliente()
        {
            _Accion = string.Empty;
            _IdCliente = 0;
            _CodCliente = string.Empty;
            _Nombre = string.Empty;
            _Apellidos = string.Empty;
            _Direccion = string.Empty;
            _Telefono = string.Empty;
            _Correo = string.Empty;
            _Usuario = string.Empty;
            _Contrasena = string.Empty;
            _Credito = 0;
            _CuentaActual = 0;
        }
        #endregion

        #region Encapsulamiento
        public string Accion { get => _Accion; set => _Accion = value; }
        public int IdCliente { get => _IdCliente; set => _IdCliente = value; }
        public string CodCliente { get => _CodCliente; set => _CodCliente = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public string Direccion { get => _Direccion; set => _Direccion = value; }
        public string Telefono { get => _Telefono; set => _Telefono = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Contrasena { get => _Contrasena; set => _Contrasena = value; }
        public decimal Credito { get => _Credito; set => _Credito = value; }
        public decimal CuentaActual { get => _CuentaActual; set => _CuentaActual = value; }
        #endregion
    }
}
