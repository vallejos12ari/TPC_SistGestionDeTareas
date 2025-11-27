<%@ Page Title="Crear Estado" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
         AutoEventWireup="true" CodeBehind="Crear.aspx.cs"
         Inherits="Web.Pages.Estados.Crear" %>

<asp:Content ID="ContenidoCrearEstado" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Crear estado</h3>

    <asp:Label ID="ErrorCrear" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

    <div class="row g-3">

        <div class="col-md-6">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="TextoNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-4">
            <label class="form-label">Color</label>

            <select id="SelectColor" class="form-select"
                    onchange="document.getElementById('<%= TextoColor.ClientID %>').value = this.value">

                <option value="#FFE066">Amarillo</option>
                <option value="#FFB347">Naranja</option>
                <option value="#8BE67C">Verde</option>
                <option value="#7FFFD4">Menta</option>
                <option value="#89CFF0">Celeste</option>
                <option value="#A7C7E7">Azul gris</option>
                <option value="#C5A3FF">Lila</option>
                <option value="#FFB5E8">Rosa</option>
                <option value="#F5DEB3">Beige</option>
                <option value="#E0E0E0">Gris</option>
                <option value="#FF7474">Rojo</option>
            </select>

            <asp:TextBox ID="TextoColor" runat="server" CssClass="d-none" />
        </div>

        <div class="col-md-2">
            <div class="form-check mt-4">
                <asp:CheckBox ID="CheckEsFinal" runat="server" CssClass="form-check-input" />
                <label class="form-check-label" for="<%= CheckEsFinal.ClientID %>">
                    Es estado final
                </label>
            </div>
        </div>

        <div class="mt-4 d-flex gap-3">
            <asp:Button ID="BotonCrear" runat="server"
                        Text="Crear"
                        CssClass="btn btn-success"
                        OnClick="ClickBotonCrear" />

            <a href="Listar.aspx" class="btn btn-secondary">Volver</a>
        </div>

    </div>

</asp:Content>
