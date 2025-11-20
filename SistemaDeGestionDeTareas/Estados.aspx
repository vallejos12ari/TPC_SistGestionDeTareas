<%@ Page Title="Gestión de Estados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estados.aspx.cs" Inherits="SistemaDeGestionDeTareas.Estados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Estados</h2>

        <div class="row mb-3">
            <div class="col-md-6">
                <asp:Button ID="btnNuevoEstado" runat="server" Text="Nuevo Estado" CssClass="btn btn-primary" OnClick="btnNuevoEstado_Click" />
                <asp:Button ID="btnGestionarOrden" runat="server" Text="Gestionar Orden de Estados" CssClass="btn btn-info ms-2" OnClick="btnGestionarOrden_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvEstados" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEstado"
                    CssClass="table table-striped table-bordered" OnRowCommand="gvEstados_RowCommand"
                    EmptyDataText="No hay estados registrados.">
                    <Columns>
                        <asp:BoundField DataField="IdEstado" HeaderText="ID" SortExpression="IdEstado" ReadOnly="True" />
                        <asp:BoundField DataField="NombreEstado" HeaderText="Nombre" SortExpression="NombreEstado" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdEstado") %>' CssClass="btn btn-sm btn-warning me-2">Editar</asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdEstado") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Estás seguro de que quieres eliminar este estado? Esto también eliminará sus transiciones.');">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Modal para Crear/Editar Estado -->
        <div class="modal fade" id="estadoModal" tabindex="-1" aria-labelledby="estadoModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="estadoModalLabel">
                            <asp:Literal ID="litModalTitle" runat="server"></asp:Literal>
                        </h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:ValidationSummary ID="vsEstado" runat="server" CssClass="alert alert-danger" HeaderText="Por favor, corrige los siguientes errores:" />
                        <div class="mb-3">
                            <label for="<%= txtNombreEstado.ClientID %>" class="form-label">Nombre del Estado</label>
                            <asp:TextBox ID="txtNombreEstado" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombreEstado" runat="server" ControlToValidate="txtNombreEstado"
                                ErrorMessage="El nombre del estado es obligatorio." Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <asp:HiddenField ID="hfIdEstado" runat="server" Value="0" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnGuardarEstado" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarEstado_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openEstadoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('estadoModal'));
            myModal.show();
        }
    </script>
</asp:Content>
