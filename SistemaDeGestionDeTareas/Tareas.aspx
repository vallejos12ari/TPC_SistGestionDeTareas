<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tareas.aspx.cs" Inherits="SistemaDeGestionDeTareas.Tareas" %>
<asp:Content ID="ContenidoTareas" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Gestión de Tareas</h2>
         <p>Todas las tareas del sistema.</p>
    
         <div class="mb-3">
             <asp:Button ID="btnNuevaTarea" runat="server" Text="Nueva Tarea" CssClass="btn btn-success"
      />
         </div>
    
         <div class="card">
            <div class="card-header">
                Lista de Tareas
            </div>
            <div class="card-body">
                <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" EmptyDataText="No hay tareas para mostrar.">
                    <Columns>
                        <asp:BoundField HeaderText="ID" />
                        <asp:BoundField HeaderText="Título" />
                        <asp:BoundField HeaderText="Asignado a" />
                        <asp:BoundField HeaderText="Prioridad" />
                        <asp:BoundField HeaderText="Estado" />
                        <asp:BoundField HeaderText="Vencimiento" />

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


