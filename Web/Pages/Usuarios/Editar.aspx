<%@ Page Title="Editar Usuario" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
         AutoEventWireup="true" CodeBehind="Editar.aspx.cs"
         Inherits="Web.Pages.Usuarios.Editar" %>

<asp:Content ID="ContenidoUsuario" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Editar usuario</h3>
    
    <asp:Label ID="ErrorEditar" runat="server"
           CssClass="text-danger fw-bold d-block mb-3"></asp:Label>
    <asp:HiddenField ID="IdUsuarioHidden" runat="server" />

    <div class="row g-3">

        <div class="col-md-6">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="TextoNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6">
            <label class="form-label">Email</label>
            <asp:TextBox ID="TextoEmail" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6">
            <label class="form-label">Rol</label>
            <asp:DropDownList ID="SelectRol" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>
        
        <asp:Panel ID="PanelAsignados" runat="server" Visible="false" CssClass="mt-5">
        
            <h5 class="fw-bold mb-3">Usuarios asignados</h5>
        
            <div class="row g-3 align-items-end">
        
                <div class="col-md-6">
                    <label class="form-label">Seleccionar usuario</label>
                    <asp:DropDownList ID="SelectUsuarioAsignar" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
        
                <div class="col-md-3">
                    <asp:Button ID="BotonAgregarAsignado" runat="server"
                                Text="Agregar"
                                CssClass="btn btn-primary w-100"
                                OnClick="ClickBotonAgregarAsignado" />
                </div>
        
            </div>
        
            <div class="mt-4">
        
                <asp:Panel ID="PanelSinAsignados" runat="server" Visible="false">
                    <div class="text-muted">No tiene usuarios asignados.</div>
                </asp:Panel>
        
                <asp:GridView ID="TablaAsignados" runat="server"
                              CssClass="table table-striped table-bordered mt-3"
                              AutoGenerateColumns="False"
                              GridLines="None">
        
                    <Columns>
        
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
        
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="BotonDesasignar" runat="server"
                                            Text="Desasignar"
                                            CssClass="btn btn-outline-danger btn-sm"
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClick="ClickBotonDesasignar" />
                            </ItemTemplate>
                        </asp:TemplateField>
        
                    </Columns>
        
                </asp:GridView>
        
            </div>
        </asp:Panel>

        <div class="mt-4 d-flex gap-3">
            <asp:Button ID="BotonGuardar" runat="server" Text="Guardar" CssClass="btn btn-success"
                        OnClick="ClickBotonGuardar" />

            <button type="button" class="btn btn-warning" onclick="AbrirModalReiniciar()">
                Reiniciar contraseña
            </button>

            <a href="Listar.aspx" class="btn btn-secondary">Volver</a>
        </div>

    </div>


    <div class="modal fade" id="ModalReiniciar" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Confirmar reinicio</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    A este usuario se le reiniciará la contraseña a <strong>gestiondetareas</strong>.
                    La próxima vez que ingrese, se le pedirá cambiarla. ¿Está seguro?
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>

                    <asp:Button ID="BotonConfirmarReinicio" runat="server"
                                CssClass="btn btn-danger"
                                Text="Reiniciar contraseña"
                                OnClick="ClickBotonConfirmarReinicio" />
                </div>

            </div>
        </div>
    </div>

    <script>
        function AbrirModalReiniciar() {
            let modal = new bootstrap.Modal(document.getElementById('ModalReiniciar'))
            modal.show()
        }
    </script>
    
    <script>
        const ddlRol = document.getElementById("<%= SelectRol.ClientID %>");
        const panelAsignados = document.getElementById("<%= PanelAsignados.ClientID %>");
    
        function ActualizarPanelAsignados() {
            if (ddlRol.value === "USER") {
                panelAsignados.style.display = "none";
            } else {
                panelAsignados.style.display = "block";
            }
        }
    
        ddlRol.addEventListener("change", ActualizarPanelAsignados);
    
        ActualizarPanelAsignados();
    </script>

</asp:Content>
