<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestEmail.aspx.cs" Inherits="SistemaDeGestionDeTareas.TestEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Prueba de Envío de Email</h2>
    <p>Esta pantalla sirve para probar el envío de correos antes de integrarlo con el ABM de tareas.</p>

    <div class="card mt-3">
        <div class="card-header">
            Enviar Email de Prueba
        </div>

        <div class="card-body">

            <!-- EMAIL DESTINO -->
            <div class="mb-3">
                <label>Correo del destinatario:</label>
                <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" />
            </div>

            <!-- TITULO DE TAREA -->
            <div class="mb-3">
                <label>Título de la tarea asignada:</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" />
            </div>

            <!-- NOMBRE ASIGNADOR -->
            <div class="mb-3">
                <label>Nombre de quien asigna la tarea:</label>
                <asp:TextBox ID="txtAsignador" runat="server" CssClass="form-control" />
            </div>

            <!-- BOTÓN ENVIAR -->
            <asp:Button ID="btnProbar" runat="server" CssClass="btn btn-primary"
                Text="Enviar Email"
                OnClick="btnProbar_Click" />

            <!-- Resultado -->
            <asp:Label ID="lblResultado" runat="server" CssClass="mt-3 d-block fw-bold"></asp:Label>

        </div>
    </div>

</asp:Content>
