<%@ Page Title="Crear Usuario" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
         AutoEventWireup="true" CodeBehind="Crear.aspx.cs"
         Inherits="Web.Pages.Usuarios.Crear" %>

<asp:Content ID="ContenidoCrear" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Crear usuario</h3>

    <div class="row g-3">

        <div class="col-md-6">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="TextoNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6">
            <label class="form-label">Email</label>
            <asp:TextBox ID="TextoEmail" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6">
            <label class="form-label">Rol</label>
            <asp:DropDownList ID="SelectRol" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <div class="col-12 mt-3">
        <div class="alert alert-info d-inline-block p-2">
            La contraseña inicial será: <strong>gestiondeturnos</strong>
        </div>
        </div>

        <div class="mt-4 d-flex gap-3">
            <asp:Button ID="BotonGuardar" runat="server"
                        Text="Guardar"
                        CssClass="btn btn-success"
                        OnClick="ClickBotonGuardar" />

            <a href="Listar.aspx" class="btn btn-secondary">Volver</a>
        </div>
    </div>
</asp:Content>