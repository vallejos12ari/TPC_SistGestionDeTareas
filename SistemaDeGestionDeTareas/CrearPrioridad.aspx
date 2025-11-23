<%@ Page Title="Crear Prioridad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CrearPrioridad.aspx.cs" Inherits="SistemaDeGestionDeTareas.CrearPrioridad" %>

<asp:Content ID="ContenidoCrearPrioridad" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Crear nueva prioridad</h2>
    <p>Complete los datos para crear una prioridad personalizada.</p>

    <div class="card">
        <div class="card-body">

            <!-- Nombre -->
            <div class="mb-3">
                <label class="form-label">Nombre de la prioridad</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <!-- Nivel -->
            <div class="mb-3">
                <label class="form-label">Nivel</label>
                <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator ID="rfvNivel" runat="server"
                    ControlToValidate="txtNivel"
                    ErrorMessage="El nivel es obligatorio"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <!-- Color -->
            <div class="mb-3">
                <label class="form-label">Color</label>
                <input type="color" id="txtColor" runat="server" class="form-control" />
                <asp:RequiredFieldValidator ID="rfvColor" runat="server"
                    ControlToValidate="txtColor"
                    ErrorMessage="Debe seleccionar un color"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <!-- Botones -->
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                CssClass="btn btn-secondary ms-2"
                CausesValidation="false" OnClick="btnCancelar_Click"/>

        </div>
    </div>

    <asp:ValidationSummary ID="vsErrores" runat="server" CssClass="text-danger mt-3" />

</asp:Content>
