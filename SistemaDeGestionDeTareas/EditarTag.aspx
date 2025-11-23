<%@ Page Title="Editar Tag" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditarTag.aspx.cs" Inherits="SistemaDeGestionDeTareas.EditarTag" %>

<asp:Content ID="ContenidoEditarTag" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Editar Tag</h2>
    <p>Modifique los datos y guarde los cambios.</p>

    <!-- Campo oculto para el ID -->
    <asp:HiddenField ID="hfIdTag" runat="server" />

    <div class="card">
        <div class="card-body">

            <!-- Nombre -->
            <div class="mb-3">
                <label class="form-label">Nombre del Tag</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <!-- Color -->
            <div class="mb-3">
                <label class="form-label">Color</label>
                <input type="color" id="txtColor" runat="server" class="form-control" />
            </div>

            <!-- Botones -->
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                CssClass="btn btn-secondary ms-2"
                CausesValidation="false" OnClick="btnCancelar_Click"/>

        </div>
    </div>

    <asp:ValidationSummary ID="vsErrores" runat="server" CssClass="text-danger mt-3" />

</asp:Content>
