<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MP_Cliente.master" AutoEventWireup="true" CodeBehind="Mod_Cliente-Inicio.aspx.cs" Inherits="Presentacion.Gestion_SCV.Mod_Cliente_Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCliente" runat="server">

    <asp:GridView ID="GrvProducto" runat="server" CssClass="table table-bordered table-hover table-responsive" AutoGenerateColumns="False" DataKeyNames="IdProducto">
        <Columns>
            <asp:BoundField DataField="IdProducto" HeaderText="IdProducto" InsertVisible="False" ReadOnly="True" SortExpression="IdProducto" />
            <asp:BoundField DataField="CodProducto" HeaderText="CodProducto" SortExpression="CodProducto" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pProductos" runat="server">
        <asp:Label ID="LblRespuesta" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="repeaterProd" runat="server">
            <ItemTemplate>
                <div class="d-flex mb-3 <%--rpt-css--%>">
                    <table>
                        <tr>
                            <td></td>
                            <%--<a href='<%# string.Format("HANDLER/RecuperaArchivo.ashx?IdProducto={0}", Eval("IdProducto")) %>'> lo cambiamos a comillas sencillas para poner la ruta de recuperacion--%>
                            <a href='<%# string.Format("https://localhost:44372/HANDLER/RecuperaArchivo.ashx?IdProducto={0}", Eval("IdProducto")) %>'><%-- lo cambiamos a comillas sencillas para poner la ruta de recuperacion--%>
                                <asp:Image ID="RepeatIMG" runat="server" CssClass="Imagen" ImageUrl='<%# string.Format("~/HANDLER/RecuperaArchivo.ashx?IdProducto={0}", Eval("IdProducto")) %>' />
                            </a>
                        </tr>
                        <tr>
                            <td>Codigo:</td>
                            <td><%# Eval("CodProducto") %></td>
                        </tr>
                        <tr>
                            <td>Nombre:</td>
                            <td><%# Eval("Nombre") %></td>
                        </tr>
                        <tr>
                            <td>Descripcion:</td>
                            <td><%# Eval("Descripcion") %></td>
                        </tr>
                        <tr>
                            <td>Precio: $</td>
                            <td><%# Eval("Precio") %></td>
                        </tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <div class="container d-flex justify-content-center mt-3">
        <asp:Panel ID="pEstatus" runat="server">
            <div class="card">
                <div class="card-header text-center alert-primary pb-3 pt-3">
                    <h2>
                        <asp:Label ID="lbNombreCliente" runat="server" Text=""></asp:Label></h2>
                </div>
                <div class="row-cols-auto mt-2">
                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon1">Nombre Completo</span>
                        <asp:TextBox ID="tbnombre" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon2">Crédito $</span>
                        <asp:TextBox ID="tbcredito" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon3">Estado Cuenta $</span>
                        <asp:TextBox ID="tbcuentaactual" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>

                </div>
                <div class="row-cols-auto d-flex justify-content-center shadow pt-3 pb-2">
                    <div class="font-weight-bold mr-1">
                        <i class="das da-at mr-2 "></i>Busca por Fecha: 
                    </div>
                    <div>
                        <asp:TextBox ID="tbInicio" runat="server" placeHolder="Fecha" Enabled="false"></asp:TextBox>
                        <asp:LinkButton ID="VerCalenInicio" CssClass="btn btn-success" runat="server" OnClick="VerCalenInicio_Click"><i class="fas fa-calendar-day"></i></asp:LinkButton>
                        <asp:Calendar ID="CalendarioInicio" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" OnSelectionChanged="CalendarioInicio_SelectionChanged">
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
                        <asp:LinkButton ID="BtnBuscarFechas" CssClass="btn btn-link" runat="server" OnClick="BtnBuscarFechas_Click"><i class="fas fa-search mr-2"></i>Buscar</asp:LinkButton>
                    </div>
                </div>
                <div class="row-col-12  alert-info mt-3 mb-3">
                    <asp:Button ID="btnAllCompras" CssClass="btn btn-block btn btn-outline-primary" runat="server" Text="Mostrar todo" OnClick="btnAllCompras_Click" />
                    <div class="d-flex justify-content-center font-weight-bold">REGISTRO DE VENTAS</div>
                </div>
                <div id="RespuestaBusquedaVentas" runat="server">
                    <asp:Label ID="lbRespuestaVentas" runat="server" Text=""></asp:Label>
                </div>
                <div class="card-body">
                    <div class="d-flex justify-content-end">
                        <asp:LinkButton ID="btnGenerarReporteCliente" CssClass="btn btn-link text-success mb-3" runat="server" OnClick="GenerarReporte_Click"><i class="fas fa-print"></i>Generar Reporte de Ventas</asp:LinkButton></div>

                    <asp:GridView ID="GrvRegVentas" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowEditing="GrvRegVentas_RowEditing">

                        <Columns>
                            <asp:BoundField DataField="CodVenta" HeaderText="CodVenta" SortExpression="CodVenta" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:d}" SortExpression="Fecha" />
                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                            <asp:BoundField DataField="TotalProductos" HeaderText="TotalProductos" SortExpression="TotalProductos" />
                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                            <asp:TemplateField HeaderText=""><%-- se agregó--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="BtnGrvRegVentasDetalles" ToolTip="Detalles-Venta" runat="server" CommandName="Edit" CssClass="btn btn-primary"><i class="fas fa-list-ul"></i>&nbsp Detalles</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                    <asp:HiddenField ID="hfIdVenta" runat="server" />
                    <%-- se agregó--%>
                </div>
            </div>
        </asp:Panel>
    </div>


    <!--VER DETALLES VENTAS -->
    <div class="modal fade" id="ModalDetalleVenta" tabindex="-1" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header alert-primary">
                    <h5 class="modal-title text-center">Detalle de venta</h5>
                </div>
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


    <asp:HiddenField ID="hfNav" runat="server" />
    <asp:HiddenField ID="hfBuscar" runat="server" />
    <asp:HiddenField ID="hfUsuario" runat="server" />
</asp:Content>
