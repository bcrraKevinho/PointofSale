using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Controls
{
    public partial class wucAlfaNumericoRequerido : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string Text
        {
            get { return TbAlfanumericoRequerido.Text.Trim(); }
            set { TbAlfanumericoRequerido.Text = value; }
        }

        public bool Enable
        {
            set { TbAlfanumericoRequerido.Enabled = value; }
        }
    }
}