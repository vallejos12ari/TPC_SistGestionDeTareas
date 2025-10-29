<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SistemaDeGestionDeTareas.Usuarios" %>
<asp:Content ID="ContenidoUsuarios" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestion de Usuarios</h2>
    <p>Lista de usuarios del sistema</p>
    
    <div class="card">
        <div class ="card-header">
            Lista de Usuarios
        </div>
        <div card="card-body">
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
                <Columns>
                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Rol" HeaderText="Rol"/>
                    <asp:BoundField DataField="Activo" HeaderText="Activo" />
                     <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                     <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-primary" />
                     <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
