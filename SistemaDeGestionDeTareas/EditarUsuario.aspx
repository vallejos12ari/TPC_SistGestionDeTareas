<%@ Page Title="Editar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditarUsuario.aspx.cs" Inherits="SistemaDeGestionDeTareas.EditarUsuario" %>

<asp:Content ID="ContenidoEditarUsuario" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Editar usuario</h2>
    <p>Modifique los campos y guarde los cambios.</p>

    <asp:HiddenField ID="hfIdUsuario" runat="server" />

    <div class="card">
        <div class="card-body">
            <div class="mb-3">
                <label for="txtNombreUsuario" class="form-label">Nombre de Usuario</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombreUsuario"
                    ErrorMessage="El nombre de usuario es obligatorio"
                    CssClass="text-danger" Display="Dynamic"
                    ValidationGroup="EditarUsuario" />
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio"
                    CssClass="text-danger" Display="Dynamic"
                    ValidationGroup="EditarUsuario" />
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Contraseña (dejar en blanco para no cambiar)</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />

            </div>

            <div class="mb-3">
                <label for="ddlRol" class="form-label">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione un rol" Value="" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                    <asp:ListItem Text="User" Value="Usuario" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRol" runat="server"
                    ControlToValidate="ddlRol" InitialValue=""
                    ErrorMessage="Debe seleccionar un rol"
                    CssClass="text-danger" Display="Dynamic"
                    ValidationGroup="EditarUsuario" />
            </div>

            <div class="form-check mb-3">
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" />
                <label class="form-check-label" for="chkActivo">Usuario activo</label>
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click"
                ValidationGroup="EditarUsuario" />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                CssClass="btn btn-secondary ms-2" OnClick="btnCancelar_Click"
                CausesValidation="false" />
        </div>
    </div>

    <asp:ValidationSummary ID="vsErrores" runat="server"
        CssClass="text-danger mt-3" ValidationGroup="EditarUsuario" />

    <hr />
    <h3>Usuarios Relacionados</h3>

 
    <asp:GridView ID="gvUsuariosRelacionados" runat="server" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped" OnRowCommand="gvUsuariosRelacionados_RowCommand">
        <Columns>
            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEliminarRelacion" runat="server"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("IdUsuario") %>'
                        Text="Quitar Relación"
                        CssClass="btn btn-danger btn-sm"
                        OnClientClick="return confirm('¿Está seguro de que desea quitar esta relación?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="alert alert-info">No hay usuarios relacionados.</div>
        </EmptyDataTemplate>
    </asp:GridView>

    <br />

    
    <h4>Agregar Nueva Relación</h4>
    <div class="form-inline">
        <div class="form-group">
            <label for="ddlUsuariosDisponibles">Seleccionar Usuario:</label>
            <asp:DropDownList ID="ddlUsuariosDisponibles" runat="server" CssClass="form-control mx-2"></asp:DropDownList>
        </div>
        <asp:Button ID="btnAgregarRelacion" runat="server" Text="Agregar Relación" CssClass="btn btn-primary" OnClick="btnAgregarRelacion_Click" />
    </div>

</asp:Content>
