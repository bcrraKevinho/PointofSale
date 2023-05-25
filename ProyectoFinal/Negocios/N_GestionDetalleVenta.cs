using StrDatosSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;


using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Negocios
{
    public class N_DetalleVenta
    {
        readonly D_SQL_Datos sqlD = new D_SQL_Datos();

        public string InsertaDetalleVenta(E_DetalleVenta pDetalleVenta)
        {
            pDetalleVenta.Accion = "INSERTAR";
            string R = sqlD.IBM_Entidad<E_DetalleVenta>("IB_DetalleVenta", pDetalleVenta);
            if (R.Contains("Exito"))
                return "Exito: Los datos del detalle de venta se registraron en la Base de Datos";
            else
                return "Error: Los datos del detalle de venta NO se registraron en la Base de Datos";
        }

        public string BorraDetalleVenta(int pIdDetalleVenta)
        {
            E_DetalleVenta EDV = new E_DetalleVenta();
            EDV.Accion = "BORRAR";
            EDV.IdDetalleVenta = pIdDetalleVenta;

            string R = sqlD.IBM_Entidad<E_DetalleVenta>("IB_DetalleVenta", EDV);
            if (R.Contains("Exito"))
                return "Exito: Los datos del detalle de venta se registraron en la Base de Datos";
            else
                return "Error: Los datos del detalle de venta NO se registraron en la Base de Datos";
        }

        //Imprimir una lista apartir de un stored procedure
        public DataTable DT_LstDetalleVenta()
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            { };
            return sqlD.DT_GetListado("[dbo].ListarDetalleVenta", LstParametros);
        }

        public DataTable DT_LstDetalleVentaPorId(int pId) //imprimir SOLO LOS REGISTROS DE IDVENTA ESPECIFICOS
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@id", pId)
            };
            return sqlD.DT_GetListado("[dbo].ListarDetallePor", LstParametros);
        }

        public List<E_DetalleVenta> LstDetallePorId(int pId)
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_DetalleVenta>(DT_LstDetalleVentaPorId(pId));
        }

        public DataTable DT_LstDetalle()
        {
            return sqlD.DT_ListadoGeneral("DetalleVenta", "IdDetalleVenta"); //Trae la info de la BD 
        }
        public List<E_DetalleVenta> LstDetalle()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_DetalleVenta>(DT_LstDetalle());
        }
        //public E_DetalleVenta BuscaDetalle(int pId) //Busqueda por ID (tipos int)
        //{
        //    return (from DetalleVenta in LstDetalle() where DetalleVenta.IdVenta == pId select DetalleVenta).FirstOrDefault();
        //}

        public List<E_DetalleVenta> LstBuscaDetalle (int pId) //Busqueda por Criterio diverso (tipos string) devuelve listas
        {
            return (from DetalleVenta in LstDetalle()
                    where (DetalleVenta.IdVenta == pId) select DetalleVenta).ToList();
        }
    }
}
