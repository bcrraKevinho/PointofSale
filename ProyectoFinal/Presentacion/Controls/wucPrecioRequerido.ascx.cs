using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Controls
{
    public partial class wucPrecioRequerido : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string Text
        {
            get { return TbPrecioRequerido.Text.Trim(); }
            set { TbPrecioRequerido.Text = value; }
        }

        public bool Enable
        {
            set { TbPrecioRequerido.Enabled = value; }
        }
    }
}