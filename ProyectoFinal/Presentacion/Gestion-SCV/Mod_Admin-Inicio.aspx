<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MP_Admin.master" AutoEventWireup="true" CodeBehind="Mod_Admin-Inicio.aspx.cs" Inherits="Presentacion.Gestion_SCV.Mod_Admin_Inicio" %>

<%@ Register Src="~/Controls/wucAlfaNumericoRequerido.ascx" TagPrefix="uc1" TagName="wucAlfaNumericoRequerido" %>
<%@ Register Src="~/Controls/wucNumeroTelefono.ascx" TagPrefix="uc1" TagName="wucNumeroTelefono" %>
<%@ Register Src="~/Controls/wucCorreoRequerido.ascx" TagPrefix="uc1" TagName="wucCorreoRequerido" %>
<%@ Register Src="~/Controls/wucUsuarioRequerido.ascx" TagPrefix="uc1" TagName="wucUsuarioRequerido" %>
<%@ Register Src="~/Controls/wucContraseñaRequerida.ascx" TagPrefix="uc1" TagName="wucContraseñaRequerida" %>
<%@ Register Src="~/Controls/wucNumeroRequerido.ascx" TagPrefix="uc1" TagName="wucNumeroRequerido" %>
<%@ Register Src="~/Controls/wucPrecioRequerido.ascx" TagPrefix="uc1" TagName="wucPrecioRequerido" %>
<%@ Register Src="~/Controls/wucCualquierDatoRequerido.ascx" TagPrefix="uc1" TagName="wucCualquierDatoRequerido" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="server">

    <div class="container-fluid">
        <br />
        <asp:Panel ID="pVentaPrincipal" runat="server">
            <div class="card">
                <h5 class="card-header alert-primary"></h5>
                <div class="card-body">
                    <%--barra de busqueda--%>
                    <div class="row">
                        <div class="col-11 form-group font-weight-bold d-inline-flex">
                            <i class="das da-at mr-2"></i>Buscar&nbsp; 
                                <asp:TextBox ID="tbInsertaCodProducto" runat="server" CssClass="form-control" ToolTip="Cod-Producto" Width="100%" placeHolder="Ingresa código producto"></asp:TextBox>&nbsp&nbsp;
                                <asp:LinkButton ID="BtnBusqueda" runat="server" CssClass="btn btn-primary" CausesValidation="false" OnClick="BtnBusqueda_Click"><i class="fas fa-search mr-2"></i></asp:LinkButton>&nbsp&nbsp;
                                <asp:LinkButton ID="BtnMostrarTodo" ToolTip="Productos" runat="server" type="button" CssClass="btn btn-success" data-bs-toggle="modal" data-bs-target="#ModalTodoProductos"><i class="fas fa-bars"></i></asp:LinkButton>
                        </div>
                        <div class="col-1  form-group font-weight-bold">
                            <asp:TextBox ID="tbCodVenta" runat="server" CssClass="text-center" placeHolder="CodVenta" Enabled="false" Width="100"></asp:TextBox>
                        </div>
                    </div>
                    <div id="confirmacionParam" runat="server">
                        <asp:Label ID="lbConfirmacionParametros" runat="server" Text=""></asp:Label>
                    </div>

                    <%--detalles cliente--%>
                    <div class="row">
                        <div class="col-5">
                            <asp:Panel ID="pDetalleCliente" runat="server">
                                <div class="card">
                                    <div class="card-body">
                                        <h5>Detalle Cliente</h5>
                                        <br />
                                        <div class="row-col-12">
                                            <asp:CheckBox ID="Credito" runat="server" OnCheckedChanged="Credito_CheckedChanged" AutoPostBack="true" Text=" Crédito " Checked="true" />
                                            &nbsp&nbsp&nbsp;
                                                    
                                                    <div class="d-inline-flex">
                                                        <asp:Label ID="lbDDL" runat="server" Text="Cod-Cliente"></asp:Label>
                                                        <asp:DropDownList ID="ddlNombres" runat="server" DataTextField="Nombre" DataValueField="Nombre"></asp:DropDownList>&nbsp&nbsp;
                                                    <asp:LinkButton ID="btnAgregarCliente" runat="server" CssClass="btn btn-success" CausesValidation="false" OnClick="btnAgregarCliente_Click"><i class="far fa-plus-square"></i></asp:LinkButton>
                                                    </div>

                                            <br />

                                            <asp:Label ID="lbCliente" runat="server" Text="Nombre"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <asp:TextBox ID="tbNombreCliente" runat="server" Width="100%" Enabled="False"></asp:TextBox>
                                                <asp:Label ID="lbSinCliente" runat="server" Text="Venta Ordinaria = {0}" Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        
                        <%--detalles producto--%>
                        <div class="col-7 ">
                            <asp:Panel ID="pDetalleProducto" runat="server">
                                <div class="card">
                                    <div class="card-body">
                                        <h5>Detalle Producto</h5>
                                        <div class="row-col-12 d-flex">
                                            <div class="col-2">
                                                <asp:Label ID="lbCodigo" runat="server" Text="Código"></asp:Label>
                                                <div class="form-group font-weight-bold">
                                                    <asp:TextBox ID="tbCodigoProducto" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-4">
                                                <asp:Label ID="lbNombre" runat="server" Text="Nombre"></asp:Label>
                                                <div class="form-group font-weight-bold">
                                                    <asp:TextBox ID="tbNombreProducto" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <asp:Label ID="lbDescripcion" runat="server" Text="Descripción"></asp:Label>
                                                <div class="form-group font-weight-bold">
                                                    <asp:TextBox ID="tbDescripcionProducto" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row-col-12 d-flex">
                                            <div class="col-3">
                                                <asp:Label ID="lbStock" runat="server" Text="Stock"></asp:Label>
                                                <div class="form-group font-weight-bold">
                                                    <asp:TextBox ID="tbStock" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <asp:Label ID="lbPrecio" runat="server" Text="Precio"></asp:Label>
                                                <div class="form-group font-weight-bold">
                                                    <asp:TextBox ID="tbPrecio" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <asp:Label ID="lbCantidad" runat="server" Text="Cantidad"></asp:Label>
                                                <div class="form-group font-weight-bold">
                                                    <uc1:wucNumeroRequerido runat="server" ID="tbCantidad" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <br />
                                                <asp:LinkButton ID="btnAgregarDetalleVenta" CssClass="btn btn-outline-success" runat="server" OnClick="btnAgregarDetalleVenta_Click"><i class="fas fa-cart-plus"></i> Agregar</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                        </div>
                    </div>


                    <%--Detalle venta--%>
                    <div class="row-col-12">
                        <asp:Panel ID="pDetalleVenta" runat="server">
                            <div class="card">
                                <div class="card-body">
                                    <hr />
                                    <div class="row">
                                        <div class="col-11">
                                            <asp:Panel ID="pVenta" runat="server">
                                                <asp:Literal ID="Tabla" runat="server"></asp:Literal> <%--Espacio para tabla dinámica-Listado DetalleProductos--%>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-1">
                                            <asp:Panel ID="pDelete" CssClass="d-block text-center text-monospace text-wrap" runat="server">
                                                <asp:Literal ID="Del" runat="server"></asp:Literal> <%--Espacio para linkbutton dinámico-Listado DetalleProductos--%>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <hr />
                                    <%--Grv de los detalles de ventas actuales--%>
                                    <%--NOTA: Grv no visible para el admin--%>
                                    <asp:GridView ID="GrvDetalleVenta" CssClass="table table-bordered table-hover table-responsive d-flex justify-content-center" runat="server" AutoGenerateColumns="False" CellSpacing="1" Width="100%" OnRowDeleting="GrvDetalleVenta_RowDeleting" DataKeyNames="Producto">

                                        <Columns>
                                            <asp:BoundField DataField="IdVenta" HeaderText="Venta" SortExpression="IdVenta" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                                            <asp:BoundField DataField="Producto" HeaderText="Producto" SortExpression="Producto" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                            <asp:BoundField DataField="PrecioUnidad" HeaderText="Precio" SortExpression="PrecioUnidad" />
                                            <asp:BoundField DataField="ImporteTotal" HeaderText="Total" SortExpression="ImporteTotal" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvDetalleVenta" runat="server" ToolTip="Borrar DetalleVenta" CommandName="Delete" CausesValidation="false" CssClass="text-danger"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <SelectedRowStyle Width="100%" Wrap="False" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <div class="card-footer alert-primary">
                    <div class="row">
                        <div class="col-6">
                            <asp:LinkButton ID="btConfirmarVenta" CssClass="btn btn-lg btn-success" runat="server" OnClick="btConfirmarVenta_Click" CausesValidation="False">Terminar Venta</asp:LinkButton>
                            <asp:LinkButton ID="btGenerarTicket" CssClass="btn btn-link" runat="server" CausesValidation="False" OnClick="btGenerarTicket_Click">Generar ticket última venta</asp:LinkButton>
                        </div>
                        <div class="col-3 d-flex align-items-center">
                            <asp:Label ID="NoProductos" runat="server" Text="Total Productos"></asp:Label>&nbsp;
                            <asp:TextBox ID="tbTotalProductos" Enabled="false" runat="server" placeHolder="0"></asp:TextBox>
                        </div>
                        <div class="col-3 d-flex align-items-center">
                            <asp:Label ID="ImporteTotal" runat="server" Text="Importe Total"></asp:Label>&nbsp; 
                            <asp:TextBox ID="tbTotalImporte" Enabled="false" runat="server" placeHolder="$0"></asp:TextBox>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfCarritoEnUso" runat="server" Value="clarines" />
                </div>
            </div>
        </asp:Panel>
    </div>

    
    <%--    MODAL CAMBIO   --%>
    <div class="modal fade" id="ModalCambioVenta" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-primary d-flex justify-content-center">
                    <h5 class="modal-title">Pago con...</h5>
                </div>
                <div class="modal-body pe-5 ps-5">  
                    
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="cambioImporte">Importe Total $</span>
                         <asp:TextBox ID="tbCambio_ImporteTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="cambioPagoCon">Pago Con $</span>
                        <asp:TextBox ID="tbCambio_PagoCon" CssClass="form-control" TextMode="Number" runat="server" OnTextChanged="tbCambio_PagoCon_TextChanged" CausesValidation="false" AutoPostBack="true" Text="0"></asp:TextBox>
                    </div>
                    <div class="input-group d-flex justify-content-around">
                        <span class="input-group-text" id="cambioCambio">Cambio $&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</span>
                        <asp:TextBox ID="tbCambio_Cambio" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div id="resultado" runat="server"></div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnConfirmarCambio" CssClass="btn btn-block btn-success" runat="server" Enabled="false" CausesValidation="false" OnClick="btnConfirmarCambio_Click">Confirmar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>



  <%--  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- GCLIENTES  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+---%>
    <div class="container-fluid">
        <asp:Panel ID="pRegClientes" runat="server">
            <div class="row">
                <div class="col-11">
                    <div>
                        <div class="card">
                            <h2 class="card-header text-center bg-primary text-white border-3 shadow-lg">Registro de Clientes</h2>
                            <div class="card-body">
                                <div class="row-col-12 form-group font-weight-bold d-inline-flex">
                                    <i class="das da-at mr-2"></i>Buscar&nbsp; 
                                    <asp:TextBox ID="tbBuscaCliente" runat="server" Width="100%" placeHolder="Buscar Cliente"></asp:TextBox>&nbsp&nbsp;
                                    <asp:LinkButton ID="BtnBusquedaCliente" runat="server" CssClass="btn btn-primary" CausesValidation="false" OnClick="BtnBusquedaCliente_Click"><i class="fas fa-search mr-2"></i></asp:LinkButton>&nbsp&nbsp;
                                </div>
                                <div id="RespuestaCliente" runat="server">
                                    <asp:Label ID="lbRegClientes" runat="server" Visible="False"></asp:Label>
                                </div>
                                <div class="row-col-auto justify-content-center">

                                    <%--GRV TODOS LOS CLIENTES--%>
                                    <asp:GridView ID="GrvCliente" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="IdCliente" OnRowDeleting="GrvCliente_RowDeleting" OnRowEditing="GrvCliente_RowEditing" OnRowUpdating="GrvCliente_RowUpdating">
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos" />
                                            <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                                            <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
                                            <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                                            <asp:BoundField DataField="Contrasena" HeaderText="Contrasena" SortExpression="Contrasena" />
                                            <asp:BoundField DataField="Credito" HeaderText="Credito" SortExpression="Credito" />
                                            <asp:BoundField DataField="CuentaActual" HeaderText="CuentaActual" SortExpression="CuentaActual" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvEditarCliente" runat="server" ToolTip="Editar Cliente" CommandName="Edit" CssClass="btn btn-light text-primary"><i class="fas fa-pencil-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvBorrarCliente" runat="server" ToolTip="Borrar Cliente" CommandName="Delete" CssClass="btn btn-light text-danger"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvEnviarUsuario" runat="server" ToolTip="Enviar Usuario" CommandName="Update" CssClass="btn btn-light text-success"><i class="far fa-paper-plane"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <%--GRV DEUDAS DE CLIENTES--%>
                                    <asp:GridView ID="GrvDeudasCliente" runat="server" CssClass="table table-bordered table-hover d-flex justify-content-center" AutoGenerateColumns="False" OnRowEditing="GrvDeudasCliente_RowEditing">
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos" />
                                            <asp:BoundField DataField="CuentaActual" HeaderText="CuentaActual" SortExpression="CuentaActual" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvEditarClienteAdeudo" CssClass="btn btn-success" runat="server" CommandName="Edit" CausesValidation="false"><i class="fas fa-trash-restore"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle HorizontalAlign="Center" />
                                    </asp:GridView>

                                    <%--GRV TODOS LOS USUARIOS--%>
                                    <asp:GridView ID="GrvUsuario" runat="server" CssClass="table table-bordered table-hover d-flex justify-content-center" AutoGenerateColumns="False" DataKeyNames="IdUsuario" OnRowEditing="GrvUsuario_RowEditing">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                            <asp:BoundField DataField="NombreUsuario" HeaderText="NombreUsuario" SortExpression="NombreUsuario" />
                                            <asp:BoundField DataField="ContrasenaUsuario" HeaderText="ContrasenaUsuario" SortExpression="ContrasenaUsuario" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvEditarUsuario" CssClass="btn btn-light text-warning" runat="server" CommandName="Edit" CausesValidation="false"><i class="fas fa-user-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-1 mt-5">
                    <asp:HiddenField ID="hfCualModuloCliente" runat="server" Value="0" />
                    <asp:LinkButton ID="BtnNuevoCliente" runat="server" CssClass="btn btn-primary btn-block mt-3 mb-2" OnClick="BtnNuevoCliente_Click" CausesValidation="False"><i class="fas fa-user-plus"></i>&nbsp Nuevo</asp:LinkButton>
                    <asp:LinkButton ID="BtnCreditos" runat="server" CssClass="btn btn-warning btn-block mb-2" OnClick="BtnCreditos_Click" CausesValidation="False"><i class="fas fa-wallet"></i>&nbsp Usuarios</asp:LinkButton>
                    <asp:LinkButton ID="BtnLiquidar" runat="server" CssClass="btn btn-success btn-block mb-2" Visible="true" OnClick="BtnLiquidar_Click" CausesValidation="False"><i class="fas fa-money-bill-alt"></i>&nbsp Liquidar</asp:LinkButton>
                    <asp:LinkButton ID="BtnCancelar" runat="server" CssClass="btn btn-danger btn-block mb-2" Visible="true" OnClick="BtnCancelar_Click" CausesValidation="False"><i class="fas fa-long-arrow-alt-left"></i>&nbsp Atrás</asp:LinkButton>

                </div>
            </div>
        </asp:Panel>
    </div>

<%-- AGREGAR NUEVO CLIENTE--%>
    <div class="container-fluid">
        <asp:Panel ID="pAgregarNuevoCliente" runat="server">
            <div class="row-cols-auto">
                <div class="">
                    <asp:Panel ID="ptitulocliente" runat="server">
                        <asp:Literal ID="header" runat="server"></asp:Literal>
                    </asp:Panel>

                    <div class="card-body">
                        <div class="modal-dialog modal-xl shadow" role="document">

                            <div class="modal-content">
                                <h3 class="modal-header">Detalle Cliente</h3>
                                <div class="modal-body">
                                    <div id="ResultNuevoCliente" runat="server">
                                        <asp:Label ID="lbResultadoNuevoCliente" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-2">
                                            <asp:Label ID="lbCodCliente" runat="server" Text="Código"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucCualquierDatoRequerido runat="server" ID="tbCodigoDeCliente" />
                                            </div>
                                        </div>
                                        <div class="col-4">
                                            <asp:Label ID="lbNombreCiente" runat="server" Text="Nombre"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucAlfaNumericoRequerido runat="server" ID="tbNombreDeCliente" />
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label ID="lbApellidosCliente" runat="server" Text="Apellidos"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucAlfaNumericoRequerido runat="server" ID="tbApellidoDeCliente" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label ID="lbDireccionCliente" runat="server" Text="Dirección"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucAlfaNumericoRequerido runat="server" ID="tbDireccionDeCliente" />
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <asp:Label ID="lbTelefonoCliente" runat="server" Text="Teléfono"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucNumeroTelefono runat="server" ID="tbTelefonoDeCliente" />
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <asp:Label ID="lbCorreoCliente" runat="server" Text="Correo"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucCorreoRequerido runat="server" ID="tbCorreoDeCliente" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label ID="lbUsuarioCliente" runat="server" Text="Usuario"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucUsuarioRequerido runat="server" ID="tbUsuarioDeCliente" />
                                            </div>
                                        </div>
                                        <div class="col-4">
                                            <asp:Label ID="lbPasswordCliente" runat="server" Text="Contraseña"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucContraseñaRequerida runat="server" ID="tbPasswordDeUsuario" />
                                            </div>
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lbCreditoCliente" runat="server" Text="Crédito $"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <uc1:wucNumeroRequerido runat="server" ID="tbCreditoDeCliente" />
                                            </div>
                                        </div>
                                        <div class="col-2">
                                            <asp:Label ID="lbCuentaActual" runat="server" Text="Deuda Actual $"></asp:Label>
                                            <div class="form-group font-weight-bold">
                                                <asp:TextBox ID="tbCuentaActualDeCliente" CssClass="form-control" runat="server" Width="100%" Text="0" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="BtnCancelarRegistroCliente" runat="server" CssClass="btn btn-danger" CausesValidation="False" OnClick="BtnCancelarRegistroCliente_Click"><i class="fas fa-backspace"></i>&nbsp Cancelar</asp:LinkButton>
                                    <asp:LinkButton ID="BtnRegistrarCliente" runat="server" CssClass="btn btn-success" OnClick="BtnRegistrarCliente_Click"><i class="fas fa-user-plus"></i>&nbsp Registrar</asp:LinkButton>
                                    <asp:LinkButton ID="BtnModificarCliente" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="BtnModificarCliente_Click"><i class="fas fa-check"></i>&nbsp Modificar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>


      <%--  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- GVENTAS  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+---%>
    <div class="container-fluid d-flex justify-content-center">
        <asp:Panel ID="pRegVentas" runat="server">
            <div class="row">
                <div class="col-12">
                    <div>
                        <div class="card">
                            <h2 class="card-header text-center bg-info text-white border-3 shadow-lg">Registro de Ventas</h2>
                            <div class="card-body">
                                <div id="RespuestaBusquedaVentas" runat="server">
                                    <asp:Label ID="lbRespuestaVentas" runat="server" Enabled="false" Text=""></asp:Label>
                                </div>
                                <div class="row-cols-auto d-flex justify-content-around">
                                    <div class="font-weight-bold">
                                        <i class="das da-at mr-2 "></i>Buscar
                                    </div>
                                    <div>
                                        <asp:TextBox ID="tbInicio" runat="server" placeHolder="Inicio" Enabled="false"></asp:TextBox>
                                        <asp:LinkButton ID="VerCalenInicio" CssClass="btn btn-success" runat="server" OnClick="VerCalenInicio_Click"><i class="fas fa-calendar-day"></i></asp:LinkButton>
                                        <asp:Calendar ID="CalendarioInicio" runat="server" OnSelectionChanged="Inicio_SelectionChanged" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                            <WeekendDayStyle BackColor="#CCCCFF" />
                                        </asp:Calendar>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="tbFin" runat="server" placeHolder="Fin" Enabled="false"></asp:TextBox>
                                        <asp:LinkButton ID="VerCalenFin" CssClass="btn btn-success" runat="server" OnClick="VerCalenFin_Click"><i class="fas fa-calendar-day"></i></asp:LinkButton>
                                        <asp:Calendar ID="CalendarioFin" runat="server" OnSelectionChanged="CalendarioFin_SelectionChanged" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                            <WeekendDayStyle BackColor="#CCCCFF" />
                                        </asp:Calendar>
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="BtnBuscarFechas" runat="server" OnClick="BtnBuscarFechas_Click"><i class="fas fa-search mr-2"></i>Buscar</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="row-cols-auto d-flex justify-content-between mt-3 mb-3">
                                    <div class="col-6 d-flex justify-content-start">
                                        <asp:LinkButton ID="BtnRestablecerRegVentas" CssClass="btn btn-outline-info" runat="server" OnClick="BtnRestablecerRegVentas_Click">Ver todas</asp:LinkButton>
                                    </div>
                                </div>

                                <div id="MensajeVenta" runat="server">
                                    <asp:Label ID="lbRegVentas" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="row-col-12">
                                    <asp:GridView ID="GrvRegVentas" runat="server" CssClass="table table-bordered table-hover table-responsive"
                                        AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="GrvRegVentas_RowDeleting" OnRowEditing="GrvRegVentas_RowEditing">
                                        <Columns>
                                            <asp:BoundField DataField="CodVenta" HeaderText="CodVenta" SortExpression="CodVenta" />
                                            <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Fecha" SortExpression="Fecha" />
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField DataField="TotalProductos" HeaderText="TotalProductos" SortExpression="TotalProductos" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvRegVentasDetalles" ToolTip="Detalles-Venta" runat="server" CommandName="Edit" CssClass="btn btn-primary"><i class="fas fa-list-ul"></i>&nbsp Detalles</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnGrvRegVentasBorrar" ToolTip="Borrar Venta" runat="server" CommandName="Delete" CssClass="btn btn-light text-danger"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                    <asp:HiddenField ID="hfIdVenta" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>
    </div>



    <%--  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- MODALES  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+---%>

    <%--INCIO - TODOS LOS PRODUCTOS--%>
    <div class="modal fade" id="ModalTodoProductos" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-scrollable  modal-lg">
            <div class="modal-content">
                <div class="modal-header alert-info">
                    <h5 class="modal-title text-center">Productos...</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="pTodoProductos" runat="server">
                        <asp:GridView ID="GrvTodo" runat="server" AutoGenerateColumns="False" DataKeyNames="IdProducto" CssClass="table table-bordered table-hover" OnRowEditing="GrvTodo_RowEditing">

                            <Columns>
                                <asp:BoundField DataField="CodProducto" HeaderText="CodProducto" SortExpression="CodProducto" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnSeleccionProducto" ToolTip="Seleccionar" runat="server" CommandName="Edit" CssClass="btn btn-light" CausesValidation="false"><i class="far fa-hand-pointer"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>


    <%--EDITAR USUARIO--%>
    <div class="modal fade" id="ModalEditaUsuario" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-warning d-flex">
                    <h5 class="modal-title">Editar Usuario</h5>
                    <asp:LinkButton ID="CerrarEditUsuario" runat="server" CssClass="btn-close" data-bs-dismiss="modal" aria-label="Close"></asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="d-flex justify-content-around mb-3">
                        <asp:TextBox ID="tbEditUsuarioActual" CssClass="form-control text-center" runat="server" Enabled="false"></asp:TextBox>
                        <asp:TextBox ID="tbEditContrasenaActual" CssClass="form-control text-center" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="edit3">Usuario</span>
                        <asp:TextBox ID="tbEditUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="edit2">Contraseña</span>
                        <asp:TextBox ID="tbEditContrasena" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-around">
                    <asp:Button ID="btnModificarUsuario" CssClass="btn btn-warning btn-block mb-2" runat="server" Text="Confirmar" OnClick="btnModificarUsuario_Click" />
                </div>
            </div>
        </div>
    </div>


    <!--VER DETALLES VENTAS -->
    <div class="modal fade" id="ModalDetalleVenta" tabindex="-1" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header alert-info">
                    <h4 class="modal-title text-center">Detalle de venta</h4>
                </div>
                <hr />
                <div class="modal-body">
                    <asp:Panel ID="pDetalles" runat="server">
                        <asp:GridView ID="GrvDetalles" CssClass="table table-bordered table-hover table-primary table-striped" runat="server" AutoGenerateColumns="False" DataKeyNames="ID">
                            <Columns>
                                <asp:BoundField DataField="CodVenta" HeaderText="CodVenta" SortExpression="CodVenta" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                                <asp:BoundField DataField="ImporteTotal" HeaderText="ImporteTotal" SortExpression="ImporteTotal" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="CerrarDetallesVenta" CssClass="btn btn-outline-dark" runat="server" Text="Cerrar" data-bs-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>


    <!-- BORRAR VENTA -->
    <div class="modal fade" id="ModalBorrar" tabindex="-1" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-danger d-flex">
                    <h5 class="modal-title">¿Desea borrar archivo seleccionado?  </h5>
                </div>
                <div class="modal-body">
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="basic-addon1">CODIGO VENTA</span>
                        <asp:TextBox ID="tbBorrar" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="cancelarBorrarVenta" CssClass="btn btn-outline-dark" runat="server" Text="Cancelar" data-bs-dismiss="modal" />
                        <asp:Button ID="btnConfirmarBorrado" CssClass="btn btn-outline-danger" runat="server" Text="Confirmar" OnClick="btnConfirmarBorrado_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- BORRAR CLIENTE -->
    <div class="modal fade" id="ModalBorrarCliente" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-danger d-flex">
                    <h5 class="modal-title">¿Desea borrar archivo seleccionado?  </h5>
                    <asp:LinkButton ID="cerrarBorrarCliente" runat="server" CssClass="btn-close" data-bs-dismiss="modal" aria-label="Close"></asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text">CODIGO CLIENTE</span>
                        <asp:TextBox ID="tbBorrarCliente" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="cancelarBorrarCliente" CssClass="btn btn-outline-dark" runat="server" Text="Cancelar" data-bs-dismiss="modal" />
                        <asp:Button ID="btnConfirmarBorradoCliente" CssClass="btn btn-outline-danger" runat="server" Text="Confirmar" OnClick="btnConfirmarBorradoCliente_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- ABONAR -->
    <div class="modal fade" id="ModalAbonoCliente" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-success">
                    <h5 class="modal-title">Abono a Cliente</h5>
                    <asp:LinkButton ID="CerrarAbono" runat="server" CssClass="btn-close" data-bs-dismiss="modal" aria-label="Close"></asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="basic1">CLIENTE</span>
                        <asp:TextBox ID="tbNombresDeuda" CssClass="form-control" runat="server" Enabled="false"> </asp:TextBox>
                    </div>
                    <div class="input-group mb-3 d-flex justify-content-around">
                        <span class="input-group-text" id="basic2">EDO ACTUAL $</span>
                        <asp:TextBox ID="tbCuentaDeuda" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="row-cols-auto justify-content-center d-flex justify-content-between" runat="server">
                        <div class="cols-auto">
                            <asp:Label ID="lbAbono" runat="server" Text="Cantidad a Abonar" Visible="true"></asp:Label>
                            <asp:TextBox ID="tbAbonar" CssClass="form-control" runat="server" Visible="true"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="text-danger font-italic" ErrorMessage="Solo datos numéricos positivos" ValidationExpression="([0-9]+([.][0-9]*)?|[.][0-9]+)$" ControlToValidate="tbAbonar" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <asp:LinkButton ID="ConfirmarAbono" type="button" runat="server" CssClass="btn btn-success btn-block mb-2" CausesValidation="True" Visible="true" OnClick="ConfirmarAbono_Click">Abonar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>


    <%--  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+- HFIELD  -+-+-+-+--+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+---%>
    <asp:HiddenField ID="hfIdCliente" runat="server" />
    <asp:HiddenField ID="hfNav" runat="server" />
    <asp:HiddenField ID="hfIdProductoResult" runat="server" />
    <asp:HiddenField ID="hfSeleccionDetalles" runat="server" />
    <asp:HiddenField ID="hfTotalImporte" runat="server" />
    <asp:HiddenField ID="hfIdUsuario" runat="server" />
    <asp:HiddenField ID="hfCuentaCliente" runat="server" />
    <asp:Label ID="LblRespuesta" runat="server" Text=""></asp:Label>
</asp:Content>
