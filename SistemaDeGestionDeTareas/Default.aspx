<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaDeGestionDeTareas._Default" %>

<asp:Content ID="ContenidoHome" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center" style="padding: 40px 0;">
        <h1 class="display-4">Panel Principal</h1>
        <p class="lead">Seleccione una opción para comenzar a gestionar el sistema.</p>
    </div>

    <hr class="my-4" />

    <div class="row justify-content-center g-4">


        <div class="col-md-4">
            <div class="d-grid">
                <a href="~/Usuarios.aspx" runat="server" class="btn btn-primary btn-lg p-4">Gestionar Usuarios
                </a>
            </div>
        </div>


        <div class="col-md-4">
            <div class="d-grid">
                <a href="~/Tareas.aspx" runat="server" class="btn btn-secondary btn-lg p-4">Gestionar Tareas
                </a>
            </div>
        </div>


        <div class="col-md-4">
            <div class="d-grid">
                <a href="~/Login.aspx" runat="server" class="btn btn-danger btn-lg p-4">Salir
                </a>
            </div>
        </div>

    </div>
</asp:Content>
