<%@ Page Title="Relaciones" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Listar.aspx.cs"
    Inherits="Web.Pages.Relaciones.Listar" %>

<asp:Content ID="ContenidoRelaciones" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3 class="fw-bold">Relaciones</h3>
        <a href="Crear.aspx" class="btn btn-primary">Crear</a>
    </div>

    <asp:HiddenField ID="IdRelacionAEliminar" runat="server" />

    <asp:GridView ID="TablaRelaciones" runat="server"
                  CssClass="table table-striped table-bordered"
                  AutoGenerateColumns="False"
                  GridLines="None">

        <Columns>

            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>

                    <a class="btn btn-sm btn-outline-primary"
                       href='<%# "Editar.aspx?id=" + Eval("Id") %>'>
                        Editar
                    </a>

                    <button type="button"
                            class="btn btn-sm btn-outline-danger ms-2"
                            onclick="AbrirModalEliminar('<%# Eval("Id") %>')">
                        Eliminar
                    </button>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <div class="modal fade" id="ModalEliminar" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Confirmar eliminación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    ¿Seguro querés eliminar este tipo de relación?
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        Cancelar
                    </button>

                    <asp:Button ID="BotonConfirmarEliminar" runat="server"
                                CssClass="btn btn-danger"
                                Text="Eliminar"
                                OnClick="ClickBotonConfirmarEliminar" />
                </div>

            </div>
        </div>
    </div>

    <script>
        function AbrirModalEliminar(id) {
            document.getElementById("<%= IdRelacionAEliminar.ClientID %>").value = id
            let modal = new bootstrap.Modal(document.getElementById('ModalEliminar'))
            modal.show()
        }
    </script>

</asp:Content>
