<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisTareas.aspx.cs" Inherits="SistemaDeGestionDeTareas.MisTareas" %>
<asp:Content ID="ContenidoMisTareas" ContentPlaceHolderID="MainContent" runat="server">
<h2>Mis Tareas</h2>
     <p>Aquí puedes ver un listado de todas las tareas que te han sido asignadas.</p>

     <div class="mb-3">
         <asp:Button ID="btnNuevaTarea" runat="server" Text="Crear Nueva Tarea" CssClass="btn btn-success" />
     </div>

     <div class="card">
        <div class="card-header">
            Tareas Asignadas a Mí
        </div>
        <div class="card-body">
            <asp:GridView ID="gvMisTareas" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" EmptyDataText="No tienes tareas asignadas.">
                <Columns>
                    <asp:BoundField HeaderText="ID" />
                    <asp:BoundField HeaderText="Título" />
                    <asp:BoundField HeaderText="Prioridad" />
                    <asp:BoundField HeaderText="Estado" />
                    <asp:BoundField HeaderText="Vencimiento" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnVerDetalles" runat="server" Text="Detalles" CssClass= "btn btn-sm btn-info" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
