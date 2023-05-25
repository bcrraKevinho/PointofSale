using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocios;
using Entidades;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Presentacion.Gestion_SCV
{
    public partial class Mod_Admin_Productos : System.Web.UI.Page
    {
        N_Producto NP = new N_Producto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Nav();
            }
        }

        protected void Nav()
        {
            if (Request.QueryString["Nav"] != null)
            {
                hfNav.Value = Request.QueryString["Nav"];
            }
            switch (hfNav.Value)
            {
                case "GProductos":
                    ON_GProductos();
                    break;
                case "Inventario":
                    ON_Inventario();
                    break;
            }  
        }

        protected void ON_GProductos()
        {
            pAgregarNuevoProducto.Visible = false;
            pRegInventario.Visible = false;
            visualizarGrvProducto();
            pRegProductos.Visible = true;
        }

        protected void ON_Inventario()
        {
            pAgregarNuevoProducto.Visible = false;
            pRegProductos.Visible = false;
            visualizarGrvInventario(Convert.ToInt32(hfInventarioMin.Value));
            pRegInventario.Visible = true;
        }

        #region Visualizar Grv
        protected void visualizarGrvProducto()
        {
            GrvProducto.DataSource = NP.LstProducto();
            GrvProducto.DataBind(); 
        }
        protected void visualizarGrvInventario(int Min)
        {
            GrvInventario.DataSource = NP.ListadoInventario(Min);
            GrvInventario.DataBind();
            pRegInventario.Visible = true;
        }
        protected void visualizarGrvProducto(List<E_Producto> pLstProducto)
        {
            GrvProducto.DataSource = pLstProducto; 
            GrvProducto.DataBind();
        }
        #endregion

        #region GPRODUCTO
        protected void BtnBusquedaProducto_Click(object sender, EventArgs e)
        {
            List<E_Producto> LstProducto = NP.LstBuscaProducto(tbBuscaProducto.Text);

            if (LstProducto.Count == 0)
            {
                LblRespuesta.Text = "NO HAY COINCIDENCIA EN LA BASE DE DATOS";
                visualizarGrvProducto(LstProducto);
                LblRespuesta.Visible = true;
            }
            else
            {
                visualizarGrvProducto(LstProducto);
                LblRespuesta.Visible = false;
            }
        }

        protected void BtnNuevoProducto_Click(object sender, EventArgs e)
        {
            BtnModificarProducto.Visible = false;
            BtnRegistrarProducto.Visible = true;

            pAgregarNuevoProducto.Visible = true;
            pRegProductos.Visible = false;
            hfAgregarOModificar.Value = "agregar";
            header.Text += "<h2 class=\"card-header bg-success text-white\">Nuevo Producto</h2>";
            ptituloproducto.Controls.Add(header);

            selecImg.Visible = false;
        }

        protected void BtnCancelarRegistroProducto_Click(object sender, EventArgs e)
        {
            pAgregarNuevoProducto.Visible = false;
            pRegProductos.Visible = true;
            lbResultadoRegistrarProducto.Visible = false;
            lbRegProductos.Text = string.Empty;
            hfAgregarOModificar.Value = string.Empty;
            InicializarTBProductos();
        }

        protected void InicializarTBProductos()
        {
            tbCodDeProducto.Text = string.Empty;
            tbNombreDeProducto.Text = string.Empty;
            tbDescripcionDeProducto.Text = string.Empty;
            tbPrecioDeProducto.Text = string.Empty;
            tbStockDeProducto.Text = string.Empty;
        }

        protected E_Producto ControlesWebForm_ObjetoEntidadProducto()
        {
            int _IdProducto = 0;
            E_Producto Producto = new E_Producto
            {
                IdProducto = _IdProducto,
                CodProducto = tbCodDeProducto.Text.ToUpper(), 
                Nombre = tbNombreDeProducto.Text.ToUpper(),
                Descripcion = tbDescripcionDeProducto.Text.ToUpper(),
                Precio = tbPrecioDeProducto.Text,
                Stock = Convert.ToInt32(tbStockDeProducto.Text),
            };
            AgregarImagenProducto(Producto);
            return Producto;
        }

        protected void AgregarImagenProducto(E_Producto pProducto)
        {
            try
            {
                if (fuArchivo.HasFile)
                {
                    HttpPostedFile Archivo = fuArchivo.PostedFile;
                    string NombreArchivo = Path.GetFileName(Archivo.FileName);
                    string TipoArchivo = fuArchivo.PostedFile.ContentType;
                    int TamanioArchivo = fuArchivo.PostedFile.ContentLength;

                    //Leemos el archivo bit a bit
                    byte[] byteArchivo = new byte[TamanioArchivo];
                    Archivo.InputStream.Read(byteArchivo, 0, TamanioArchivo);

                    pProducto.ImagenProducto = byteArchivo;
                    lbResultadoDeCargarImagen.Text = string.Empty;
                }
                else
                {
                    lbResultadoDeCargarImagen.Text = "Debe capturar un archivo";
                    lbRegProductos.Text = "Imagen no encontrada -> ";
                    lbResultadoDeCargarImagen.Visible = true;
                }
            }
            catch (Exception E)
            {
                lbResultadoDeCargarImagen.Text = E.Message;
            }
        }

        protected void BtnRegistrarProducto_Click(object sender, EventArgs e)
        {
            N_Producto NP = new N_Producto();
            string R;

            header.Text += "<h2 class=\"card-header bg-success text-white\">Nuevo Producto</h2>";
            ptituloproducto.Controls.Add(header);

            R = NP.InsertaProducto(ControlesWebForm_ObjetoEntidadProducto());
            visualizarGrvProducto();
            CardFooter.Attributes.Clear();
           
            lbResultadoRegistrarProducto.Text = R;
            if (R.Contains("Exito"))
            {
                CardFooter.Attributes.Add("class", "row alert-success");
                InicializarTBProductos();
            }
            else
                CardFooter.Attributes.Add("class", "row alert-danger");
            lbResultadoRegistrarProducto.Visible = true;
        }

        protected void NoCambiar_CheckedChanged(object sender, EventArgs e)
        {
            hfAgregarOModificar.Value = "modificar";
        }
        #endregion

        #region PRODUCTO Eliminar | Modificar
        protected void GrvProducto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int f = 0;
            hfIdProducto.Value = GrvProducto.DataKeys[e.RowIndex].Value.ToString();
            while (f < GrvProducto.Rows.Count)
            {
                GrvProducto.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }

            GrvProducto.Rows[e.RowIndex].Attributes.Add("class", "alert-danger");
            e.Cancel = true;
            E_Producto EP = NP.BuscaProducto(Convert.ToInt16(hfIdProducto.Value));
            tbBorrarProducto.Text = EP.CodProducto;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalBorrarProducto').modal();", true);
        }

        protected void btnConfirmarBorradoProducto_Click(object sender, EventArgs e)
        {
            string R;
            if(tbBorrarProducto.Text == "")
            {
                lbRegProductos.Text = "No se encontró en la base de datos";
                return;
            }
            E_Producto EP = NP.BuscaProducto(Convert.ToInt16(hfIdProducto.Value));
            if (EP != null && tbBorrarProducto.Text != "")
            {
                R = NP.BorraProducto(EP.IdProducto, EP.ImagenProducto);
                lbbody.Attributes.Clear();
                lbbody.Attributes.Add("class", "alert-success");
                lbRegProductos.Text = R;
                visualizarGrvProducto();
            }
            else
            {
                lbRegProductos.Text = "No se encontró en la base de datos";
            }
            lbRegProductos.Visible = true;
        }

        protected void ObjetoEntidadProducto_ControlesWebForm(E_Producto P) //copiamos los valores del Objeto a los textbox's
        {
            hfIdProducto.Value = P.IdProducto.ToString();
            tbCodDeProducto.Text = P.CodProducto;
            tbNombreDeProducto.Text = P.Nombre;
            tbDescripcionDeProducto.Text = P.Descripcion;
            tbPrecioDeProducto.Text = P.Precio;
            tbStockDeProducto.Text = P.Stock.ToString();
        }

        protected void GrvProducto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int f = 0;
            BtnModificarProducto.Visible = true;
            BtnRegistrarProducto.Visible = false;
            
            hfIdProducto.Value = GrvProducto.DataKeys[e.NewEditIndex].Value.ToString(); /*recuperación de la llave primaria*/
            while (f < GrvProducto.Rows.Count)
            {
                GrvProducto.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }
            GrvProducto.Rows[e.NewEditIndex].Attributes.Add("class", "alert-primary");
            e.Cancel = true;

            pAgregarNuevoProducto.Visible = true;
            pRegProductos.Visible = false;
            E_Producto EP = NP.BuscaProducto(Convert.ToInt16(hfIdProducto.Value));
            ObjetoEntidadProducto_ControlesWebForm(EP);

            header.Text += "<h2 class=\"card-header bg-primary text-white\">Modificar Producto</h2>";
            ptituloproducto.Controls.Add(header);

            lbResultadoDeCargarImagen.Text = string.Empty;
            NoCambiar.Checked = true;
            hfAgregarOModificar.Value = "";
            selecImg.Visible = true;
        }

        protected E_Producto ControlesWebForm_ObjetoEntidadProducto(int IdProducto) //movemos los valores de textbox's modificados al objeto
        {
            int _IdProducto = IdProducto;
            
            E_Producto Producto = new E_Producto
            {
                IdProducto = _IdProducto,
                CodProducto = tbCodDeProducto.Text,
                Nombre = tbNombreDeProducto.Text,
                Descripcion = tbDescripcionDeProducto.Text,
                Precio = tbPrecioDeProducto.Text,
                Stock = Convert.ToInt32(tbStockDeProducto.Text),
            };
            
            switch (hfAgregarOModificar.Value)
            {
                case "agregar":
                case "modificar":
                    AgregarImagenProducto(Producto);
                    break;
                default:
                    Producto.ImagenProducto = NP.BuscaProducto(IdProducto).ImagenProducto;
                    break;
            }
            
            return Producto;
        }

        protected void BtnModificarProducto_Click(object sender, EventArgs e)
        {
            string R;
            lbRegProductos.Text = "";

            header.Text += "<h2 class=\"card-header bg-success text-white\">Nuevo Producto</h2>";
            ptituloproducto.Controls.Add(header);

            if (hfIdProducto.Value != "")
            {
                E_Producto Producto = NP.BuscaProducto(Convert.ToInt32(hfIdProducto.Value));
                E_Producto PModificado = ControlesWebForm_ObjetoEntidadProducto(Producto.IdProducto);

                R = NP.ModificaProducto(PModificado,PModificado.ImagenProducto);
                lbRegProductos.Text += R;

                lbbody.Attributes.Clear();
                if (R.Contains("Exito"))
                {
                    lbbody.Attributes.Add("class", "row alert-success");
                }
                else
                    lbbody.Attributes.Add("class", "row alert-danger");
                visualizarGrvProducto();
            }
            else
            {
                lbRegProductos.Text = "No se encontró en la Base de Datos";
            }

            lbRegProductos.Visible = true;
            InicializarTBProductos();
            BtnModificarProducto.Visible = false;
            BtnRegistrarProducto.Visible = true;
            pAgregarNuevoProducto.Visible = false;
            pRegProductos.Visible = true;
        }
        #endregion

        #region PRODUCTO Inventario
        protected void BtnInventarioMin_Click(object sender, EventArgs e)
        {
            hfInventarioMin.Value = tbInventarioMin.Text;
            visualizarGrvInventario(Convert.ToInt32(hfInventarioMin.Value));

        }

        protected void btnInventarioBajo_Click(object sender, EventArgs e)
        {
            GenerarTicketInventarioMin(Convert.ToInt32(hfInventarioMin.Value));
        }

        protected void GenerarTicketInventarioMin(int MinStock)
        {
            Document Documento = new Document(PageSize.LETTER, 20, 20, 100, 20);
            BaseFont helvetica = BaseFont.CreateFont("Helvetica", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fontNormal = new Font(helvetica, 10, Font.NORMAL);
            Font fontBold = new Font(helvetica, 10, Font.BOLD);
            PdfWriter pdf = PdfWriter.GetInstance(Documento, new FileStream(@"C:\Reportes\ReporteInventario_" + Convert.ToString(MinStock) + "ProductoMin.pdf", FileMode.Create));
            Documento.Open();
            pdf.PageEvent = new NuevaPagina("REPORTE DE INVENTARIO: " + Convert.ToString(MinStock) + " PRODUCTOS MINIMOS");
            pdf.Add(Chunk.NEWLINE);

            PdfPTable tblInventarioBajo = new PdfPTable(4) { WidthPercentage = 100 };
            float[] anchoCeldas = { 1F, 3.2F, 3.2F, 1.2F };
            tblInventarioBajo.SetWidths(anchoCeldas);

            PdfPCell CeldaCodProducto = new PdfPCell(new Phrase("No", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaNombre = new PdfPCell(new Phrase("Producto", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaDescripcion = new PdfPCell(new Phrase("Descripcion", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaStock = new PdfPCell(new Phrase("Stock", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

            tblInventarioBajo.AddCell(CeldaCodProducto);
            tblInventarioBajo.AddCell(CeldaNombre);
            tblInventarioBajo.AddCell(CeldaDescripcion);
            tblInventarioBajo.AddCell(CeldaStock);

            List<E_Producto> Producto = NP.LstProductoPorStock(MinStock);

            foreach (E_Producto P in Producto)
            {

                tblInventarioBajo.SpacingAfter = 5f;
                tblInventarioBajo.SpacingAfter = 5f;

                CeldaCodProducto = new PdfPCell(new Phrase(P.CodProducto, fontNormal)) { BorderWidth = 0 };
                CeldaNombre = new PdfPCell(new Phrase(P.Nombre, fontNormal)) { BorderWidth = 0 };
                CeldaDescripcion = new PdfPCell(new Phrase(P.Descripcion, fontNormal)) { BorderWidth = 0 };
                CeldaStock = new PdfPCell(new Phrase(P.Stock.ToString(), fontNormal)) { BorderWidth = 0 };

                tblInventarioBajo.AddCell(CeldaCodProducto);
                tblInventarioBajo.AddCell(CeldaNombre);
                tblInventarioBajo.AddCell(CeldaDescripcion);
                tblInventarioBajo.AddCell(CeldaStock);
            }

            Documento.Add(tblInventarioBajo);
            Documento.Close();
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.TransmitFile(@"C:\Reportes\ReporteInventario_" + Convert.ToString(MinStock) + "ProductoMin.pdf");
            Response.End();
        }

        public partial class NuevaPagina : PdfPageEventHelper
        {
            private string Msg;
            public NuevaPagina(string pMsg) { Msg = pMsg; }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //ENCABEZADO
                PdfPTable tblHeader = new PdfPTable(1);
                tblHeader.WidthPercentage = 100;
                Paragraph Parrafo = new Paragraph("ABARROTES EMMANUEL" + Chunk.NEWLINE + "Calle 11" + Chunk.NEWLINE + Chunk.NEWLINE + "ENSENADA, BAJA CALIFORNIA" + Chunk.NEWLINE + Msg);
                PdfPCell CeldaHeader = new PdfPCell(Parrafo);  //Incrustamos la celda en el parrafo
                CeldaHeader.HorizontalAlignment = Element.ALIGN_CENTER;

                //Posicionamos el encabezado en la parte superior
                tblHeader.AddCell(CeldaHeader);
                tblHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblHeader.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height, writer.DirectContent);



                //FOOTER
                PdfPTable tblFooter = new PdfPTable(1);
                tblFooter.TotalWidth = 100;
                tblFooter.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell CeldaFooter = new PdfPCell();
                CeldaFooter.Border = 0;

                Paragraph ParrafoFooter = new Paragraph();
                ParrafoFooter.Add("Pág. " + writer.PageNumber.ToString());
                CeldaFooter.AddElement(ParrafoFooter);
                tblFooter.AddCell(CeldaFooter);
                tblFooter.WriteSelectedRows(0, -1, 500, 30, writer.DirectContent);
            }
        }
        #endregion
    }
}