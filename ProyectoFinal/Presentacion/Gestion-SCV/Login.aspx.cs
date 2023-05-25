using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocios;
using Entidades;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace Presentacion.Gestion_SCV
{
    public partial class Login : System.Web.UI.Page
    {
        N_Usuario NU = new N_Usuario();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if(NU.LstValido(TbUsuario.Text, TbPassword.Text).Count > 0)
            {

                if (NU.LstValido(TbUsuario.Text, TbPassword.Text)[0].Tipo == 1)
                {
                    Session["Admin"] = TbUsuario.Text;
                    Response.Redirect("../Gestion-SCV/Mod_Admin-Inicio.aspx");
                }
                else if (NU.LstValido(TbUsuario.Text, TbPassword.Text)[0].Tipo == 2)
                {
                    Session["Cliente"] = TbUsuario.Text;
                    Response.Redirect("../Gestion-SCV/Mod_Cliente-Inicio.aspx?user=" + TbUsuario.Text);
                }
            }
            else
            {
                InicioEstatus.Attributes.Clear();
                InicioEstatus.Attributes.Add("class", "row-col-auto alert-danger text-center");
                lbRespuesta.Text = "Usuario o contraseña incorrectos";
            }
        }

        protected void BtnOlvidePass_Click(object sender, EventArgs e)
        {
            TbRemitente.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalCambioPass').modal();", true);
        }

        protected bool EnviarCorreo(string pAsunto, string pMensaje, string pOrigen)
        {
            bool Enviado = false;
            try  //para autocompletar try, damos doc veces tab
            {
                using (MemoryStream stream = new MemoryStream(new byte[64000]))
                {
                    MailMessage Email = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    Email.SubjectEncoding = Encoding.UTF8; //UTF8 nos permite utilizar caracteres ASCII, pero mas limitado que .ASCII
                    Email.BodyEncoding = Encoding.UTF8;
                    Email.From = new MailAddress(pOrigen, "Usuario Cliente"); //Address es el correo de donde se enviara el correo
                    Email.Subject = pAsunto;
                    Email.Body = pMensaje;
                    Email.To.Add("correo.pruebaschool@gmail.com"); //agregamos el destino


                    smtpServer.Port = 587; //puerto utilizado para servidor Gmail
                    smtpServer.Credentials = new NetworkCredential(pOrigen, TbPassCorreo.Text);
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(Email);

                    Enviado = true;
                }
            }
            catch (Exception)
            {
                Enviado = false;
            }

            return Enviado;
        }

        protected void btnConfirmarSolicitud_Click(object sender, EventArgs e)
        {
            EnviarCorreo(TbAsunto.Text, TbMensaje.Text, TbRemitente.Text); //se puede mandar así, pero es necesario que el usuario habilite la opción de envíos no seguros
        }
    }
}