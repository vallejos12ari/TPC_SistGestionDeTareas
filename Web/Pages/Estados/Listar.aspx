<%@ Page Title="Estados" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
         AutoEventWireup="true" CodeBehind="Listar.aspx.cs"
         Inherits="Web.Pages.Estados.Listar" %>

<asp:Content ID="ContenidoEstados" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-content-center w-100">
        <p class="fw-bold fs-3">Estados</p>

        <div class="d-flex gap-2">
            <a href="Flujo.aspx" class="btn btn-secondary mb-3">Modificar flujo</a>
            <a href="Crear.aspx" class="btn btn-primary mb-3">Crear</a>
        </div>
    </div>

    <asp:HiddenField ID="IdEstadoAEliminar" runat="server" />

    <asp:GridView ID="TablaEstados" runat="server"
                  CssClass="table table-striped table-bordered"
                  AutoGenerateColumns="False"
                  GridLines="None">

        <Columns>

            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

            <asp:TemplateField HeaderText="Color">
                <ItemTemplate>
                    <div style='width: 20px; height: 20px; border-radius: 4px; border: 1px solid #ccc; background-color:<%# Eval("Color") %>'></div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Estado final">
                                  <ItemTemplate>
                                      <p><%# (byte)Eval("EsFinal") == 1 ? "Si" : "No" %></p>
                                  </ItemTemplate>
                              </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>

                    <a class="btn btn-sm btn-outline-primary" href='<%# "Editar.aspx?id=" + Eval("Id") %>'>
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
                    ¿Seguro querés eliminar este estado?
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

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
            document.getElementById("<%= IdEstadoAEliminar.ClientID %>").value = id
            let modal = new bootstrap.Modal(document.getElementById('ModalEliminar'))
            modal.show()
        }
    </script>

</asp:Content>
