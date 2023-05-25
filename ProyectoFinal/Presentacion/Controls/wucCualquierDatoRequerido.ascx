<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCualquierDatoRequerido.ascx.cs" Inherits="Presentacion.Controls.wucCualquierDatoRequerido" %>
<link href="../CSS/bootstrap.min.css" rel="stylesheet" />
<asp:TextBox ID="TbCualquierDatoRequerido" runat="server" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvTbCualquierDatoRequerido" CssClass="text-danger font-italic" runat="server" ErrorMessage="Capture Dato" ControlToValidate="TbCualquierDatoRequerido" Display="Dynamic"></asp:RequiredFieldValidator>
