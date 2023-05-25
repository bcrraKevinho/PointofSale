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
    public class N_Ventas
    {
        readonly D_SQL_Datos sqlD = new D_SQL_Datos();
        

        public string InsertaVenta(E_Ventas pVenta)
        {
            pVenta.Accion = "INSERTAR";
            if (pVenta.IdCliente == 0)
            {
                pVenta.IdCliente = 1;
            }
            string R = sqlD.IBM_Entidad<E_Ventas>("IB_Venta", pVenta);
            if (R.Contains("Exito"))
                return "Exito: Los datos de la venta se registraron en la Base de Datos";
            else
                return "Error: Los datos de la venta NO se registraron en la Base de Datos";
        }

        public string BorraVenta(int pIdVenta)
        {
            E_Ventas EV = new E_Ventas();
            EV.Accion = "BORRAR";
            EV.IdVenta = pIdVenta;

            string R = sqlD.IBM_Entidad<E_Ventas>("IB_Venta", EV);
            if (R.Contains("Exito"))
                return "Exito: Los datos de la venta se borraron de la Base de Datos";
            else
                return "Error: Los datos de la venta NO se borraron de la Base de Datos";
        }

        //Devuelve ultima venta realizada {no importa si fue hecha a un cliente o no}
        public int IdUltimaVenta()
        {
            return sqlD.UltimoRegistroInsertado("IdVenta", "Venta");
        }

        //Imprimir una lista apartir de un stored procedure
        public DataTable DT_LstVenta()
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            { };
            return sqlD.DT_GetListado("[dbo].ListarVentas", LstParametros);
        }

        //Imprimir lista periodo de fechas
        public DataTable DT_LstVentaPeriodo(string inicio, string fin)
        {
            DateTime pinicio = Convert.ToDateTime(inicio);
            DateTime pfin = Convert.ToDateTime(fin);
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@inicio", pinicio),
                new SqlParameter("@fin", pfin)
            };
            return sqlD.DT_GetListado("[dbo].ListarPeriodo", LstParametros);
        }

        public DataTable DT_LstVentaCliente(int IdCliente, string inicio, string fin)
        {
            DateTime pinicio = Convert.ToDateTime(inicio);
            DateTime pfin = Convert.ToDateTime(fin);
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@Id",IdCliente),
                new SqlParameter("@inicio", pinicio),
                new SqlParameter("@fin", pfin)
            };
            return sqlD.DT_GetListado("[dbo].ListarComprasDeCliente", LstParametros);
        }

        public DataTable DT_LstVentaCliente(int IdCliente)
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@Id",IdCliente)
            };
            return sqlD.DT_GetListado("[dbo].ListarComprasClienteSinIntervalos", LstParametros);
        }

        public List<E_Ventas> LstVentasPorPeriodo(string inicio, string fin)
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Ventas>(DT_LstVentaPeriodo(inicio, fin));
        }

        public List<E_Ventas> LstVentasPorCliente(int IdCliente, string inicio, string fin)
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Ventas>(DT_LstVentaCliente(IdCliente, inicio, fin));
        }

        public List<E_Ventas> LstVentasPorCliente(int IdCliente)
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Ventas>(DT_LstVentaCliente(IdCliente));
        }

        public List<E_Ventas> LstVenta()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Ventas>(sqlD.DT_ListadoGeneral("Venta", "IdVenta"));
        }

        public E_Ventas BuscaVenta(string pCriterio) //Busqueda por Clave (tipos string)
        {
            return (from Venta in LstVenta() where Venta.CodVenta== pCriterio.ToUpper() select Venta).FirstOrDefault(); /*usamos programación LINQ*/

        }

        public E_Ventas BuscaVenta(int pIdVenta) //Busqueda por Clave (tipos string)
        {
            return (from Venta in LstVenta() where Venta.IdVenta == pIdVenta select Venta).FirstOrDefault(); /*usamos programación LINQ*/

        }
    }
}
