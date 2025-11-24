<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteRendimiento.aspx.cs" Inherits="SistemaDeGestionDeTareas.ReporteRendimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reporte de Rendimiento</h2>
    <p>Resumen del rendimiento de cada usuario según sus tareas.</p>

    <!-- FILTROS -->
<div class="card p-3 mb-3">

     <div class="card mt-3">
        <div class="card-header">
            Resultados del Reporte
        </div>

        <div class="card-body">
            <asp:GridView ID="gvReporte" runat="server"
                CssClass="table table-bordered table-hover"
                AutoGenerateColumns="False">

                <Columns>

                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />

                    <asp:BoundField DataField="TareasCreadas" HeaderText="Creadas" />

                    <asp:BoundField DataField="TareasAsignadas" HeaderText="Asignadas" />

                    <asp:BoundField DataField="TareasCompletadas" HeaderText="Completadas" />

                    <asp:BoundField DataField="TareasPendientes" HeaderText="Pendientes" />

                    <asp:BoundField DataField="TareasVencidas" HeaderText="Vencidas" />

                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>


