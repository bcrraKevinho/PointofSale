<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucAlfaNumericoRequerido.ascx.cs" Inherits="Presentacion.Controls.wucAlfaNumericoRequerido" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="TbAlfanumericoRequerido" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvTbAlfanumericoRequerido" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="TbAlfanumericoRequerido" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revTbAlfanumericoRequerido" runat="server" CssClass="text-danger font-italic" ErrorMessage="No se permiten caracteres especiales" ValidationExpression="^[a-zA-Z0-9 .áéíóúñÁÉÍÓÚÑ]+$" ControlToValidate="TbAlfanumericoRequerido" Display="Dynamic"></asp:RegularExpressionValidator>
