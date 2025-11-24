<%@ Page Title="Crear Tag" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CrearTag.aspx.cs" Inherits="SistemaDeGestionDeTareas.CrearTag" %>

<asp:Content ID="ContenidoCrearTag" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Crear nuevo Tag</h2>
    <p>Complete los datos para crear un nuevo tag.</p>

    <div class="card">
        <div class="card-body">

           
            <div class="mb-3">
                <label class="form-label">Nombre del Tag</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

           
            <div class="mb-3">
                <label class="form-label">Color</label>
                <input type="color" id="txtColor" runat="server" class="form-control" />
                <asp:RequiredFieldValidator ID="rfvColor" runat="server"
                    ControlToValidate="txtColor"
                    ErrorMessage="Debe seleccionar un color"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

         
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                CssClass="btn btn-secondary ms-2"
                CausesValidation="false" OnClick="btnCancelar_Click"/>

        </div>
    </div>

    <asp:ValidationSummary ID="vsErrores" runat="server" CssClass="text-danger mt-3" />

</asp:Content>
