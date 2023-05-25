using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using StrDatosSQL;
using System.Data;

//se agregaron despues
using System.Data.SqlClient;
using Datos;
using System.Data.Common;
//falta agregar Datos


namespace Negocios
{
   public class N_Usuario
    {
        readonly D_SQL_Datos sqlD = new D_SQL_Datos();
        E_Usuario EU = new E_Usuario();

        public string InsertaUsuario(E_Usuario pUsuario)
        {
            pUsuario.Accion = "INSERTAR";
            //pUsuario.Tipo = 2;
            string R = sqlD.IBM_Entidad<E_Usuario>("IBM_Usuario", pUsuario);
            if (R.Contains("Exito"))
                return "Exito: Los datos del usuario se registraron en la Base de Datos";
            else
                return "Error: Los datos del usuario NO se registraron en la Base de Datos";
        }

        public string BorraUsuario(int pIdUsuario)
        {
            E_Usuario EU = new E_Usuario();
            EU.Accion = "BORRAR";
            EU.IdUsuario= pIdUsuario;

            string R = sqlD.IBM_Entidad<E_Usuario>("IBM_Usuario", EU);
            if (R.Contains("Exito"))
                return "Exito: Los datos del usuario se borraron de la Base de Datos";
            else
                return "Error: Los datos del usuario NO se borraron de la Base de Datos";
        }

        public string ModificaUsuario(E_Usuario pUsuario)
        {
            pUsuario.Accion = "MODIFICAR";

            string R = sqlD.IBM_Entidad<E_Usuario>("IBM_Usuario", pUsuario);
            if (R.Contains("Exito"))
                return "Exito: Los datos del usuario se modificaron de la Base de Datos";
            else
                return "Error: Los datos del usuario NO se modificaron de la Base de Datos";
        }

        //Listar Usuarios
        public DataTable ListarUsuarios()
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            {
            };
            return sqlD.DT_GetListado("[dbo].[ListarUsuarios]", LstParametros);
        }
        public List<E_Usuario> LstUsuarios()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Usuario>(ListarUsuarios());
        }




        //Validar Usuarios
        public DataTable ValidarUsuario(string pusuario, string pcontrasena)
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@Usuario", pusuario),
                new SqlParameter("@Contrasena", pcontrasena)
            };
            return sqlD.DT_GetListado("[dbo].[ValidarUsuario]", LstParametros);
        }
        public List<E_Usuario> LstValido(string pusuario, string pcontrasena)
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Usuario>(ValidarUsuario(pusuario,pcontrasena));
        }
        
        
        
        
        //Imprimir una lista apartir de un stored procedure
        public DataTable ListadoUsuarios()
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            { };
            return sqlD.DT_GetListado("[dbo].ListarUsuarios", LstParametros);
        }
        public DataTable ListadoUsuariosPorID(string pnombre)
        {
            List<DbParameter> LstParametros = new List<DbParameter>
            {
                new SqlParameter("@nombre", pnombre)
            };
            return sqlD.DT_GetListado("[dbo].UsuarioPorID", LstParametros);
        }
        public DataTable DT_LstUsuario()
        {
            return sqlD.DT_ListadoGeneral("Usuario", "Tipo"); //Trae la info de la BD 
        }
        public List<E_Usuario> LstUsuario()
        {
            return StrDatosSQL.D_ConvierteDatos.ConvertirDTALista<E_Usuario>(DT_LstUsuario());
        }
        public E_Usuario BuscaUsuario(int pIdUsuario) //Busqueda por ID (tipos int)
        {
            return (from Usuario in LstUsuario() where Usuario.IdUsuario == pIdUsuario select Usuario).FirstOrDefault();
        }
        public E_Usuario BuscaUsuario(string pNombreUsuario) //Busqueda por ID (tipos int)
        {
            return (from Usuario in LstUsuario() where Usuario.NombreUsuario == pNombreUsuario select Usuario).FirstOrDefault();
        }
        public List<E_Usuario> LstBuscaUsuario(string pIdUsuario) //Busqueda por Criterio diverso (tipos string) devuelve listas
        {
            return (from Usuario in LstUsuario()
                    where (Usuario.NombreUsuario.ToUpper().Contains(pIdUsuario.ToUpper()))
                    select Usuario).ToList();
        }
    }
}
