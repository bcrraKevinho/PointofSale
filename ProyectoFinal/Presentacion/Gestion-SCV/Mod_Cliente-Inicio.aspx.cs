using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocios;
using Entidades;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Presentacion.Gestion_SCV
{
    public partial class Mod_Cliente_Inicio : System.Web.UI.Page
    {
        N_Producto NP = new N_Producto();
        N_Ventas NV = new N_Ventas();
        N_DetalleVenta NDV = new N_DetalleVenta();
        N_Cliente NC = new N_Cliente();
        E_Cliente ClienteActual = new E_Cliente();

        protected void Page_Load(object sender, EventArgs e)
        {
            //TOVisualizarBD
            if (!IsPostBack)
            {
                if (Session["Cliente"] != null)
                {
                    hfUsuario.Value = Convert.ToString(Session["Cliente"]);
                }
                ModulosNav();
            }
        }

        protected void ModulosNav()
        {
            if (Request.QueryString["Nav"] != null || Request.QueryString["user"] != null)
            {
                hfNav.Value = Request.QueryString["Nav"];
                switch (hfNav.Value)
                {
                    case "Productos":
                        ON_Productos();
                        break;
                    case "Estatus":
                        ON_Estatus();
                        break;
                    default:
                        ON_Productos();
                        break;
                }
            }
            else if (Request.QueryString["Buscar"] != null)
            {
                hfBuscar.Value = Request.QueryString["Buscar"];
                BusquedaProducto();
            }
            else if (Request.QueryString["user"] != null)
            {
                ClienteActual = NC.BuscaCliente(hfUsuario.Value);
                ON_Productos();
            }
        }

        protected void BusquedaProducto()
        {
            List<E_Producto> LstProducto = NP.LstBuscaProducto(hfBuscar.Value);

            if (LstProducto.Count == 0)
            {
                LblRespuesta.Text = "NO HAY COINCIDENCIA EN LA BASE DE DATOS";
                visualizarRptProducto(LstProducto);
                LblRespuesta.Visible = true;
            }
            else
            {
                visualizarRptProducto(LstProducto);
                LblRespuesta.Visible = false;
            }
        }

        protected void ON_Productos()
        {
            pEstatus.Visible = false;
            visualizarRptProducto();
            pProductos.Visible = true;
        }

        protected void ON_Estatus()
        {
            pProductos.Visible = false;
            pEstatus.Visible = true;
            visualizarGrvVentas(hfUsuario.Value);
        }

        protected void visualizarGrvVentas(string usuario)
        {
            E_Cliente C = NC.BuscaCliente(hfUsuario.Value);
            llenarInfoUsuario(C);
            lbNombreCliente.Text = "Bienvenido " + C.Nombre + " " + C.Apellidos;

            if (C != null)
            {
                List<E_Ventas> LstVentas = NV.LstVentasPorCliente(C.IdCliente, DateTime.Now.ToShortDateString(), DateTime.Today.ToShortDateString());
                if (LstVentas.Count < 1)
                {
                    lbRespuestaVentas.Text = "";
                    RespuestaBusquedaVentas.Attributes.Clear();
                    RespuestaBusquedaVentas.Attributes.Add("class", "row alert-danger text-center");
                    lbRespuestaVentas.Text = "No se hay compras el día de hoy:(";

                }
                else
                {
                    GrvRegVentas.DataSource = NV.DT_LstVentaCliente(C.IdCliente, DateTime.Today.ToShortDateString(), DateTime.Today.ToShortDateString());
                    GrvRegVentas.DataBind();
                }

            }
        }

        protected void llenarInfoUsuario(E_Cliente C)
        {
            tbnombre.Text = C.Nombre + " " + C.Apellidos;
            tbcredito.Text = C.Credito.ToString();
            tbcuentaactual.Text = C.CuentaActual.ToString();
        }

        protected void visualizarRptProducto()
        {
            //modo repeater

            repeaterProd.DataSource = NP.LstProducto();
            repeaterProd.DataBind();

        }

        protected void visualizarRptProducto(List<E_Producto> pLstProducto)
        {
            pEstatus.Visible = false;
            repeaterProd.DataSource = pLstProducto;
            repeaterProd.DataBind();
        }

        protected void CalendarioInicio_SelectionChanged(object sender, EventArgs e)
        {
            tbInicio.Text = CalendarioInicio.SelectedDate.ToShortDateString();
        }

        protected void VerCalenInicio_Click(object sender, EventArgs e)
        {
            if (CalendarioInicio.Visible)
                CalendarioInicio.Visible = false;
            else
                CalendarioInicio.Visible = true;
        }

        protected void BtnBuscarFechas_Click(object sender, EventArgs e)
        {
            E_Cliente C = NC.BuscaCliente(hfUsuario.Value);
            if (tbInicio.Text == "")
            {
                lbRespuestaVentas.Text = "";
                RespuestaBusquedaVentas.Attributes.Clear();
                RespuestaBusquedaVentas.Attributes.Add("class", "row alert-danger text-center");
                lbRespuestaVentas.Text = "No se ha seleccionado un periodo de fechas";
                return;
            }

            List<E_Ventas> LstVentas = NV.LstVentasPorCliente(C.IdCliente, tbInicio.Text, tbInicio.Text);
            if (LstVentas.Count == 0)
            {
                lbRespuestaVentas.Text = "";
                RespuestaBusquedaVentas.Attributes.Clear();
                RespuestaBusquedaVentas.Attributes.Add("class", "row alert-warning text-center");
                lbRespuestaVentas.Text = "NO HAY REGISTROS DE VENTAS EN EL INTERVALO INDICADO";
                GrvRegVentas.DataSource = NV.DT_LstVentaCliente(C.IdCliente, tbInicio.Text, tbInicio.Text);
                GrvRegVentas.DataBind();
            }
            else
            {
                lbRespuestaVentas.Text = "";
                GrvRegVentas.DataSource = NV.DT_LstVentaCliente(C.IdCliente, tbInicio.Text, tbInicio.Text);
                GrvRegVentas.DataBind();
            }

        }

        protected void btnAllCompras_Click(object sender, EventArgs e)
        {
            E_Cliente C = NC.BuscaCliente(hfUsuario.Value);
            List<E_Ventas> LstVentas = NV.LstVentasPorCliente(C.IdCliente);
            if (LstVentas.Count == 0)
            {
                lbRespuestaVentas.Text = "";
                RespuestaBusquedaVentas.Attributes.Clear();
                RespuestaBusquedaVentas.Attributes.Add("class", "row alert-warning text-center");
                lbRespuestaVentas.Text = "SIN REGISTRO DE VENTAS";
                GrvRegVentas.DataSource = NV.DT_LstVentaCliente(C.IdCliente);
                GrvRegVentas.DataBind();
            }
            else
            {
                lbRespuestaVentas.Text = "";
                GrvRegVentas.DataSource = NV.DT_LstVentaCliente(C.IdCliente);
                GrvRegVentas.DataBind();
            }
        }

        protected void GrvRegVentas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int f = 0;
            GrvDetalles.Visible = true;
            hfIdVenta.Value = GrvRegVentas.DataKeys[e.NewEditIndex].Value.ToString();
            while (f < GrvRegVentas.Rows.Count)
            {
                GrvRegVentas.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }

            GrvRegVentas.Rows[e.NewEditIndex].Attributes.Add("class", "alert-primary");

            e.Cancel = true;
            E_Ventas EV = NV.BuscaVenta(Convert.ToInt32(hfIdVenta.Value));

            GrvDetalles.DataSource = NDV.DT_LstDetalleVentaPorId(EV.IdVenta);
            GrvDetalles.DataBind();
            GrvDetalles.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalDetalleVenta').modal();", true);
        }

        #region GenerarReporteCompras
        protected void GenerarReporte_Click(object sender, EventArgs e)
        {
            E_Cliente C = NC.BuscaCliente(hfUsuario.Value);
            List<E_Ventas> LstVentas = NV.LstVentasPorCliente(C.IdCliente);
            GenerarReporteVentasCliente(LstVentas);
        }

        protected void GenerarReporteVentasCliente(List<E_Ventas> Ventas)
        {
            Document Documento = new Document(PageSize.LETTER, 20, 20, 100, 20);
            BaseFont helvetica = BaseFont.CreateFont("Helvetica", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fontNormal = new Font(helvetica, 10, Font.NORMAL);
            Font fontBold = new Font(helvetica, 10, Font.BOLD);
            PdfWriter pdf = PdfWriter.GetInstance(Documento, new FileStream(@"C:\Reportes\ReporteCompras_" + Convert.ToString(NC.BuscaCliente(hfUsuario.Value).CodCliente) + ".pdf", FileMode.Create));
            Documento.Open();
            pdf.PageEvent = new NuevaPagina("REPORTE DE VENTAS: Cliente " + Convert.ToString(NC.BuscaCliente(hfUsuario.Value).CodCliente));
            pdf.Add(Chunk.NEWLINE);

            //***************************************************** TEXT VENTAS GENERALES ********************************************************************************************

            PdfPTable tblInfoGeneral = new PdfPTable(1) { WidthPercentage = 100 };
            float[] anchoCeldaInfo = { 7F };
            tblInfoGeneral.SetWidths(anchoCeldaInfo);

            PdfPCell CeldaInfo = new PdfPCell(new Phrase("INFORMACION VENTAS GENERALES" + Chunk.NEWLINE + Chunk.NEWLINE, fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            tblInfoGeneral.AddCell(CeldaInfo);
            Documento.Add(tblInfoGeneral);
            //*************************************************************************************************************************************************

            PdfPTable tblVentaCliente = new PdfPTable(3) { WidthPercentage = 100 };
            float[] anchoCeldasVenta = { 1F, 3.2F, 1.2F };
            tblVentaCliente.SetWidths(anchoCeldasVenta);

            PdfPCell CeldaCod = new PdfPCell(new Phrase("CodVenta", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaTotalProdcutos = new PdfPCell(new Phrase("TotalProductos", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaTotal = new PdfPCell(new Phrase("Total", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

            tblVentaCliente.AddCell(CeldaCod);
            tblVentaCliente.AddCell(CeldaTotalProdcutos);
            tblVentaCliente.AddCell(CeldaTotal);

            foreach (E_Ventas V2 in Ventas)
            {
                E_Ventas Venta2 = NV.BuscaVenta(V2.CodVenta);
                tblVentaCliente.SpacingAfter = 5f;
                tblVentaCliente.SpacingAfter = 5f;

                CeldaCod = new PdfPCell(new Phrase(Venta2.CodVenta, fontNormal)) { BorderWidth = 0 };
                CeldaTotalProdcutos = new PdfPCell(new Phrase(Venta2.CantidadProducto.ToString(), fontNormal)) { BorderWidth = 0 };
                CeldaTotal = new PdfPCell(new Phrase(Venta2.Total, fontNormal)) { BorderWidth = 0 };

                tblVentaCliente.AddCell(CeldaCod);
                tblVentaCliente.AddCell(CeldaTotalProdcutos);
                tblVentaCliente.AddCell(CeldaTotal);
            }

            Documento.Add(tblVentaCliente);


            //***************************************************** TEXT DETALLES DE VENTAS ********************************************************************************************

            PdfPTable tblInfoDetalles = new PdfPTable(1) { WidthPercentage = 100 };
            float[] anchoCeldaDetalles = { 7F };
            tblInfoGeneral.SetWidths(anchoCeldaInfo);

            PdfPCell CeldaDetalle = new PdfPCell(new Phrase("" + Chunk.NEWLINE + Chunk.NEWLINE + Chunk.NEWLINE + Chunk.NEWLINE + "INFORMACION DETALLES DE VENTAS" + Chunk.NEWLINE + Chunk.NEWLINE, fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            tblInfoDetalles.AddCell(CeldaDetalle);
            Documento.Add(tblInfoDetalles);


            foreach (E_Ventas V in Ventas)
            {
                E_Ventas Venta = NV.BuscaVenta(V.CodVenta);

                //**************************************** TABLA DE DETALLES *********************************************

                PdfPTable tblComprasCliente = new PdfPTable(5) { WidthPercentage = 100 };
                float[] anchoCeldas = { 1F, 3.2F, 3.2F, 1.2F, 1F };
                tblComprasCliente.SetWidths(anchoCeldas);

                PdfPCell CeldaCantidad = new PdfPCell(new Phrase("Cant", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
                PdfPCell CeldaNombre = new PdfPCell(new Phrase("Producto", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
                PdfPCell CeldaDescripcion = new PdfPCell(new Phrase("Descripcion", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
                PdfPCell CeldaPrecio = new PdfPCell(new Phrase("Precio", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
                PdfPCell CeldaImporte = new PdfPCell(new Phrase("Importe", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

                tblComprasCliente.AddCell(CeldaCantidad);
                tblComprasCliente.AddCell(CeldaNombre);
                tblComprasCliente.AddCell(CeldaDescripcion);
                tblComprasCliente.AddCell(CeldaPrecio);
                tblComprasCliente.AddCell(CeldaImporte);



                List<E_DetalleVenta> ListDetalles = NDV.LstBuscaDetalle(Venta.IdVenta);
                foreach (E_DetalleVenta DV in ListDetalles)
                {
                    E_Producto P = NP.BuscaProducto(DV.Producto);
                    tblComprasCliente.SpacingAfter = 5f;
                    tblComprasCliente.SpacingAfter = 5f;

                    CeldaCantidad = new PdfPCell(new Phrase(DV.Cantidad.ToString(), fontNormal)) { BorderWidth = 0 };
                    CeldaNombre = new PdfPCell(new Phrase(P.Nombre, fontNormal)) { BorderWidth = 0 };
                    CeldaDescripcion = new PdfPCell(new Phrase(P.Descripcion, fontNormal)) { BorderWidth = 0 };
                    CeldaPrecio = new PdfPCell(new Phrase(P.Precio, fontNormal)) { BorderWidth = 0 };
                    CeldaImporte = new PdfPCell(new Phrase(DV.ImporteTotal, fontNormal)) { BorderWidth = 0 };

                    tblComprasCliente.AddCell(CeldaCantidad);
                    tblComprasCliente.AddCell(CeldaNombre);
                    tblComprasCliente.AddCell(CeldaDescripcion);
                    tblComprasCliente.AddCell(CeldaPrecio);
                    tblComprasCliente.AddCell(CeldaImporte);
                }
                Documento.Add(tblComprasCliente);
                pdf.Add(Chunk.NEWLINE);
                pdf.Add(Chunk.NEWLINE);
            }

            Documento.Close();
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.TransmitFile(@"C:\Reportes\ReporteCompras_" + Convert.ToString(NC.BuscaCliente(hfUsuario.Value).CodCliente) + ".pdf");
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