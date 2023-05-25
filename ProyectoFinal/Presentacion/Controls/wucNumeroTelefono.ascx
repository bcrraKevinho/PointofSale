<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucNumeroTelefono.ascx.cs" Inherits="Presentacion.Controls.wucNumeroTelefono" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="tbNumeroTelefono" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvtbNumeroTelefono" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="tbNumeroTelefono" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revtbNumeroTelefono" runat="server" CssClass="text-danger font-italic" ErrorMessage="Formato incorrecto {###-#######}" ValidationExpression="[0-9]{3,3}[-]{1}[0-9]{7,7}$" ControlToValidate="tbNumeroTelefono" Display="Dynamic"></asp:RegularExpressionValidator>
