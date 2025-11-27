<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Panel de Reportes</h3>

    <div class="row g-3 mb-4">
        <div class="col-md-3">
            <label class="form-label">Fecha desde</label>
            <asp:TextBox ID="TextoDesde" runat="server" TextMode="Date" CssClass="form-control" />
        </div>

        <div class="col-md-3">
            <label class="form-label">Fecha hasta</label>
            <asp:TextBox ID="TextoHasta" runat="server" TextMode="Date" CssClass="form-control" />
        </div>

        <div class="col-md-2 d-flex align-items-end">
            <asp:Button ID="BotonFiltrar" runat="server" Text="Filtrar"
                        CssClass="btn btn-primary w-100"
                        OnClick="ClickBotonFiltrar" />
        </div>
    </div>
    
    <div class="card mb-4">
        <div class="card-header fw-bold">Tareas por Estado</div>
        <div class="card-body">
            <asp:GridView ID="TablaTareasPorEstado" runat="server"
                CssClass="table table-bordered table-striped"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="card mb-4">
        <div class="card-header fw-bold">Tareas Vencidas vs En Fecha</div>
        <div class="card-body">
            <asp:GridView ID="TablaVencidas" runat="server"
                CssClass="table table-bordered table-striped"
                AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </div>

    <div class="card mb-4">
        <div class="card-header fw-bold">Horas Cargadas por Usuario</div>
        <div class="card-body">
            <asp:GridView ID="TablaHorasPorUsuario" runat="server"
                CssClass="table table-bordered table-striped"
                AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </div>

</asp:Content>