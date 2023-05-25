<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCorreoRequerido.ascx.cs" Inherits="Presentacion.Controls.wucCorreoRequerido" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="tbCorreoRequerido" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvtbCorreoRequerido" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="tbCorreoRequerido" Display="Dynamic"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revtbCorreoRequerido" runat="server" CssClass="text-danger font-italic" ErrorMessage="Formato del correo erroneo" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbCorreoRequerido" Display="Dynamic"></asp:RegularExpressionValidator>
