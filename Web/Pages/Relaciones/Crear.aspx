<%@ Page Title="Crear Relación" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Crear.aspx.cs"
    Inherits="Web.Pages.Relaciones.Crear" %>

<asp:Content ID="ContenidoCrearRelacion" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Crear relación</h3>

    <asp:Label ID="ErrorCrear" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

    <div class="row g-3">

        <div class="col-md-6">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="TextoNombre" runat="server" CssClass="form-control" />
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
