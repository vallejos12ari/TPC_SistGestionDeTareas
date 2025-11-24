<%@ Page Title="Gestión de Orden de Estados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenEstados.aspx.cs" Inherits="SistemaDeGestionDeTareas.OrdenEstados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Gestion de Orden de Estados</h2>

        <div class="row mb-3">
            <div class="col-md-12">
                <asp:Button ID="btnVolverEstados" runat="server" Text="Volver a Estados" CssClass="btn btn-secondary" OnClick="btnVolverEstados_Click" />
            </div>
        </div>

        <div class="row mb-4">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        Agregar Nueva Transicion
                    </div>
                    <div class="card-body">
                        <asp:ValidationSummary ID="vsOrdenEstado" runat="server" CssClass="alert alert-danger" HeaderText="Por favor, corrige los siguientes errores:" />
                        <div class="mb-3">
                            <label for="<%= ddlEstadoActual.ClientID %>" class="form-label">Estado Actual</label>
                            <asp:DropDownList ID="ddlEstadoActual" runat="server" CssClass="form-select"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEstadoActual" runat="server" ControlToValidate="ddlEstadoActual"
                                ErrorMessage="Debe seleccionar un estado actual." InitialValue="0" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <label for="<%= ddlEstadoDestino.ClientID %>" class="form-label">Estado Destino</label>
                            <asp:DropDownList ID="ddlEstadoDestino" runat="server" CssClass="form-select"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEstadoDestino" runat="server" ControlToValidate="ddlEstadoDestino"
                                ErrorMessage="Debe seleccionar un estado destino." InitialValue="0" Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Button ID="btnAgregarTransicion" runat="server" Text="Agregar Transicion" CssClass="btn btn-primary" OnClick="btnAgregarTransicion_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <h3>Transiciones Existentes</h3>
                <asp:GridView ID="gvOrdenEstados" runat="server" AutoGenerateColumns="False" DataKeyNames="IdOrden"
                    CssClass="table table-striped table-bordered" OnRowCommand="gvOrdenEstados_RowCommand"
                    EmptyDataText="No hay transiciones de estado registradas.">
                    <Columns>
                        <asp:BoundField DataField="IdOrden" HeaderText="ID" SortExpression="IdOrden" ReadOnly="True" />
                        <asp:BoundField DataField="EstadoActual.NombreEstado" HeaderText="Estado Actual" SortExpression="EstadoActual.NombreEstado" />
                        <asp:BoundField DataField="EstadoDestino.NombreEstado" HeaderText="Estado Destino" SortExpression="EstadoDestino.NombreEstado" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdOrden") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Estas seguro de que quieres eliminar esta transicion?');">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
