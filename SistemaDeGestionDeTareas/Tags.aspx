<%@ Page Title="Gestión de Tags" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="SistemaDeGestionDeTareas.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2>Gestión de Tags</h2>

        <div class="row mb-3">
            <div class="col-md-6">
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Tag" CssClass="btn btn-primary"
                    OnClick="btnNuevo_Click" />
            </div>
        </div>

        <asp:GridView ID="gvTags" runat="server"
            CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False"
            DataKeyNames="IdTag"
            OnRowCommand="gvTags_RowCommand">
            
           <Columns>

    <asp:BoundField DataField="IdTag" HeaderText="ID" />

    <asp:BoundField DataField="Nombre" HeaderText="Nombre Tag" />

    <asp:TemplateField HeaderText="Color">
        <ItemTemplate>
            <div style="width:25px; height:25px; background-color:<%# Eval("Color") %>; border:1px solid #000;"></div>
        </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Acciones">
        <ItemTemplate>

            <asp:LinkButton ID="btnEditar" runat="server"
                Text="Editar"
                CommandName="Editar"
                CommandArgument='<%# Eval("IdTag") %>'
                CssClass="btn btn-warning btn-sm me-2">
            </asp:LinkButton>

            <asp:LinkButton ID="btnEliminar" runat="server"
                Text="Eliminar"
                CommandName="Eliminar"
                CommandArgument='<%# Eval("IdTag") %>'
                CssClass="btn btn-danger btn-sm">
            </asp:LinkButton>

        </ItemTemplate>
    </asp:TemplateField>

</Columns>


        </asp:GridView>

    </div>

</asp:Content>
