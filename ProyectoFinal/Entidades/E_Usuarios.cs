using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Usuario
    {
        #region Atributos
        private string _Accion;
        private int _IdUsuario;
        private int _Tipo;
        private string _NombreUsuario;
        private string _ContrasenaUsuario;
        #endregion



        #region Constructores
        public E_Usuario(string accion, int idUsuario, int tipo, string nombreUsuario, string contrasenaUsuario)
        {
            _Accion = accion;
            _IdUsuario = idUsuario;
            _Tipo = tipo;
            _NombreUsuario = nombreUsuario;
            _ContrasenaUsuario = contrasenaUsuario;
        }
        public E_Usuario()
        {
            _Accion = string.Empty;
            _IdUsuario = 0;
            _Tipo = 0;
            _NombreUsuario = string.Empty;
            _ContrasenaUsuario = string.Empty;
        }
        #endregion


        #region
        public string Accion { get => _Accion; set => _Accion = value; }
        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        public int Tipo { get => _Tipo; set => _Tipo = value; }
        public string NombreUsuario { get => _NombreUsuario; set => _NombreUsuario = value; }
        public string ContrasenaUsuario { get => _ContrasenaUsuario; set => _ContrasenaUsuario = value; }

        #endregion
    }
}
