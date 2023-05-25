<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucNumeroRequerido.ascx.cs" Inherits="Presentacion.Controls.wucNumeroRequerido" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="TbNumeroRequerido" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvTbNumeroRequerido" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="TbNumeroRequerido" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revTbNumeroRequerido" runat="server" CssClass="text-danger font-italic" ErrorMessage="Solo datos enteros numéricos y positivos" ValidationExpression="^[0-9]+$" ControlToValidate="TbNumeroRequerido" Display="Dynamic"></asp:RegularExpressionValidator>
