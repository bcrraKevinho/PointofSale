using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.MasterPages
{
    public partial class MP_Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("../Gestion-SCV/Login.aspx");
            }
        }
        protected void Logo_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "Inicio");
        }
        protected void Inicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "Inicio");
        }

        protected void GProducto_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "GProductos");
            Response.Redirect("../Gestion-SCV/Mod_Admin-Productos.aspx?Nav=" + "GProductos");
        }

        protected void GCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "GClientes");
        }

        protected void Ventas_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx?Nav=" + "Ventas");
        }

        protected void Inventario_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Gestion-SCV/Mod_Admin-Productos.aspx?Nav=" + "Inventario");
        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Remove("Admin");
            Response.Redirect("../Gestion-SCV/Login.aspx");
        }
    }
}