using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_DetalleVenta
    {
        #region Atributos
        private string _Accion;
        private int _IdDetalleVenta;
        private int _IdVenta;
        private int _Producto;
        private int _Descripcion;
        private int _Cantidad;
        private int _PrecioUnidad;
        private string _ImporteTotal;
        #endregion

        #region Constructor
        public E_DetalleVenta(string accion, int idDetalleVenta, int idVenta, int producto, int descripcion , int cantidad, int precioUnidad, string importeTotal)
        {
            _Accion = accion;
            _IdDetalleVenta = idDetalleVenta;
            _IdVenta = idVenta;
            _Producto = producto;
            _Descripcion = descripcion;
            _Cantidad = cantidad;
            _PrecioUnidad = precioUnidad;
            _ImporteTotal = importeTotal;
        }
        public E_DetalleVenta()
        {
            _Accion = string.Empty;
            _IdDetalleVenta = 0;
            _IdVenta = 0;
            _Producto = 0;
            _Descripcion = 0;
            _Cantidad = 0;
            _PrecioUnidad = 0;
            _ImporteTotal = string.Empty;
        }
        #endregion

        #region Encapsulamiento
        public string Accion { get => _Accion; set => _Accion = value; }
        public int IdDetalleVenta { get => _IdDetalleVenta; set => _IdDetalleVenta = value; }
        public int IdVenta { get => _IdVenta; set => _IdVenta = value; }
        public int Producto { get => _Producto; set => _Producto = value; }
        public int Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public int Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public int PrecioUnidad { get => _PrecioUnidad; set => _PrecioUnidad = value; }
        public string ImporteTotal { get => _ImporteTotal; set => _ImporteTotal = value; }
        #endregion
    }
}
