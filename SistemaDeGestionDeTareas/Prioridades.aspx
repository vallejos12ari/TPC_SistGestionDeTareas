<%@ Page Title="Gestión de Prioridades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Prioridades.aspx.cs" Inherits="SistemaDeGestionDeTareas.Prioridades" %>

<asp:Content ID="ContenidoPrioridades" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Prioridades</h2>
    <p>Lista de prioridades disponibles en el sistema</p>

    <div class="card mb-3">
        <div class="card-header d-flex justify-content-between align-items-center">
            <span>Lista de Prioridades</span>
            <asp:Button ID="btnCrear" runat="server" Text="Crear nueva prioridad"
                CssClass="btn btn-success btn-sm" OnClick="btnCrear_Click1" />
        </div>

        <div class="card-body">
            <asp:GridView ID="gvPrioridades" runat="server" AutoGenerateColumns="False"
                CssClass="table table-hover table-bordered"
                OnRowCommand="gvPrioridades_RowCommand1"
                DataKeyNames="IdPrioridad">

                <Columns>

                   
                    <asp:BoundField DataField="NombrePrioridad" HeaderText="Nombre" />
                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" />

                   
                    <asp:TemplateField HeaderText="Color">
                        <ItemTemplate>
                            <div style="width:25px; height:25px; background-color:<%# Eval("Color") %>; border:1px solid #000;"></div>
                        </ItemTemplate>
                    </asp:TemplateField>

                   
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>

                            <asp:Button ID="btnEditar" runat="server" Text="Editar"
                                CssClass="btn btn-primary btn-sm me-1"
                                CommandName="Editar" CommandArgument='<%# Eval("IdPrioridad") %>' />

                            <asp:Button ID="btnBorrar" runat="server" Text="Borrar"
                                CssClass="btn btn-danger btn-sm"
                                CommandName="Borrar" CommandArgument='<%# Eval("IdPrioridad") %>' />

                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center" />

                    </asp:TemplateField>

                </Columns>

                <HeaderStyle CssClass="thead-dark" />

            </asp:GridView>
        </div>
    </div>

</asp:Content>
