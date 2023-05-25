using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Presentacion.HANDLER
{
    /// <summary>
    /// Summary description for RecuperaArchivo
    /// </summary>
    public class RecuperaArchivo : IHttpHandler
    {

        public void ProcessRequest(HttpContext contexto)
        {
            var Id = contexto.Request.QueryString["IdProducto"];     //usamos var porque no sabemos el tipo de archivo que llegará
            int IdProducto;

            if (int.TryParse(Id, out IdProducto) == true)
            {
                SqlConnection Conexion = new SqlConnection("Data Source=LAPTOP-I02QSCQT\\SQLEXPRESS; Initial Catalog=NegocioV2; Integrated Security=true;");
                Conexion.Open();

                DataTable DT = new DataTable();
                SqlDataAdapter DA = new SqlDataAdapter("SELECT * FROM Producto WHERE IdProducto = " + IdProducto, Conexion);
                DA.Fill(DT);

                if (DT != null)
                {
                    contexto.Response.ContentType = "image/jpg";
                    Stream Str = new MemoryStream((byte[])DT.Rows[0]["ImagenProducto"]);
                    byte[] buffer = new byte[4096];
                    int byteSeq = Str.Read(buffer, 0, 4096);

                    while (byteSeq > 0)
                    {
                        contexto.Response.OutputStream.Write(buffer, 0, byteSeq); //
                        byteSeq = Str.Read(buffer, 0, 4096);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}