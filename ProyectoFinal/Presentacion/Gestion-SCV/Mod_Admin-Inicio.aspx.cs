
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocios;
using Entidades;
using System.IO;
using System.Data;

using iTextSharp.text;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem; //para el Dropdown List de DETALLEVENTA-CLIENTE
using System.Net.Mail;
using System.Text;
using System.Net;

namespace Presentacion.Gestion_SCV
{
    public partial class Mod_Admin_Inicio : System.Web.UI.Page
    {
        N_Cliente NC = new N_Cliente();
        N_Producto NP = new N_Producto();
        N_Usuario NU = new N_Usuario();
        N_Ventas NV = new N_Ventas();
        N_DetalleVenta NDV = new N_DetalleVenta();

        private List<E_DetalleVenta> Detalles
        {
            get
            {
                if (HttpContext.Current.Session["Detalles"] == null)
                {
                    HttpContext.Current.Session["Detalles"] = new List<E_DetalleVenta>();
                }
                return HttpContext.Current.Session["Detalles"] as List<E_DetalleVenta>;
            }
            set
            {
                HttpContext.Current.Session["Detalles"] = value;
            }
        } //ListaDetalles - Inicio

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _IsPostBack();
            }
            PreAcciones();
        }

        #region IsPostBack
        protected void _IsPostBack()
        {
            //Para el cambio de ventana 
            ModulosNav();
            //Llenamos DDL
            LlenarDDL();

            AsignarCodVenta();
            visualizarGrvTodoProducto();
        }

        protected void AsignarCodVenta()
        {
            E_Ventas V = NV.BuscaVenta(NV.IdUltimaVenta());
            String ultimo, ult;
            int val;
            ultimo = V.CodVenta;
            ult = ultimo.Substring(ultimo.Length - 2, 2);
            val = Convert.ToInt32(ult) + 1;
            tbCodVenta.Text = "V00" + val.ToString();
        }

        protected void ModulosNav()
        {
            if (Request.QueryString["Nav"] != null)
            {
                hfNav.Value = Request.QueryString["Nav"];
            }
            switch (hfNav.Value)
            {
                case "Ventas":
                    ON_RegVentas();
                    break;
                case "GClientes":
                    ON_GClientes();
                    break;
                default:
                    ON_VentaPrincipal();
                    break;
            }
        }
        #endregion

        #region PreAcciones
        protected void PreAcciones() //AGREG {SESSIONES} [TOTALES]
        {
            MostrarRegDetalles();

            if (Session["StockMax"] != null)
            {
                confirmacionParam.Attributes.Clear();
                confirmacionParam.Attributes.Add("class", "row-col-12 text-center alert-danger");
                lbConfirmacionParametros.Text = "Verifique: Cantidad o Producto incorrecto | supera el stock disponible";

                Session.Remove("StockMax");
            }

            if (Session["VentaTerminada"] != null)
            {
                confirmacionParam.Attributes.Clear();
                confirmacionParam.Attributes.Add("class", "row-col-12 text-center alert-success");
                lbConfirmacionParametros.Text = Session["VentaTerminada"].ToString();

                Session.Remove("VentaTerminada");
            }
            if (Session["TotalImporte"] != null && Session["TotalProductos"] != null)
            {
                tbTotalImporte.Text = Session["TotalImporte"].ToString();
                tbTotalProductos.Text = Session["TotalProductos"].ToString();
            }
            if (Detalles.Count == 0)
            {
                tbTotalImporte.Text = string.Empty;
                tbTotalProductos.Text = string.Empty;
            }
            if (Detalles.Count != 0)
            {
                decimal importeTotal = 0;
                int NoProductos = 0;
                foreach (E_DetalleVenta N in Detalles)
                {
                    importeTotal = importeTotal + Convert.ToDecimal(N.ImporteTotal);
                    NoProductos = NoProductos + N.Cantidad;
                }
                tbTotalImporte.Text = Convert.ToString(importeTotal);
                tbTotalProductos.Text = Convert.ToString(NoProductos);
            }
        }

        #endregion

        #region ON/OFF panel
        protected void allOFF()
        {
            CalendarioInicio.Visible = false;
            CalendarioFin.Visible = false;
            pVentaPrincipal.Visible = false;
            pAgregarNuevoCliente.Visible = false;
            pRegClientes.Visible = false;
            pRegVentas.Visible = false;
        } //OffVisible-Paneles

        protected void OFF_Label()
        {
            LblRespuesta.Text = string.Empty;
            lbResultadoNuevoCliente.Text = string.Empty;
            lbRespuestaVentas.Text = string.Empty;
            lbRegVentas.Text = string.Empty;
            lbRegClientes.Text = string.Empty;
            lbConfirmacionParametros.Text = string.Empty;
        } //OffVisible-Labels

        protected void ON_GClientes()
        {
            allOFF();
            visualizarGrvCliente();
            pRegClientes.Visible = true;
        }

        protected void ON_VentaPrincipal()
        {
            allOFF();
            pVentaPrincipal.Visible = true;
        }

        protected void ON_RegVentas()
        {
            allOFF();
            visualizarGrvRegVentas();
            pRegVentas.Visible = true;
        }

        protected void ON_RegUsuarios()
        {
            allOFF();
            visualizarGrvUsuarios();
            pRegClientes.Visible = true;
        }
        #endregion

        #region visualizaciones sin parametro
        protected void visualizarGrvTodoProducto()
        {
            GrvTodo.DataSource = NP.LstProducto();
            GrvTodo.DataBind();
        }

        protected void visualizarGrvCliente()
        {
            GrvCliente.DataSource = NC.ListadoClientes();
            GrvCliente.DataBind();
        }

        protected void visualizarGrvUsuarios()
        {
            GrvUsuario.DataSource = NU.LstUsuario();
            GrvUsuario.DataBind();
        }

        protected void visualizarGrvRegVentas()
        {
            GrvRegVentas.DataSource = NV.DT_LstVenta();
            GrvRegVentas.DataBind();
        }

        protected void visualizarGrvVentaPrincipal()
        {
            GrvDetalleVenta.DataSource = Detalles;
            GrvDetalleVenta.DataBind();

            if (Detalles != null)
            {
                Decimal importeTotal = 0;
                int NoProductos = 0;
                foreach (E_DetalleVenta N in Detalles)
                {
                    importeTotal = importeTotal + Convert.ToDecimal(N.ImporteTotal);
                    NoProductos = NoProductos + N.Cantidad;
                }
                tbTotalImporte.Text = importeTotal.ToString();
                tbTotalProductos.Text = NoProductos.ToString();
            }
        }

        protected void visualizarGrvDeudaClientes()
        {
            GrvDeudasCliente.DataSource = NC.ListadoClientesAdeudo();
            GrvDeudasCliente.DataBind();
        }
        #endregion

        #region visualizaciones con parametro
        protected void visualizarGrvCliente(List<E_Cliente> pLstCliente)
        {
            GrvCliente.DataSource = pLstCliente;
            GrvCliente.DataBind();
        }

        protected void visualizarGrvDeudaClientes(List<E_Cliente> pLstCliente)
        {
            GrvDeudasCliente.DataSource = pLstCliente;
            GrvDeudasCliente.DataBind();
        }

        protected void visualizarGrvUsuario(List<E_Usuario> LstUsuario)
        {
            GrvUsuario.DataSource = LstUsuario;
            GrvUsuario.DataBind();
        }
        #endregion

        #region Inicio Detalle-Cliente
        protected void LlenarDDL()
        {
            ddlNombres.DataSource = NC.ListaCliente();
            ddlNombres.DataTextField = "CodCliente";
            ddlNombres.DataValueField = "IdCliente";
            ddlNombres.DataBind();
            ddlNombres.Items.Insert(0, new ListItem("<Selecciona un cliente>", "0"));
        }

        protected void Credito_CheckedChanged(object sender, EventArgs e)
        {
            if (Credito.Checked)
            {
                ddlNombres.Enabled = true;
                lbSinCliente.Visible = false;
                btnAgregarCliente.Enabled = true;
            }
            else if (Credito.Checked == false)
            {
                ddlNombres.Enabled = false;
                tbNombreCliente.Text = "0";
                lbSinCliente.Visible = true;
                btnAgregarCliente.Enabled = false;
            }
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (ddlNombres.Text != "0")
            {
                E_Cliente C = NC.BuscaCliente(Convert.ToInt32(ddlNombres.Text));
                tbNombreCliente.Text = C.Nombre + " " + C.Apellidos;
                lbConfirmacionParametros.Text = "";
            }
            else
            {
                confirmacionParam.Attributes.Clear();
                confirmacionParam.Attributes.Add("class", "row-col-12 text-center alert-danger");
                lbConfirmacionParametros.Text = "Seleccione un cliente valido";
            }

        }
        #endregion

        #region Panel-Inicio DetalleVenta
        protected void BtnBusqueda_Click(object sender, EventArgs e)
        {
            OFF_Label();
            if (tbInsertaCodProducto.Text != "")
                InsertarProducto(tbInsertaCodProducto.Text);
            else
                return;
        }

        protected void InsertarProducto(string producto)
        {
            E_Producto Producto = NP.BuscaProducto(producto);
            if (Producto != null)
            {
                hfIdProductoResult.Value = Convert.ToString(Producto.IdProducto);
                tbCodigoProducto.Text = Producto.CodProducto;
                tbNombreProducto.Text = Producto.Nombre;
                tbDescripcionProducto.Text = Producto.Descripcion;
                tbStock.Text = Producto.Stock.ToString();
                tbPrecio.Text = Producto.Precio;
                LblRespuesta.Visible = false;
            }
            else
            {
                confirmacionParam.Attributes.Clear();
                confirmacionParam.Attributes.Add("class", "row-col-12 text-center alert-danger");
                lbConfirmacionParametros.Text = "Asegurese de haber ingresado todos los datos";
                lbConfirmacionParametros.Text = "No se encontró en la Base de Datos";
            }
        }

        protected void InsertarProducto(int producto)
        {
            E_Producto Producto = NP.BuscaProducto(producto);
            if (Producto != null)
            {
                hfIdProductoResult.Value = Convert.ToString(Producto.IdProducto);
                tbCodigoProducto.Text = Producto.CodProducto;
                tbNombreProducto.Text = Producto.Nombre;
                tbDescripcionProducto.Text = Producto.Descripcion;
                tbStock.Text = Producto.Stock.ToString();
                tbPrecio.Text = Producto.Precio;
                LblRespuesta.Visible = false;
            }
            else
            {
                LblRespuesta.Text = "No se encontró en la Base de Datos";
                LblRespuesta.Visible = true;
            }
        }

        protected void GrvTodo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            OFF_Label();
            hfIdProductoResult.Value = GrvTodo.DataKeys[e.NewEditIndex].Value.ToString();
            e.Cancel = true;

            InsertarProducto(Convert.ToInt32(hfIdProductoResult.Value));
        }

        protected void limpiarParametrosDetalleVenta()
        {
            tbCodigoProducto.Text = string.Empty;
            tbNombreProducto.Text = string.Empty;
            tbDescripcionProducto.Text = string.Empty;
            tbStock.Text = string.Empty;
            tbPrecio.Text = string.Empty;
            tbCantidad.Text = string.Empty;
            tbInsertaCodProducto.Text = string.Empty;
        }

        protected void btnAgregarDetalleVenta_Click(object sender, EventArgs e)
        {
            if (tbCodigoProducto.Text == "")
            {
                Session["StockMax"] = "Superado";
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            lbConfirmacionParametros.Text = string.Empty;
            hfCarritoEnUso.Value = "nel";

            if (Convert.ToInt32(tbStock.Text) >= Convert.ToInt32(tbCantidad.Text) && Convert.ToInt32(tbCantidad.Text) > 0)
            {
                E_Producto NuevoProducto = NP.BuscaProducto(tbCodigoProducto.Text);
                Decimal importe = Convert.ToDecimal(tbCantidad.Text) * Convert.ToDecimal(tbPrecio.Text);
                Decimal importeTotal = 0;
                int NoProductos = 0;
                int _IdDetalleVenta = 0;
                E_DetalleVenta NuevoDetalleVenta = new E_DetalleVenta
                {
                    IdDetalleVenta = _IdDetalleVenta,
                    IdVenta = 0,
                    Producto = NuevoProducto.IdProducto,
                    Descripcion = NuevoProducto.IdProducto,
                    Cantidad = Convert.ToInt32(tbCantidad.Text),
                    PrecioUnidad = NuevoProducto.IdProducto,
                    ImporteTotal = Convert.ToString(importe)
                };

                if (!VerificarRepetido(Detalles, NuevoDetalleVenta))
                {
                    Detalles.Add(NuevoDetalleVenta);
                }

                QuitarStock(NuevoDetalleVenta);

                foreach (E_DetalleVenta N in Detalles)
                {
                    importeTotal = importeTotal + Convert.ToDecimal(N.ImporteTotal);
                    NoProductos = NoProductos + N.Cantidad;
                }

                Session["TotalImporte"] = Convert.ToString(importeTotal);
                Session["TotalProductos"] = Convert.ToString(NoProductos);
                tbTotalImporte.Text = Convert.ToString(importeTotal);
                tbTotalProductos.Text = Convert.ToString(NoProductos);
                limpiarParametrosDetalleVenta();
                visualizarGrvVentaPrincipal();

                lbConfirmacionParametros.Text = "";
            }
            else
            {
                Session["StockMax"] = "Superado";
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            MostrarRegDetalles();
        }

        protected bool VerificarRepetido(List<E_DetalleVenta> Lista, E_DetalleVenta Detalle)
        {
            for (int i = 0; i < Lista.Count; i++)
            {
                if (Lista[i].Producto == Detalle.Producto)
                {
                    Lista[i].Cantidad += Detalle.Cantidad;
                    Lista[i].ImporteTotal = (Convert.ToDecimal(Lista[i].ImporteTotal) + Convert.ToDecimal(Detalle.ImporteTotal)).ToString();
                    return true;
                }
            }
            return false;
        }

        protected E_Ventas ControlesWebForm_ObjetoEntidadVentas()
        {
            int _IdVenta = 0;

            E_Ventas Venta = new E_Ventas
            {
                IdVenta = _IdVenta,
                CodVenta = tbCodVenta.Text.ToUpper(),
                IdCliente = Convert.ToInt32(ddlNombres.Text),
                CantidadProducto = Convert.ToInt32(tbTotalProductos.Text),
                Total = tbTotalImporte.Text
            };
            tbCambio_ImporteTotal.Text = tbTotalImporte.Text;
            return Venta;
        }

        protected bool AumentarCuentaCliente(int idCliente)
        {
            if (idCliente == 0)
            {
                return true;
            }
            E_Cliente C = NC.BuscaCliente(idCliente);
            C.CuentaActual += Convert.ToDecimal(tbTotalImporte.Text);
            if (C.CuentaActual > C.Credito)
                return false;
            else
            {
                NC.ModificaCliente(C);
                return true;
            }
        }

        protected void AgregarDetallesVenta()
        {
            foreach (E_DetalleVenta N in Detalles)
            {
                N.IdVenta = NV.IdUltimaVenta();
                NDV.InsertaDetalleVenta(N);
            }
        }

        protected void QuitarStock(E_DetalleVenta DV)
        {
            E_Producto producto = NP.BuscaProducto(DV.Producto);
            int StockActual = producto.Stock;
            int ProductosVendidos = DV.Cantidad;
            producto.Stock = StockActual - ProductosVendidos;
            NP.ModificaProducto(producto, producto.ImagenProducto);
        }

        protected void btConfirmarVenta_Click(object sender, EventArgs e)
        {
            string R;

            if (tbCodVenta.Text == "" || Credito.Checked && (tbNombreCliente.Text == "" || tbNombreCliente.Text == "0") || Detalles.Count == 0)
            {
                confirmacionParam.Attributes.Clear();
                confirmacionParam.Attributes.Add("class", "row-col-12 text-center alert-danger");
                lbConfirmacionParametros.Text = "Asegurese de haber ingresado todos los datos";
                return;
            }

            if (!AumentarCuentaCliente(Convert.ToInt32(ddlNombres.Text)))
            {
                confirmacionParam.Attributes.Clear();
                confirmacionParam.Attributes.Add("class", "row-col-12 text-center alert-danger");
                lbConfirmacionParametros.Text = "CLIENTE: Límite de crédito superado";
                return;
            }

            if (!Credito.Checked)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalCambioVenta').modal();", true);
                tbCambio_ImporteTotal.Text = tbTotalImporte.Text;
            }
            else
            {
                R = NV.InsertaVenta(ControlesWebForm_ObjetoEntidadVentas());

                AgregarDetallesVenta();

                Detalles.Clear();
                Session["VentaTerminada"] = R;
                Session.Remove("TotalImporte");
                Session.Remove("TotalProductos");

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        protected void btnConfirmarCambio_Click(object sender, EventArgs e)
        {
            string R;
            R = NV.InsertaVenta(ControlesWebForm_ObjetoEntidadVentas());

            AgregarDetallesVenta();

            Detalles.Clear();
            Session["VentaTerminada"] = R;
            Session.Remove("TotalImporte");
            Session.Remove("TotalProductos");

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void tbCambio_PagoCon_TextChanged(object sender, EventArgs e)
        {
            if (verificarCambio())
                btnConfirmarCambio.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalCambioVenta').modal();", true);
        }

        protected bool verificarCambio()
        {
            if (tbCambio_PagoCon.Text == "")
                tbCambio_PagoCon.Text = "0";
            decimal importe = Convert.ToDecimal(tbCambio_ImporteTotal.Text);
            decimal pagoCon = Convert.ToDecimal(tbCambio_PagoCon.Text);
            
            decimal cambio = pagoCon - importe;
            if (cambio < 0)
            {
                resultado.Attributes.Clear();
                resultado.Attributes.Add("class", "form-control d-flex justify-content-center alert-danger");
                resultado.InnerText = "Error: Cantidad insuficiente para pagar";
                return false;
            }
            else
            {
                resultado.Attributes.Clear();
                resultado.Attributes.Add("class", "form-control d-flex justify-content-center alert-success");
                resultado.InnerText = "";
                tbCambio_Cambio.Text = cambio.ToString();
                return true;
            }
        }

        protected void MostrarRegDetalles()
        {
            Tabla.Text = string.Empty;

            Tabla.Text += "<table id=\"VentaPrincipal\" runat=\"server\" class=\"table table-striped table-bordered table-success table-hover\" style=\"width:100%>\" OnRowDeleting=\"VentaPrincipal_RowDeleting\"";
            Tabla.Text += "<thead>";
            Tabla.Text += "<tr>";
            Tabla.Text += "<th scope=\"col\"> Cantidad </ th >";
            Tabla.Text += "<th scope=\"col\"> Producto </ th >";
            Tabla.Text += "<th scope=\"col\"> Descripción </ th >";
            Tabla.Text += "<th scope=\"col\"> Precio Unidad </ th >";
            Tabla.Text += "<th scope=\"col\"> Importe Total </ th >";
            Tabla.Text += "</tr>";
            Tabla.Text += "</thead>";
            foreach (E_DetalleVenta DV in Detalles)
            {
                E_Producto P = NP.BuscaProducto(DV.Producto);

                Tabla.Text += "<tr class=\" table table-light\">";

                Tabla.Text += "<td>" + DV.Cantidad + "</ td >";
                Tabla.Text += "<td>" + P.Nombre + "</td>";
                Tabla.Text += "<td>" + P.Descripcion + "</td>";
                Tabla.Text += "<td>" + P.Precio + "</td>";
                Tabla.Text += "<td>" + DV.ImporteTotal + "</td>";
                //Tabla.Text += "<td>" +
                Tabla.Text += "</tr>";
            }
            Tabla.Text += "</table>";

            pVenta.Controls.Add(Tabla);
            nuevoSelec();
        }

        protected void nuevoSelec()
        {
            if (Detalles.Count > 0 && hfCarritoEnUso.Value == "clarines")
            {
                Del.Text += "<div class=\"container-fluid d-block mt-5\">";
                int index = 0;
                foreach (E_DetalleVenta DV in Detalles)
                {
                    LinkButton Seleccionar = new LinkButton();
                    Seleccionar.ID = "SeleccionarProd" + index.ToString();
                    Seleccionar.Text = "Eliminar";
                    Seleccionar.CausesValidation = false;
                    Seleccionar.Click += new EventHandler(BtnNuevo_Click);
                    Seleccionar.CommandArgument += index.ToString();
                    Seleccionar.CssClass += "d-block pb-4 text-danger";

                    pDelete.Controls.Add(Seleccionar);
                    index += 1;
                }
                Del.Text += "</div >";
            }
            else if (hfCarritoEnUso.Value == "nel")
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            hfCarritoEnUso.Value = "clarines";
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            string yourValue = btn.CommandArgument;
            E_DetalleVenta dv = Detalles[Convert.ToInt32(btn.CommandArgument)];

            AgregarStock(dv);
            Detalles.Remove(dv);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void AgregarStock(E_DetalleVenta DV)
        {
            E_Producto producto = NP.BuscaProducto(DV.Producto);
            int StockActual = producto.Stock;
            int ProductosVendidos = DV.Cantidad;
            producto.Stock = StockActual + ProductosVendidos;
            NP.ModificaProducto(producto, producto.ImagenProducto);
        }

        protected void GrvDetalleVenta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            hfSeleccionDetalles.Value = GrvDetalleVenta.DataKeys[e.RowIndex].Value.ToString(); /*recuperación de la llave primaria*/
            e.Cancel = true;
            E_DetalleVenta dv = Detalles[e.RowIndex];
            
            Detalles.Remove(dv);
            
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            visualizarGrvVentaPrincipal();
        }

        #region GenerarTicketUltimaVenta
        protected void btGenerarTicket_Click(object sender, EventArgs e)
        {
            GenerarTicketVenta(NV.IdUltimaVenta());
        }

        protected void GenerarTicketVenta(int IdVentaRealizada)
        {
            E_Ventas VentaRealizada = NV.BuscaVenta(IdVentaRealizada);
            Document Documento = new Document(PageSize.SMALL_PAPERBACK, 20, 20, 100, 20);
            BaseFont helvetica = BaseFont.CreateFont("Helvetica", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fontNormal = new Font(helvetica, 10, Font.NORMAL);
            Font fontBold = new Font(helvetica, 10, Font.BOLD);
            PdfWriter pdf = PdfWriter.GetInstance(Documento, new FileStream(@"C:\Reportes\ReporteVenta" + VentaRealizada.CodVenta + ".pdf", FileMode.Create));
            Documento.Open();
            pdf.PageEvent = new NuevaPagina("REPORTE DE VENTA " + VentaRealizada.CodVenta);
            pdf.Add(Chunk.NEWLINE);

            PdfPTable tblDetalleVenta = new PdfPTable(5) { WidthPercentage = 100 };
            float[] anchoCeldas = { 0.6F, 3F, 3.2F, 1.2F, 1.2F };
            tblDetalleVenta.SetWidths(anchoCeldas);

            PdfPCell CeldaCantidad = new PdfPCell(new Phrase("No", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaProducto = new PdfPCell(new Phrase("Producto", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaDescripcion = new PdfPCell(new Phrase("Descripcion", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaPrecio = new PdfPCell(new Phrase("Precio", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaTotal = new PdfPCell(new Phrase("Total", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

            tblDetalleVenta.AddCell(CeldaCantidad);
            tblDetalleVenta.AddCell(CeldaProducto);
            tblDetalleVenta.AddCell(CeldaDescripcion);
            tblDetalleVenta.AddCell(CeldaPrecio);
            tblDetalleVenta.AddCell(CeldaTotal);

            List<E_DetalleVenta> DV = NDV.LstBuscaDetalle(IdVentaRealizada);

            foreach (E_DetalleVenta DVenta in DV)
            {
                E_Producto P = NP.BuscaProducto(DVenta.Producto);
                tblDetalleVenta.SpacingAfter = 5f;
                tblDetalleVenta.SpacingAfter = 5f;

                CeldaCantidad = new PdfPCell(new Phrase(DVenta.Cantidad.ToString(), fontNormal)) { BorderWidth = 0 };
                CeldaProducto = new PdfPCell(new Phrase(P.Nombre, fontNormal)) { BorderWidth = 0 };
                CeldaDescripcion = new PdfPCell(new Phrase(P.Descripcion, fontNormal)) { BorderWidth = 0 };
                CeldaPrecio = new PdfPCell(new Phrase(P.Precio, fontNormal)) { BorderWidth = 0 };
                CeldaTotal = new PdfPCell(new Phrase(DVenta.ImporteTotal, fontNormal)) { BorderWidth = 0 };

                tblDetalleVenta.AddCell(CeldaCantidad);
                tblDetalleVenta.AddCell(CeldaProducto);
                tblDetalleVenta.AddCell(CeldaDescripcion);
                tblDetalleVenta.AddCell(CeldaPrecio);
                tblDetalleVenta.AddCell(CeldaTotal);
            }

            Documento.Add(tblDetalleVenta);


            //***************************************************** VENTAS GENERALES ********************************************************************************************

            PdfPTable tblVentaCliente = new PdfPTable(3) { WidthPercentage = 100 };
            float[] anchoCeldasVenta = { 2.2F, 4F, 3F };
            tblVentaCliente.SetWidths(anchoCeldasVenta);

            PdfPCell CeldaCod = new PdfPCell(new Phrase("CodVenta", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaTotalProdcutos = new PdfPCell(new Phrase("No. Productos", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };
            PdfPCell CeldaImporteTotal = new PdfPCell(new Phrase("Importe Total", fontBold)) { BorderWidth = 0, BorderWidthBottom = 0.75f };

            tblVentaCliente.AddCell(CeldaCod);
            tblVentaCliente.AddCell(CeldaTotalProdcutos);
            tblVentaCliente.AddCell(CeldaImporteTotal);


            tblVentaCliente.SpacingAfter = 5f;
            tblVentaCliente.SpacingAfter = 5f;

            CeldaCod = new PdfPCell(new Phrase(VentaRealizada.CodVenta, fontNormal)) { BorderWidth = 0 };
            CeldaTotalProdcutos = new PdfPCell(new Phrase(VentaRealizada.CantidadProducto.ToString(), fontNormal)) { BorderWidth = 0 };
            CeldaImporteTotal = new PdfPCell(new Phrase(VentaRealizada.Total, fontNormal)) { BorderWidth = 0 };

            tblVentaCliente.AddCell(CeldaCod);
            tblVentaCliente.AddCell(CeldaTotalProdcutos);
            tblVentaCliente.AddCell(CeldaImporteTotal);

            Documento.Add(tblVentaCliente);

            //***********************************************************************************************************************************************************************


            Documento.Close();
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.TransmitFile(@"C:\Reportes\ReporteVenta" + VentaRealizada.CodVenta + ".pdf");
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
        #endregion

        #region VENTAS - Busqueda Ventas
        protected void Inicio_SelectionChanged(object sender, EventArgs e)
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

        protected void VerCalenFin_Click(object sender, EventArgs e)
        {
            if (CalendarioFin.Visible)
                CalendarioFin.Visible = false;
            else
                CalendarioFin.Visible = true;
        }

        protected void CalendarioFin_SelectionChanged(object sender, EventArgs e)
        {
            tbFin.Text = CalendarioFin.SelectedDate.ToShortDateString();
        }

        protected void BtnBuscarFechas_Click(object sender, EventArgs e)
        {

            if (tbInicio.Text == "" || tbFin.Text == "")
            {
                lbRespuestaVentas.Text = "";
                RespuestaBusquedaVentas.Attributes.Clear();
                RespuestaBusquedaVentas.Attributes.Add("class", "row alert-danger text-center");
                lbRespuestaVentas.Text = "No se ha seleccionado un periodo de fechas";
                return;
            }

            List<E_Ventas> LstVentas = NV.LstVentasPorPeriodo(tbInicio.Text, tbFin.Text);
            if (LstVentas.Count == 0)
            {
                lbRespuestaVentas.Text = "";
                RespuestaBusquedaVentas.Attributes.Clear();
                RespuestaBusquedaVentas.Attributes.Add("class", "row alert-warning text-center");
                lbRespuestaVentas.Text = "NO HAY REGISTROS DE VENTAS EN EL INTERVALO INDICADO";
                GrvRegVentas.DataSource = NV.DT_LstVentaPeriodo(tbInicio.Text, tbFin.Text);
                GrvRegVentas.DataBind();
            }
            else
            {
                lbRespuestaVentas.Text = "";
                GrvRegVentas.DataSource = NV.DT_LstVentaPeriodo(tbInicio.Text, tbFin.Text);
                GrvRegVentas.DataBind();
            }
        }

        protected void BtnRestablecerRegVentas_Click(object sender, EventArgs e)
        {
            lbRespuestaVentas.Text = "";
            ON_RegVentas();
        }
        #endregion

        #region VENTAS Eliminar | Detalles
        protected void GrvRegVentas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int f = 0;
            hfIdVenta.Value = GrvRegVentas.DataKeys[e.RowIndex].Value.ToString();
            while (f < GrvRegVentas.Rows.Count)
            {
                GrvRegVentas.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }

            GrvRegVentas.Rows[e.RowIndex].Attributes.Add("class", "alert-danger");
            e.Cancel = true;
            E_Ventas EV = NV.BuscaVenta(Convert.ToInt32(hfIdVenta.Value));
            tbBorrar.Text = EV.CodVenta;


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalBorrar').modal();", true);

        }

        protected void btnConfirmarBorrado_Click(object sender, EventArgs e)
        {
            string R;
            if (tbBorrar.Text == "")
            {
                lbRegVentas.Text = "No se encontró en la base de datos";
                return;
            }
            E_Ventas EV = NV.BuscaVenta(Convert.ToInt32(hfIdVenta.Value));

            if (EV != null)
            {
                R = NV.BorraVenta(EV.IdVenta);
                lbRegVentas.Text = R;
                if (lbRegVentas.Text.Contains("Exito"))
                {
                    MensajeVenta.Attributes.Clear();
                    MensajeVenta.Attributes.Add("class", " row-cols-auto alert-success");
                }
                else
                {
                    MensajeVenta.Attributes.Clear();
                    MensajeVenta.Attributes.Add("class", " row-cols-auto alert-danger");
                }
                visualizarGrvRegVentas();
            }
            else
            {
                lbRegVentas.Text = "No se encontró en la base de datos";
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
        #endregion

        #region GCLIENTE
        protected void BtnBusquedaCliente_Click(object sender, EventArgs e)
        {
            OFF_Label();
            List<E_Cliente> LstCliente = NC.LstBuscaCliente(tbBuscaCliente.Text);

            if (hfCualModuloCliente.Value == "1") //USUARIOS
            {
                List<E_Usuario> LstUsu = NU.LstBuscaUsuario(tbBuscaCliente.Text);

                if (LstUsu.Count == 0)
                {
                    lbRegClientes.Text = "NO HAY COINCIDENCIA EN LA BASE DE DATOS";
                    visualizarGrvUsuario(LstUsu);
                    lbRegClientes.Visible = true;
                }
                else
                {
                    visualizarGrvUsuario(LstUsu);
                    lbRegClientes.Visible = false;
                }
            }
            else if (hfCualModuloCliente.Value == "2") //DEUDAS ACTUALES
            {
                if (LstCliente.Count == 0)
                {
                    lbRegClientes.Text = "NO HAY COINCIDENCIA EN LA BASE DE DATOS";
                    visualizarGrvDeudaClientes(LstCliente);
                    lbRegClientes.Visible = true;
                }
                else
                {
                    visualizarGrvDeudaClientes(LstCliente);
                    lbRegClientes.Visible = false;
                }
            }
            else
            {
                if (LstCliente.Count == 0) //CLIENTES
                {
                    lbRegClientes.Text = "NO HAY COINCIDENCIA EN LA BASE DE DATOS";
                    visualizarGrvCliente(LstCliente);
                    lbRegClientes.Visible = true;
                }
                else
                {
                    visualizarGrvCliente(LstCliente);
                    lbRegClientes.Visible = false;
                }
            }
        }

        protected void BtnNuevoCliente_Click(object sender, EventArgs e)
        {
            InicializarTBClientes();
            pAgregarNuevoCliente.Visible = true;
            pRegClientes.Visible = false;
            BtnModificarCliente.Visible = false;
            BtnRegistrarCliente.Visible = true;

            ResultNuevoCliente.Attributes.Clear();
            lbResultadoNuevoCliente.Text = "";
            lbRegClientes.Visible = false;

            header.Text += "<h2 class=\"card-header bg-success text-white\">Nuevo Cliente</h2>";
            ptitulocliente.Controls.Add(header);
        }

        protected void InicializarTBClientes()
        {
            tbCodigoDeCliente.Text = string.Empty;
            tbNombreDeCliente.Text = string.Empty;
            tbApellidoDeCliente.Text = string.Empty;
            tbDireccionDeCliente.Text = string.Empty;
            tbTelefonoDeCliente.Text = string.Empty;
            tbCorreoDeCliente.Text = string.Empty;
            tbUsuarioDeCliente.Text = string.Empty;
            tbPasswordDeUsuario.Text = string.Empty;
            tbCreditoDeCliente.Text = string.Empty;
            tbCuentaActualDeCliente.Text = "0";
        }

        protected E_Cliente ControlesWebForm_ObjetoEntidadCliente()
        {
            int _IdCliente = 0;
            E_Cliente Cliente = new E_Cliente
            {
                IdCliente = _IdCliente,
                CodCliente = tbCodigoDeCliente.Text,
                Nombre = tbNombreDeCliente.Text.ToUpper(),
                Apellidos = tbApellidoDeCliente.Text.ToUpper(),
                Direccion = tbDireccionDeCliente.Text.ToUpper(),
                Telefono = tbTelefonoDeCliente.Text,
                Correo = tbCorreoDeCliente.Text.ToLower(),
                Usuario = tbUsuarioDeCliente.Text.ToUpper(),
                Contrasena = tbPasswordDeUsuario.Text,
                Credito = Convert.ToDecimal(tbCreditoDeCliente.Text),
                CuentaActual = Convert.ToDecimal(tbCuentaActualDeCliente.Text)
            };

            return Cliente;
        }

        protected bool verificarUsuarioRepetido(E_Cliente nuevoCliente)
        {
            List<E_Usuario> UsuariosActuales = NU.LstUsuarios();
            if (UsuariosActuales.Count > 0)
            {
                foreach (E_Usuario Usu in UsuariosActuales)
                {
                    if (Usu.NombreUsuario.ToUpper() == nuevoCliente.Usuario.ToUpper())
                        return true;
                }
            }
            return false;
        }

        protected E_Usuario RegistrarUsuario(E_Cliente Cliente)
        {
            int _IdUsuario = 0;
            E_Usuario Usuario = new E_Usuario
            {
                IdUsuario = _IdUsuario,
                Tipo = 2,
                NombreUsuario = Cliente.Usuario.ToUpper(),
                ContrasenaUsuario = Cliente.Contrasena
            };
            return Usuario;
        }

        protected void BtnRegistrarCliente_Click(object sender, EventArgs e)
        {
            header.Text += "<h2 class=\"card-header bg-success text-white\">Nuevo Cliente</h2>";
            ptitulocliente.Controls.Add(header);

            string R, U;
            N_Cliente NC = new N_Cliente();
            E_Cliente NuevoCliente = ControlesWebForm_ObjetoEntidadCliente();
            E_Usuario NuevoUsuario = RegistrarUsuario(NuevoCliente);

            if (verificarUsuarioRepetido(NuevoCliente))
            {
                ResultNuevoCliente.Attributes.Clear();
                ResultNuevoCliente.Attributes.Add("class", " row-cols-auto alert-danger text-center");
                lbResultadoNuevoCliente.Text = "Error: Usuario actualmente en uso";
                return;
            }

            R = NC.InsertaCliente(NuevoCliente);
            U = NU.InsertaUsuario(NuevoUsuario);

            if (R.Contains("Exito") && U.Contains("Exito"))
            {
                ResultNuevoCliente.Attributes.Clear();
                ResultNuevoCliente.Attributes.Add("class", " row-cols-auto alert-success text-center");
            }
            else
            {
                ResultNuevoCliente.Attributes.Clear();
                ResultNuevoCliente.Attributes.Add("class", " row-cols-auto alert-danger text-center");
            }
            lbResultadoNuevoCliente.Text = R;
            visualizarGrvCliente();
            InicializarTBClientes();
        }

        protected void BtnCancelarRegistroCliente_Click(object sender, EventArgs e)
        {
            pAgregarNuevoCliente.Visible = false;
            pRegClientes.Visible = true;

            ResultNuevoCliente.Attributes.Clear();
            lbResultadoNuevoCliente.Visible = false;
        }

        protected void BtnCreditos_Click(object sender, EventArgs e)
        {
            OFF_Label();
            ResultNuevoCliente.Attributes.Clear();

            BtnNuevoCliente.Enabled = true;

            tbBuscaCliente.Attributes.Clear();
            tbBuscaCliente.Attributes.Add("PlaceHolder", "Busca Usuario");

            ON_RegUsuarios();
            GrvUsuario.Visible = true;
            GrvCliente.Visible = false;
            GrvDeudasCliente.Visible = false;
            hfCualModuloCliente.Value = "1";
        }
        #endregion

        #region CLIENTE Liquidar-Cliente
        protected void BtnLiquidar_Click(object sender, EventArgs e)
        {
            OFF_Label();
            ResultNuevoCliente.Attributes.Clear();

            tbBuscaCliente.Attributes.Clear();
            tbBuscaCliente.Attributes.Add("PlaceHolder", "Busca Cliente");

            BtnNuevoCliente.Enabled = false;

            visualizarGrvDeudaClientes();
            GrvDeudasCliente.Visible = true;
            GrvCliente.Visible = false;
            GrvUsuario.Visible = false;
            hfCualModuloCliente.Value = "2";
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            OFF_Label();
            ON_GClientes();

            BtnNuevoCliente.Enabled = true;

            GrvUsuario.Visible = false;
            GrvDeudasCliente.Visible = false;
            GrvCliente.Visible = true;
            hfCualModuloCliente.Value = "0";
        }

        protected void GrvDeudasCliente_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int f = 0;

            hfIdCliente.Value = GrvCliente.DataKeys[e.NewEditIndex].Value.ToString();

            while (f < GrvDeudasCliente.Rows.Count)
            {
                GrvDeudasCliente.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }
            GrvDeudasCliente.Rows[e.NewEditIndex].Attributes.Add("class", "alert-success");

            e.Cancel = true;
            E_Cliente EC = NC.BuscaCliente(Convert.ToInt16(hfIdCliente.Value));
            ObjetoEntidadCliente_ControlesWebForm(EC);
            tbNombresDeuda.Text = EC.Nombre + " " + EC.Apellidos;
            tbCuentaDeuda.Text = EC.CuentaActual.ToString();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalAbonoCliente').modal();", true);
        }

        protected void ConfirmarAbono_Click(object sender, EventArgs e)
        {
            string R;

            if (tbAbonar.Text == "")
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", "row-cols-auto d-flex justify-content-center justify-items-center alert-danger");
                lbRegClientes.Text = "Faltan datos por ingresar";
                lbRegClientes.Visible = true;
                return;
            }

            decimal cuentaActual = Convert.ToDecimal(tbCuentaActualDeCliente.Text);
            decimal Abono = Convert.ToDecimal(tbAbonar.Text);
            if (hfIdCliente.Value != "")
            {
                if (Abono <= cuentaActual)
                {
                    R = NC.ModificaCliente(ControlesWebForm_ObjetoEntidadLiquidarCliente(Convert.ToInt32(hfIdCliente.Value), cuentaActual, Abono));
                    RespuestaCliente.Attributes.Clear();
                    RespuestaCliente.Attributes.Add("class", "row-cols-auto d-flex justify-content-center justify-items-center alert-success");
                    lbRegClientes.Text = R;
                }
                else
                {
                    RespuestaCliente.Attributes.Clear();
                    RespuestaCliente.Attributes.Add("class", "row-cols-auto d-flex justify-content-center justify-items-center alert-danger");
                    lbRegClientes.Text = "Abono mayor a la cuenta actual";
                }

            }
            else
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", "row-cols-auto d-flex justify-content-center justify-items-center alert-danger");
                lbRegClientes.Text = "No se encontró en la Base de Datos";
            }
            lbRegClientes.Visible = true;
            BtnNuevoCliente.Enabled = true;
            tbAbonar.Text = "";

            InicializarTBClientes();
            visualizarGrvDeudaClientes();
        }
        #endregion

        #region CLIENTE Eliminar | Modificar | EnviarCorreo
        protected void GrvCliente_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int f = 0;
            lbRegClientes.Visible = false;
            hfIdCliente.Value = GrvCliente.DataKeys[e.RowIndex].Value.ToString();
            e.Cancel = true;
            E_Cliente EC = NC.BuscaCliente(Convert.ToInt16(hfIdCliente.Value));

            while (f < GrvCliente.Rows.Count)
            {
                GrvCliente.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }
            GrvCliente.Rows[e.RowIndex].Attributes.Add("class", "alert-danger");
            tbBorrarCliente.Text = EC.CodCliente;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalBorrarCliente').modal();", true);
        }

        protected void btnConfirmarBorradoCliente_Click(object sender, EventArgs e)
        {
            string R;
            E_Cliente EC = NC.BuscaCliente(Convert.ToInt16(hfIdCliente.Value));
            E_Usuario U = NU.BuscaUsuario(EC.Usuario);

            if (EC != null)
            {
                R = NC.BorraCliente(EC.IdCliente);
                NU.BorraUsuario(U.IdUsuario);
                lbRegClientes.Text = R;
                RespuestaCliente.Attributes.Clear();
                if (lbRegClientes.Text.Contains("Exito"))
                {
                    RespuestaCliente.Attributes.Clear();
                    RespuestaCliente.Attributes.Add("class", " row-cols-auto alert-success text-center");
                }
                else
                {
                    RespuestaCliente.Attributes.Clear();
                    RespuestaCliente.Attributes.Add("class", " row-cols-auto alert-danger text-center");
                }
                lbRegClientes.Visible = true;
                visualizarGrvCliente();
            }
            else
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", "row-col-auto alert-danger text-center");
                lbRegClientes.Text = "No se encontró en la base de datos";
                lbRegClientes.Visible = true;
            }
        }

        protected void ObjetoEntidadCliente_ControlesWebForm(E_Cliente C) //copiamos los valores del Objeto a los textbox's
        {
            string credito_int = C.Credito.ToString().Substring(0, C.Credito.ToString().Length - 3);
            hfIdCliente.Value = C.IdCliente.ToString();
            tbCodigoDeCliente.Text = C.CodCliente;
            tbNombreDeCliente.Text = C.Nombre;
            tbApellidoDeCliente.Text = C.Apellidos;
            tbDireccionDeCliente.Text = C.Direccion;
            tbTelefonoDeCliente.Text = C.Telefono;
            tbCorreoDeCliente.Text = C.Correo;
            tbUsuarioDeCliente.Text = C.Usuario.ToUpper();
            tbPasswordDeUsuario.Text = C.Contrasena; //no carga la contraseña por TextMode=Password
            tbCreditoDeCliente.Text = credito_int;
            tbCuentaActualDeCliente.Text = C.CuentaActual.ToString();
        }

        protected void GrvCliente_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int f = 0;
            OFF_Label();
            BtnModificarCliente.Visible = true;
            BtnRegistrarCliente.Visible = false;
            hfIdCliente.Value = GrvCliente.DataKeys[e.NewEditIndex].Value.ToString();
            e.Cancel = true;

            while (f < GrvCliente.Rows.Count)
            {
                GrvCliente.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }
            GrvCliente.Rows[e.NewEditIndex].Attributes.Add("class", "alert-primary");

            pAgregarNuevoCliente.Visible = true;
            pRegClientes.Visible = false;
            E_Cliente EC = NC.BuscaCliente(Convert.ToInt16(hfIdCliente.Value));
            ObjetoEntidadCliente_ControlesWebForm(EC);

            header.Text += "<h2 class=\"card-header bg-primary text-white\">Modificar Cliente</h2>";
            ptitulocliente.Controls.Add(header);
        }

        protected E_Cliente ControlesWebForm_ObjetoEntidadCliente(int IdCliente) //movemos los valores de textbox's modificados al objeto
        {
            int _IdCliente = IdCliente;
            E_Cliente Cliente = new E_Cliente
            {
                IdCliente = _IdCliente,
                CodCliente = tbCodigoDeCliente.Text,
                Nombre = tbNombreDeCliente.Text.ToUpper(),
                Apellidos = tbApellidoDeCliente.Text.ToUpper(),
                Direccion = tbDireccionDeCliente.Text,
                Telefono = tbTelefonoDeCliente.Text,
                Correo = tbCorreoDeCliente.Text,
                Usuario = tbUsuarioDeCliente.Text.ToUpper(),
                Contrasena = tbPasswordDeUsuario.Text,
                Credito = Convert.ToDecimal(tbCreditoDeCliente.Text),
                CuentaActual = Convert.ToDecimal(tbCuentaActualDeCliente.Text)
            };
            return Cliente;
        }

        protected E_Usuario ModificarUsuario(E_Usuario UActual, E_Cliente CModificado)
        {
            E_Usuario Usuario = new E_Usuario
            {
                IdUsuario = UActual.IdUsuario,
                Tipo = UActual.Tipo,
                NombreUsuario = CModificado.Usuario.ToUpper(),
                ContrasenaUsuario = CModificado.Contrasena
            };
            return Usuario;
        }

        protected void BtnModificarCliente_Click(object sender, EventArgs e)
        {
            header.Text += "<h2 class=\"card-header bg-success text-white\">Nuevo Cliente</h2>";
            ptitulocliente.Controls.Add(header);

            string R;
            E_Usuario UsuarioPrevio = NU.BuscaUsuario(NC.BuscaCliente(Convert.ToInt32(hfIdCliente.Value)).Usuario);
            E_Cliente ClienteModificado = ControlesWebForm_ObjetoEntidadCliente(Convert.ToInt32(hfIdCliente.Value));
            E_Usuario UsuarioModificado = ModificarUsuario(UsuarioPrevio, ClienteModificado);
            if (UsuarioPrevio.NombreUsuario != UsuarioModificado.NombreUsuario)
            {
                if (verificarUsuarioRepetido(ClienteModificado))
                {
                    ResultNuevoCliente.Attributes.Clear();
                    ResultNuevoCliente.Attributes.Add("class", " row-cols-auto alert-danger text-center");
                    lbResultadoNuevoCliente.Text = "Error: Usuario actualmente en uso";
                    lbResultadoNuevoCliente.Visible = true;
                    //header.Text += "<h2 class=\"card-header bg-primary text-white\">Modificar Cliente</h2>";
                    return;
                }
            }

            if (hfIdCliente.Value != "")
            {
                R = NC.ModificaCliente(ClienteModificado);
                NU.ModificaUsuario(UsuarioModificado);
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", "row-cols-auto alert-success text-center");
                lbRegClientes.Text = R;
            }
            else
            {
                lbRegClientes.Text = "No se encontró en la Base de Datos";
            }

            lbRegClientes.Visible = true;
            InicializarTBClientes();
            visualizarGrvCliente();
            BtnModificarCliente.Visible = false;
            BtnRegistrarCliente.Visible = true;
            pAgregarNuevoCliente.Visible = false;
            pRegClientes.Visible = true;
        }

        protected E_Cliente ControlesWebForm_ObjetoEntidadLiquidarCliente(int IdCliente, decimal Actual, decimal abono) //movemos los valores de textbox's modificados al objeto
        {
            int _IdCliente = IdCliente;

            E_Cliente Cliente = new E_Cliente
            {
                IdCliente = _IdCliente,
                CodCliente = tbCodigoDeCliente.Text,
                Nombre = tbNombreDeCliente.Text,
                Apellidos = tbApellidoDeCliente.Text,
                Direccion = tbDireccionDeCliente.Text,
                Telefono = tbTelefonoDeCliente.Text,
                Correo = tbCorreoDeCliente.Text,
                Usuario = tbUsuarioDeCliente.Text,
                Contrasena = tbPasswordDeUsuario.Text,
                Credito = Convert.ToDecimal(tbCreditoDeCliente.Text),
                CuentaActual = (Actual - abono)
            };
            return Cliente;
        }

        protected void GrvCliente_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int f = 0;
            hfIdCliente.Value = GrvCliente.DataKeys[e.RowIndex].Value.ToString();
            e.Cancel = true;

            while (f < GrvCliente.Rows.Count)
            {
                GrvCliente.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }
            GrvCliente.Rows[e.RowIndex].Attributes.Add("class", "alert-success");

            E_Cliente EC = NC.BuscaCliente(Convert.ToInt16(hfIdCliente.Value));

            string Mensaje = "Ahora eres cliente de Abarrotes Emmanuel." + Chunk.NEWLINE + Chunk.NEWLINE + "Usuario: " + EC.Usuario + Chunk.NEWLINE + "Contraseña: " + EC.Contrasena + Chunk.NEWLINE + Chunk.NEWLINE + "Nos da gusto tenerte como Cliente. Con los datos adjuntos podrás acceder a tu cuenta en la página oficial." + Chunk.NEWLINE + "¡Bienvenido!";
            if (EnviarCorreo("Bienvenido - Abarrotes Emmanuel", Mensaje, EC.Correo))
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", "row-cols-auto alert-success text-center");
                lbRegClientes.Text = "Los datos del Cliente fueron enviados con éxito";
            }
            else
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", "row-cols-auto alert-danger text-center");
                lbRegClientes.Text = "Los datos del Cliente NO fueron enviados";
            }
            lbRegClientes.Visible = true;
        } //mandar por correo Usuario | Contraseña

        protected bool EnviarCorreo(string pAsunto, string pMensaje, string pDestino)
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
                    Email.From = new MailAddress("correo.pruebaschool@gmail.com", "Administrador de sistema"); //Address es el correo de donde se enviara el correo
                    Email.Subject = pAsunto;
                    Email.Body = pMensaje;
                    Email.To.Add(pDestino); //agregamos el destino

                    smtpServer.Port = 587; //puerto utilizado para servidor Gmail
                    smtpServer.Credentials = new NetworkCredential("correo.pruebaschool@gmail.com", "pruebasdeenvio");
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
        #endregion

        #region CLIENTE Usuarios
        protected void GrvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int f = 0;
            tbEditContrasena.Text = string.Empty;
            tbEditUsuario.Text = string.Empty;

            hfIdUsuario.Value = GrvUsuario.DataKeys[e.NewEditIndex].Value.ToString();
            e.Cancel = true;
            while (f < GrvUsuario.Rows.Count)
            {
                GrvUsuario.Rows[f].Attributes.Add("class", "alert-light");
                f++;
            }
            GrvUsuario.Rows[e.NewEditIndex].Attributes.Add("class", "alert-warning");
            E_Usuario Usuario = NU.BuscaUsuario(Convert.ToInt32(hfIdUsuario.Value));
            tbEditUsuarioActual.Text = Usuario.NombreUsuario;
            tbEditContrasenaActual.Text = Usuario.ContrasenaUsuario;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#mymodal", "$('#ModalEditaUsuario').modal();", true);
        }

        protected void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            string R;
            lbRegClientes.Text = string.Empty;
            if (tbEditContrasena.Text == "" || tbEditUsuario.Text == "")
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", " row-cols-auto alert-danger text-center");
                lbRegClientes.Text = "No debe dejar campos vacíos";
                lbRegClientes.Visible = true;
                return;
            }
            E_Usuario UActual = NU.BuscaUsuario(Convert.ToInt32(hfIdUsuario.Value));
            E_Cliente UsuarioCliente = NC.BuscaCliente(UActual.NombreUsuario);
            R = ModificarUsuarioDesdeUsuarios(UsuarioCliente, UActual);
            if (R.Contains("Exito"))
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", " row-cols-auto alert-success text-center");
                lbRegClientes.Text = R;
            }
            else
            {
                RespuestaCliente.Attributes.Clear();
                RespuestaCliente.Attributes.Add("class", " row-cols-auto alert-danger text-center");
                lbRegClientes.Text = "El Usuario ya existe";
            }
            lbRegClientes.Visible = true;
            tbEditContrasena.Text = string.Empty;
            tbEditContrasenaActual.Text = string.Empty;
            tbEditUsuario.Text = string.Empty;
            tbEditUsuarioActual.Text = string.Empty;
        }

        protected string ModificarUsuarioDesdeUsuarios(E_Cliente ClienteActual, E_Usuario UsuarioActual)
        {
            string R;
            E_Cliente ClienteModificado = ClienteActual;
            ClienteModificado.Usuario = tbEditUsuario.Text.ToUpper();
            ClienteModificado.Contrasena = tbEditContrasena.Text;
            E_Usuario UsuarioModificado = ModificarUsuario(UsuarioActual, ClienteModificado);
            if (UsuarioActual.NombreUsuario != UsuarioModificado.NombreUsuario)
            {
                if (verificarUsuarioRepetido(ClienteModificado))
                {
                    return "Error";
                }
            }

            NC.ModificaCliente(ClienteModificado);
            R = NU.ModificaUsuario(UsuarioModificado);
            visualizarGrvCliente();
            visualizarGrvUsuarios();
            return R;
        }
        #endregion


    }
}