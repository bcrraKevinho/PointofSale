<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucUsuarioRequerido.ascx.cs" Inherits="Presentacion.Controls.wucUsuarioRequerido" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="TbUsuarioRequerido" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvTbUsuarioRequerido" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="TbUsuarioRequerido" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revTbUsuarioRequerido" runat="server" CssClass="text-danger font-italic" ErrorMessage="No se permiten caracteres especiales" ValidationExpression="[0-9]{3,3}[-]{1}[a-zA-Z0-9 .áéíóúñÁÉÍÓÚÑ]+$" ControlToValidate="TbUsuarioRequerido" Display="Dynamic"></asp:RegularExpressionValidator>
