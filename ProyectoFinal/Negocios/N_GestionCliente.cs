using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using StrDatosSQL;
using System.Data;
using System.Data.Common;

namespace Negocios
{
    public class N_Cliente
    {
        readonly D_SQL_Datos sqlD = new D_SQL_Datos();
        E_Cliente EC = new E_Cliente();

        public string InsertaCliente(E_Cliente pCliente)
        {
            pCliente.Accion = "INSERTAR";
            string R = sqlD.IBM_Entidad<E_Cliente>("IBM_Cliente", pCliente);
            if (R.Contains("Exito"))
                return "Exito: Los datos del cliente se registraron en la Base de Datos";
            else
                return "Error: Los datos del cliente NO se registraron en la Base de Datos";
        }

        public string BorraCliente(int pIdCliente)
        {
            E_Cliente EC = new E_Cliente();
            EC.Accion = "BORRAR";
            EC.IdCliente = pIdCliente;

            string R = sqlD.IBM_Entidad<E_Cliente>("IBM_Cliente", EC);
            if (R.Contains("Exito"))
                return "Exito: Los datos del cliente se borraron de la Base de Datos";
            else
                return "Error: Los datos del cliente NO se borraron de la Base de Datos";
        }

        public string ModificaCliente(E_Cliente pCliente)
        {
            pCliente.Accion = "MODIFICAR";
            string R = sqlD.IBM_Entidad<E_Cliente>("IBM_Cliente", pCliente);
            if (R.Contains("Exito"))
                return "Exito: Los datos del cliente se modificaron en la Base de Datos";
            else
                return "Error: Los datos del cliente NO se modificaron en la Base de Datos";
        }
        public DataTable ListadoClientesAdeudo()
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            { };
            return sqlD.DT_GetListado("[dbo].InfoClienteAdeudo", LstParametros);
        }
        public DataTable ListadoClientes() //Listamos aquellos diferentes a ID 1
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            { };
            return sqlD.DT_GetListado("[dbo].ListadoClientes", LstParametros);
        }
        public List<E_Cliente> ListaCliente()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Cliente>(ListadoClientes());
        }
        public DataTable DT_LstCliente()
        {
            return sqlD.DT_ListadoGeneral("Cliente", "IdCliente"); //Trae la info de la BD 
        }
        public List<E_Cliente> LstCliente()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Cliente>(DT_LstCliente());
        }


        public E_Cliente BuscaCliente(int pIdCliente) //Busqueda por ID (tipos int)
        {
            return (from Cliente in LstCliente() where Cliente.IdCliente == pIdCliente select Cliente).FirstOrDefault();
        }
        public E_Cliente BuscaCliente (string pCriterio) //Busqueda por Cod (tipos string)
        {
            return (from Cliente in LstCliente() where (Cliente.CodCliente == pCriterio.ToUpper() || Cliente.Usuario.ToUpper().Contains(pCriterio.ToUpper())) && (Cliente.IdCliente != 1) select Cliente).FirstOrDefault(); /*usamos programación LINQ*/
        }
        public List<E_Cliente> LstBuscaCliente(string pCriterio) //Busqueda por Criterio diverso (tipos string) devuelve listas
        {
            return (from Cliente in LstCliente()
                    where (Cliente.CodCliente.Contains(pCriterio.ToUpper()) ||
                          Cliente.Nombre.ToUpper().Contains(pCriterio.ToUpper()) ||
                          Cliente.Apellidos.ToUpper().Contains(pCriterio.ToUpper()) ||
                          Cliente.Direccion.ToUpper().Contains(pCriterio.ToUpper()) ||
                          Cliente.Usuario.ToUpper().Contains(pCriterio.ToUpper())) && (Cliente.IdCliente != 1)
                    select Cliente).ToList();
        }
    }
}
