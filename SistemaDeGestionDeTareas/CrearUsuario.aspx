<%@ Page Title="Crear Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="SistemaDeGestionDeTareas.CrearUsuario" %>

<asp:Content ID="ContenidoCrearUsuario" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Crear nuevo usuario</h2>
    <p>Complete los datos para dar de alta un nuevo usuario.</p>

    <div class="card">
        <div class="card-body">
            <div class="mb-3">
                <label for="txtNombreUsuario" class="form-label">Nombre de Usuario</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreUsuario"
                    ErrorMessage="El nombre de usuario es obligatorio" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es obligatoria" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label for="ddlRol" class="form-label">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione un rol" Value="" />
                    <asp:ListItem Text="Admin" Value="Administrador" />
                    <asp:ListItem Text="User" Value="Usuario" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol"
                    InitialValue="" ErrorMessage="Debe seleccionar un rol" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="form-check mb-3">
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" Checked="true" />
                <label class="form-check-label" for="chkActivo">Usuario activo</label>
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            
            <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" Text="Cancelar" CssClass="btn btn-secondary ms-2" OnClick="btnCancelar_Click" />
        </div>
    </div>

    <asp:ValidationSummary ID="vsErrores" runat="server" CssClass="text-danger mt-3" />
</asp:Content>
