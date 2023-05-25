using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Ventas
    {
        #region Atributos
        private string _Accion;
        private int _IdVenta;
        private string _CodVenta;
        //private DateTime _Fecha;
        private int _IdCliente;
        private int _CantidadProducto;
        private string _Total;
        #endregion

        #region Constructor
        public E_Ventas(string accion, int idVenta, string codVenta, /*DateTime fecha,*/ int idCliente, int cantidadProducto, string total, List<E_DetalleVenta> detallesDeVenta)
        {
            _Accion = accion;
            _IdVenta = idVenta;
            _CodVenta = codVenta;
           // _Fecha = fecha;
            _IdCliente = idCliente;
            _CantidadProducto = cantidadProducto;
            _Total = total;
        }

        public E_Ventas()
        {
            _Accion = string.Empty;
            _IdVenta = 0;
            _CodVenta = string.Empty;
            //_Fecha = DateTime.Today;
            _IdCliente = 0;
            _CantidadProducto = 0;
            _Total = string.Empty;
        }
        #endregion

        #region Encapsulamiento
        public string Accion { get => _Accion; set => _Accion = value; }
        public int IdVenta { get => _IdVenta; set => _IdVenta = value; }
        public string CodVenta { get => _CodVenta; set => _CodVenta = value; }
        //public DateTime Fecha { get => _Fecha; set => _Fecha = value; }
        public int IdCliente { get => _IdCliente; set => _IdCliente = value; }
        public int CantidadProducto { get => _CantidadProducto; set => _CantidadProducto = value; }
        public string Total { get => _Total; set => _Total = value; }
        #endregion
    }
}
