<%@ Page Title="Gestión de Prioridades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prioridades.aspx.cs" Inherits="SistemaDeGestionDeTareas.Prioridades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2>Gestión de Prioridades</h2>

        <div class="row mb-3">
            <div class="col-md-6">
                <asp:Button ID="btnNuevo" runat="server" Text="Nueva Prioridad" CssClass="btn btn-primary"
                    OnClick="btnNuevo_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">

                <asp:GridView ID="gvPrioridades" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-striped table-bordered"
                    DataKeyNames="IdPrioridad"
                    OnRowCommand="gvPrioridades_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="IdPrioridad" HeaderText="ID" />
                        <asp:BoundField DataField="NombrePrioridad" HeaderText="Nombre" />
                        <asp:BoundField DataField="Nivel" HeaderText="Nivel" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    Text="Editar"
                                    CommandName="Editar"
                                    CommandArgument='<%# Eval("IdPrioridad") %>'
                                    CssClass="btn btn-warning btn-sm me-2"></asp:LinkButton>

                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    Text="Eliminar"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("IdPrioridad") %>'
                                    CssClass="btn btn-danger btn-sm"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>

            </div>
        </div>
    </div>

</asp:Content>
