using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.MasterPages
{
    public partial class MP_Cliente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cliente"] == null)
            {
                Response.Redirect("../Gestion-SCV/Login.aspx");
            }
            if (!IsPostBack)
            {
                
            }
        }
        protected void Logo_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Cliente-Inicio.aspx?Nav=" + "Productos");
        }
        protected void Inicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Cliente-Inicio.aspx?Nav=" + "Productos");
        }
        protected void ViewProducto_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "GProductos");
            Response.Redirect("../Gestion-SCV/Mod_Cliente-Inicio.aspx?Nav=" + "Productos");
        }
       
        protected void ViewEstatus_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "GProductos");
            Response.Redirect("../Gestion-SCV/Mod_Cliente-Inicio.aspx?Nav=" + "Estatus");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Cliente-Inicio.aspx?Buscar=" + tbBuscarProducto.Text.ToUpper());
        }
        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Remove("Cliente");
            Response.Redirect("../Gestion-SCV/Login.aspx");
        }
    }
}