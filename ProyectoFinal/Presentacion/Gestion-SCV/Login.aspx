<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MP_Inicio.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Gestion_SCV.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphInicio" runat="server">
    <div class="container-fluid">
        <asp:Panel ID="PnlValidaUsuario" runat="server">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content shadow">
                    <div class="modal-header d-inline-block">
                        <h1 class="text-primary text-center font-weight-bold alert-light">Inicio de sesión</h1>
                    </div>
                    <div class="modal-body d-block">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group font-weight-bold">
                                    <i class="das da-at mr-2"></i>Usuario
                                    <asp:TextBox ID="TbUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group font-weight-bold">
                                    <i class="das da-at mr-2"></i>Contraseña
                                    <asp:TextBox ID="TbPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="InicioEstatus" runat="server">
                        <asp:Label ID="lbRespuesta" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="BtnLogin" runat="server" CssClass="btn btn-outline-success btn-block mr-2 font-monospace" OnClick="BtnLogin_Click">Acceder</asp:LinkButton>
                        <asp:LinkButton ID="BtnOlvidePass" CssClass="text-right text-success font-weight-light form-control" runat="server" OnClick="BtnOlvidePass_Click">Olvidé mi contraseña</asp:LinkButton>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>



    <!-- SOLICITAR CAMBIO CONTRASEÑA -->
    <div class="modal fade" id="ModalCambioPass" tabindex="-1" data-keyboard="false">
        <div class="modal-dialog modal-s">
            <div class="modal-content">
                <div class="modal-header alert-warning d-flex">
                    <h3 class="modal-title">Solicitar renovación de contraseña</h3>
                </div>
                <div class="modal-body">

                    <Panel id="form1" runat="server">
                        <div>
<%--                            <h3>Envío de correos</h3>--%>
                            <p>
                                Asunto:
                                <asp:TextBox ID="TbAsunto" runat="server" CssClass="form-control" Width="100%" Text="Solicitud - Renovación de contraseña" Enabled="false"></asp:TextBox>
                            </p>
                            <p>
                                De:
                                <asp:TextBox ID="TbRemitente" runat="server" CssClass="form-control" Width="100%" TextMode="Email" placeHolder="Ingresa correo" ToolTip="Correo registrado con la cuenta"></asp:TextBox>
                            </p>
                            <p>
                                Contraseña:
                                <asp:TextBox ID="TbPassCorreo" runat="server" CssClass="form-control" Width="100%" TextMode="Password"></asp:TextBox>
                            </p>
                            <p>Mensaje:</p>
                            <p>
                                <asp:TextBox ID="TbMensaje" runat="server" CssClass="form-control h-auto" TextMode="MultiLine" Width="100%" Enabled="false" Text="Solicito una renovación de la cuenta relacionada con el correo actual."></asp:TextBox>
                            </p>
                        </div>
                    </Panel>
                    <div class="d-flex justify-content-start">
                        <asp:Button ID="btnConfirmarSolicitud" CssClass="btn btn-outline-success" runat="server" Text="Enviar" OnClick="btnConfirmarSolicitud_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
