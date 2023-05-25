using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Producto
    {
        #region Atributos
        private string _Accion;
        private int _IdProducto;
        private string _CodProducto;
        private string _Nombre;
        private string _Descripcion;
        private string _Precio;
        private int _Stock;
        private byte[] _ImagenProducto;
        #endregion


        #region Constructor
        public E_Producto(string accion, int idProducto, string codProducto, string nombre, string descripcion, string precio, int stock, byte[] imagen)
        {
            _Accion = accion;
            _IdProducto = idProducto;
            _CodProducto = codProducto;
            _Nombre = nombre;
            _Descripcion = descripcion;
            _Precio = precio;
            _Stock = stock;
            _ImagenProducto = imagen;
        }
        public E_Producto()
        {
            _Accion = string.Empty;
            _IdProducto = 0;
            _CodProducto = string.Empty;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
            _Precio = string.Empty;
            _Stock = 0;
            _ImagenProducto = null;
        }
        #endregion

        #region Encapsulamiento
        public string Accion { get => _Accion; set => _Accion = value; }
        public int IdProducto { get => _IdProducto; set => _IdProducto = value; }
        public string CodProducto { get => _CodProducto; set => _CodProducto = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string Precio { get => _Precio; set => _Precio = value; }
        public int Stock { get => _Stock; set => _Stock = value; }
        public byte[] ImagenProducto { get => _ImagenProducto; set => _ImagenProducto = value; }
        #endregion
    }
}
