<%@ Page Title="Editar Relación" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Editar.aspx.cs"
    Inherits="Web.Pages.Relaciones.Editar" %>

<asp:Content ID="ContenidoEditarRelacion" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Editar relación</h3>

    <asp:Label ID="ErrorEditar" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

    <asp:HiddenField ID="IdRelacionHidden" runat="server" />

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
