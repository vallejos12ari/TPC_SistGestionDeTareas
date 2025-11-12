<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="SistemaDeGestionDeTareas.Usuarios" %>

<asp:Content ID="ContenidoUsuarios" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Usuarios</h2>
    <p>Lista de usuarios del sistema</p>
    
    <div class="card mb-3">
        <div class="card-header d-flex justify-content-between align-items-center">
            <span>Lista de Usuarios</span>
            <asp:Button ID="btnCrear" runat="server" Text="Crear nuevo usuario" CssClass="btn btn-success btn-sm" OnClick="btnCrear_Click" />
        </div>

        <div class="card-body">
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-bordered"
                OnRowCommand="gvUsuarios_RowCommand">
                <Columns>
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Rol" HeaderText="Rol" />
                    <asp:BoundField DataField="Activo" HeaderText="Activo" />
                   <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha de Creación" 
                                   DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-primary btn-sm me-1"
                                CommandName="Editar" CommandArgument='<%# Eval("IdUsuario") %>' />
                            <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-danger btn-sm"
                                CommandName="Borrar" CommandArgument='<%# Eval("IdUsuario") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
