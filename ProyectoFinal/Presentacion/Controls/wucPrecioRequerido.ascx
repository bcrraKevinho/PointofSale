<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucPrecioRequerido.ascx.cs" Inherits="Presentacion.Controls.wucPrecioRequerido" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="TbPrecioRequerido" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvTbPrecioRequerido" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="TbPrecioRequerido" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revTbPrecioRequerido" runat="server" CssClass="text-danger font-italic" ErrorMessage="Solo datos numéricos positivos" ValidationExpression="^[0-9.]+$" ControlToValidate="TbPrecioRequerido" Display="Dynamic"></asp:RegularExpressionValidator>
