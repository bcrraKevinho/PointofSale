using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using StrDatosSQL;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Negocios
{
    public class N_Producto
    {
        readonly D_SQL_Datos sqlD = new D_SQL_Datos();
        E_Producto EP = new E_Producto();

        public string InsertaProducto(E_Producto pProducto)
        {
            pProducto.Accion = "INSERTAR";
            string R = sqlD.IBM_Entidad<E_Producto>("IBM_Producto", pProducto);
            if (R.Contains("Exito"))
                return "Exito: Los datos del producto se registraron en la Base de Datos";
            else
                return "Error: Los datos del producto NO se registraron en la Base de Datos";
        }

        public string BorraProducto(int pIdProducto, byte[] imagen)
        {
            E_Producto EP = new E_Producto();
            EP.Accion = "BORRAR";
            EP.IdProducto= pIdProducto;
            EP.ImagenProducto = imagen;
            string R = sqlD.IBM_Entidad<E_Producto>("IBM_Producto", EP);
            if (R.Contains("Exito"))
                return "Exito: Los datos del producto se borraron de la Base de Datos";
            else
                return "Error: Los datos del producto NO se borraron de la Base de Datos";
        }

        public string ModificaProducto(E_Producto pProducto, byte[] imagen)
        {
            pProducto.Accion = "MODIFICAR";
            pProducto.ImagenProducto = imagen;
            string R = sqlD.IBM_Entidad<E_Producto>("IBM_Producto", pProducto);
            if (R.Contains("Exito"))
                return "Exito: Los datos del producto se modificaron en la Base de Datos";
            else
                return "Error: Los datos del producto NO se modificaron en la Base de Datos";
        }


        public DataTable DT_LstProducto()
        {
            return sqlD.DT_ListadoGeneral("Producto", "CodProducto"); //Trae la info de la BD
        }

        public List<E_Producto> LstProducto()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Producto>(DT_LstProducto());
        }

        public E_Producto BuscaProducto(int pIdProducto) //Busqueda por ID (tipos int)
        {
            return (from Producto in LstProducto() where Producto.IdProducto == pIdProducto select Producto).FirstOrDefault();
        }
        public E_Producto BuscaProducto(string pCriterio) //Busqueda por Clave (tipos string)
        {
            return (from Producto in LstProducto() where Producto.CodProducto == pCriterio.ToUpper() select Producto).FirstOrDefault(); /*usamos programación LINQ*/
        }
        public List<E_Producto> LstProductoPorStock(int pStock) //Busqueda por Stock
        {
            return (from Producto in LstProducto()
                    where (Producto.Stock <= pStock)
                    select Producto).ToList();
        }
        public List<E_Producto> LstBuscaProducto(string pCriterio) //Busqueda por Criterio diverso (tipos string) devuelve listas
        {
            return (from Producto in LstProducto()
                    where Producto.CodProducto.Contains(pCriterio.ToUpper()) ||
                          Producto.Nombre.ToUpper().Contains(pCriterio.ToUpper()) ||
                          Producto.Descripcion.ToUpper().Contains(pCriterio.ToUpper()) ||
                          Producto.Precio == pCriterio
                    select Producto).ToList();
        }

        public DataTable ListadoInventario(int pstock) //Trae un listado de productos Parametro Stock
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@stock", pstock)
            };
            return sqlD.DT_GetListado("[dbo].ListarInventario", LstParametros);
        }

    }

    
}
