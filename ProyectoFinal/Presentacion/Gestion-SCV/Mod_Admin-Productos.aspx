<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MP_Admin.master" AutoEventWireup="true" CodeBehind="Mod_Admin-Productos.aspx.cs" Inherits="Presentacion.Gestion_SCV.Mod_Admin_Productos" %>

<%@ Register Src="~/Controls/wucCualquierDatoRequerido.ascx" TagPrefix="uc1" TagName="wucCualquierDatoRequerido" %>
<%@ Register Src="~/Controls/wucAlfaNumericoRequerido.ascx" TagPrefix="uc1" TagName="wucAlfaNumericoRequerido" %>
<%@ Register Src="~/Controls/wucPrecioRequerido.ascx" TagPrefix="uc1" TagName="wucPrecioRequerido" %>
<%@ Register Src="~/Controls/wucNumeroRequerido.ascx" TagPrefix="uc1" TagName="wucNumeroRequerido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="server">
    <br />
    <%-------------------------------------------------------------------- GPRODUCTOS -----------------------------------------------------------------------%>
    <div class="container-fluid d-flex justify-content-center">
        <asp:Panel ID="pRegProductos" runat="server">
            <div class="row">
                <div class="col-auto">
                    <div>
                        <div class="card">
                            <h2 class="card-header text-center bg-success text-white border-3 shadow-lg">Registro Productos</h2>
                            <div class="card-body">|
                                <div class="row-col-auto form-group font-weight-bold d-inline-flex">
                                    <i class="das da-at mr-2"></i>Buscar&nbsp; 
                                    <asp:TextBox ID="tbBuscaProducto" runat="server" ToolTip="Producto" Width="100%" placeHolder="Buscar Producto"></asp:TextBox>&nbsp&nbsp;
                                    <asp:LinkButton ID="BtnBusquedaProducto" runat="server" CssClass="btn btn-primary" CausesValidation="false" OnClick="BtnBusquedaProducto_Click"><i class="fas fa-search mr-2"></i></asp:LinkButton>&nbsp&nbsp;
                                </div>
                                <div id="lbbody" runat="server" class="row-cols-auto alert-danger">
                                    <asp:Label ID="lbRegProductos" runat="server" Visible="False"></asp:Label>
                                </div>

                                <div class="row-col-auto table-responsive">
                                    <asp:GridView ID="GrvProducto" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IdProducto" OnRowDeleting="GrvProducto_RowDeleting" OnRowEditing="GrvProducto_RowEditing">

                                        <Columns>
                                            <asp:BoundField DataField="CodProducto" HeaderText="CodProducto" SortExpression="CodProducto" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                            <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                                            <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvEditarProducto" runat="server" ToolTip="Editar Producto" CommandName="Edit" CssClass="btn btn-light text-primary"><i class="fas fa-pencil-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvBorrarProducto" runat="server" ToolTip="Borrar Producto" CommandName="Delete" CssClass="btn btn-light text-danger"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-auto mt-5 align-top">
                    <asp:LinkButton ID="BtnNuevoProducto" runat="server" CssClass="btn btn-primary d-block mt-3" OnClick="BtnNuevoProducto_Click"><i class="fas fa-user-plus"></i>&nbsp Nuevo</asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
    </div>

   <%-- AGREGAR PRODUCTO--%>
    <div class="container-fluid">
        <asp:Panel ID="pAgregarNuevoProducto" runat="server">
            <div class="row-col-12">
                <div class="">
                    <asp:Panel ID="ptituloproducto" runat="server">
                        <asp:Literal ID="header" runat="server"></asp:Literal>
                    </asp:Panel>
                    <div class="card-body">
                        <div class="modal-dialog modal-xl shadow" role="document">
                            <div class="modal-content">
                                <h3 class="modal-header">Detalle Producto</h3>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-2">
                                            <asp:Label ID="lbCodProducto" runat="server" Text="Código"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucCualquierDatoRequerido runat="server" ID="tbCodDeProducto" />
                                            </div>
                                        </div>
                                        <div class="col-4">
                                            <asp:Label ID="lbNombreDeProducto" runat="server" Text="Nombre"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucAlfaNumericoRequerido runat="server" ID="tbNombreDeProducto" />

                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label ID="lbDescripcionDeProducto" runat="server" Text="Descripción"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucAlfaNumericoRequerido runat="server" ID="tbDescripcionDeProducto" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-2">
                                            <asp:Label ID="lbPrecioDeProducto" runat="server" Text="Precio Unitario"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucPrecioRequerido runat="server" ID="tbPrecioDeProducto" />
                                            </div>
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lbStockDeProducto" runat="server" Text="Stock"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucNumeroRequerido runat="server" ID="tbStockDeProducto" />
                                            </div>
                                        </div>
                                        <div class="col-8">
                                            <asp:Panel ID="pnlCapturaArchivo" runat="server">
                                                <div>
                                                    <asp:FileUpload ID="fuArchivo" runat="server" Width="300px" />
                                                    <div>
                                                        <asp:Label ID="lbResultadoDeCargarImagen" CssClass="alert-danger" runat="server" Text="" Visible="false"></asp:Label>
                                                    </div>
                                                    <div id="selecImg" runat="server">
                                                        <asp:CheckBox ID="NoCambiar" runat="server" Checked="true" OnCheckedChanged="NoCambiar_CheckedChanged" />
                                                        <label class="form-check-label" for="NoCambiar">Mantener Imagen</label>
                                                    </div>

                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div id="CardFooter" runat="server">
                                    <asp:Label ID="lbResultadoRegistrarProducto" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="BtnCancelarRegistroProducto" runat="server" CssClass="btn btn-danger" CausesValidation="False" OnClick="BtnCancelarRegistroProducto_Click"><i class="fas fa-backspace"></i>&nbsp Cancelar</asp:LinkButton>
                                    <asp:LinkButton ID="BtnRegistrarProducto" runat="server" CssClass="btn btn-success" OnClick="BtnRegistrarProducto_Click"><i class="fas fa-plus-circle"></i>&nbsp Registrar</asp:LinkButton>
                                    <asp:LinkButton ID="BtnModificarProducto" runat="server" CssClass="btn btn-success" Visible="false" OnClick="BtnModificarProducto_Click"><i class="fas fa-check"></i>&nbsp Modificar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>


    <%-------------------------------------------------------------------- INVENTARIO -----------------------------------------------------------------------%>
    <div class="container-fluid d-flex justify-content-center">
        <asp:Panel ID="pRegInventario" runat="server">
            <div class="row">
                <div class="col-auto">
                    <div>
                        <div class="card">
                            <h2 class="card-header text-center bg-success text-white border-5 shadow-lg">Inventario Productos</h2>
                            <div class="card-body">

                                <div class="row-col-auto form-group font-weight-bold d-flex align-items-center">
                                    <i class="das da-at mr-2"></i>Cantidad Minima&nbsp; 
                                    <uc1:wucNumeroRequerido runat="server" ID="tbInventarioMin" /><br />&nbsp; &nbsp; 
                                    <asp:LinkButton ID="BtnInventarioMin" runat="server" CssClass="btn btn-primary" CausesValidation="true" OnClick="BtnInventarioMin_Click"><i class="fas fa-search mr-2"></i></asp:LinkButton>&nbsp&nbsp;
                                </div>
                                <div class="row-cols-auto">
                                    <asp:LinkButton ID="btnInventarioBajo" CssClass="btn btn-link" runat="server" OnClick="btnInventarioBajo_Click" CausesValidation="False"><i class="fas fa-print"></i>Imprimir Inventario</asp:LinkButton>
                                </div>
                                <div class="row-cols-auto alert-danger">
                                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                                </div>
                                <div class="row-col-12 table-responsive">
                                    <asp:GridView ID="GrvInventario" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IdProducto">

                                        <Columns>
                                            <asp:BoundField DataField="CodProducto" HeaderText="CodProducto" SortExpression="CodProducto" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                            <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                        </Columns>

                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>


    <%-------------------------------------------------------------------- MODALES -----------------------------------------------------------------------%>
    <%--BORRAR PRODUCTO--%>
    <div class="modal fade" id="ModalBorrarProducto" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-danger d-flex">
                    <h5 class="modal-title">¿Desea borrar archivo seleccionado?</h5>
                    <asp:LinkButton ID="CerrarBorrarProducto" runat="server" CssClass="btn-close" data-bs-dismiss="modal" aria-label="Close"></asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text">CODIGO PRODUCTO</span>
                        <asp:TextBox ID="tbBorrarProducto" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="cancelarBorrarProducto" CssClass="btn btn-outline-dark" runat="server" Text="Cancelar" data-bs-dismiss="modal"/>
                        <asp:Button ID="btnConfirmarBorradoProducto" CssClass="btn btn-outline-danger" runat="server" Text="Confirmar" OnClick="btnConfirmarBorradoProducto_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%-------------------------------------------------------------------- HFIELDS -----------------------------------------------------------------------%>
    <asp:HiddenField ID="hfIdProducto" runat="server" />
    <asp:HiddenField ID="hfAgregarOModificar" runat="server" />
    <asp:HiddenField ID="hfNav" runat="server" />
    <asp:HiddenField ID="hfInventarioMin" runat="server" Value="3" />
    <asp:Label ID="LblRespuesta" runat="server" Text=""></asp:Label>

</asp:Content>
