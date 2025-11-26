<%@ Page Title="Flujo de Estados" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
         AutoEventWireup="true" CodeBehind="Flujo.aspx.cs"
         Inherits="Web.Pages.Estados.Flujo" %>

<asp:Content ID="ContenidoFlujo" ContentPlaceHolderID="MainContent" runat="server">

    <p class="fw-bold fs-3">Flujo de Estados</p>

    <asp:Label ID="ErrorFlujo" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>
    
      <div class="col-4">
            <label class="form-label">Estado inicial</label>
            <asp:DropDownList ID="SelectInicial" runat="server"
                              CssClass="form-select"
                              AutoPostBack="true"
                              OnSelectedIndexChanged="SelectInicial_SelectedIndexChanged">
            </asp:DropDownList>
        </div>

    <div class="row mb-4">
        <div class="col-4">
            <label class="form-label">Estado origen</label>
            <asp:DropDownList ID="SelectOrigen" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <div class="col-4">
            <label class="form-label">Estado destino</label>
            <asp:DropDownList ID="SelectDestino" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <div class="col-4 d-flex align-items-end">
            <asp:Button ID="BotonAgregar" runat="server" 
                        Text="Agregar flujo"
                        CssClass="btn btn-primary w-100"
                        OnClick="ClickBotonAgregarFlujo" />
        </div>
    </div>

    <asp:GridView ID="TablaFlujo" runat="server"
                  CssClass="table table-striped table-bordered"
                  AutoGenerateColumns="False"
                  GridLines="None">

        <Columns>

            <asp:BoundField DataField="Origen.Nombre" HeaderText="Origen" />
            <asp:BoundField DataField="Destino.Nombre" HeaderText="Destino" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="BotonEliminar" runat="server"
                                CssClass="btn btn-sm btn-outline-danger"
                                Text="Eliminar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="ClickBotonEliminar" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
