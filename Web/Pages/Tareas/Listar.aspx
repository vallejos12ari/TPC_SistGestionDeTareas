<%@ Page Title="Tareas" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Listar.aspx.cs"
    Inherits="Web.Pages.Tareas.Listar" %>

<asp:Content ID="ContenidoTareas" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-3">
        <p class="fw-bold fs-3 m-0">Tareas</p>

        <div class="d-flex gap-2">

            <button class="btn btn-secondary"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#PanelFiltros">
                Filtrar
            </button>

            <asp:Panel ID="PanelBotonCrear" runat="server" Visible="false">
                <a href="Crear.aspx" class="btn btn-primary">Crear tarea</a>
            </asp:Panel>

        </div>
    </div>

    <div class="collapse mb-4" id="PanelFiltros">
        <div class="card p-3">

            <div class="row g-3">

                <div class="col-md-3">
                    <label class="form-label">Estado</label>
                    <asp:DropDownList ID="FiltroEstado" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label">Prioridad</label>
                    <asp:DropDownList ID="FiltroPrioridad" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-3" id="FiltroUsuarioContainer" runat="server">
                    <label class="form-label">Usuario asignado</label>
                    <asp:DropDownList ID="FiltroUsuarioAsignado" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label">Tag</label>
                    <asp:DropDownList ID="FiltroTags" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label">Vencimiento desde</label>
                    <asp:TextBox ID="FiltroDesde" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-3">
                    <label class="form-label">Vencimiento hasta</label>
                    <asp:TextBox ID="FiltroHasta" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-6 d-flex align-items-end">
                    <div class="w-100">
                        <label class="form-label">Buscar por texto</label>
                        <asp:TextBox ID="FiltroTexto" runat="server" CssClass="form-control" />
                    </div>

                    <asp:Button ID="BotonFiltrar" runat="server"
                                Text="Aplicar filtros"
                                CssClass="btn btn-primary ms-3 mb-1"
                                OnClick="ClickBotonFiltrar" />
                    
                    <asp:Button ID="BotonReniciarFiltro" runat="server"
                                                    Text="Reiniciar"
                                                    CssClass="btn btn-warning ms-3 mb-1"
                                                    OnClick="ClickBotonReinicar" />
                </div>

            </div>

        </div>
    </div>

    <asp:HiddenField ID="IdTareaAEliminar" runat="server" />

        <asp:GridView ID="TablaTareas" runat="server"
                      CssClass="table table-striped table-bordered align-middle"
                      AutoGenerateColumns="False"
                      GridLines="None">
            
        <Columns>
            <asp:BoundField DataField="Titulo" HeaderText="Título" />

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <span style='color:<%# Eval("EstadoColor") %>'>
                        <%# Eval("EstadoNombre") %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Prioridad">
                <ItemTemplate>
                    <span style='color:<%# Eval("PrioridadColor") %>'>
                        <%# Eval("PrioridadNombre") %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="UsuarioAsignadoNombre" HeaderText="Asignado a" />

         <asp:TemplateField HeaderText="Tags">
             <ItemTemplate>
                 <asp:Repeater ID="RepeaterTags" runat="server" DataSource='<%# Eval("Tags") %>'>
                     <ItemTemplate>
                         <div class="mb-1">
                             <span class="badge rounded-pill text-black"
                                   style='background-color:<%# Eval("Color") %>;'>
                                 <%# Eval("Nombre") %>
                             </span>
                         </div>
                     </ItemTemplate>
                 </asp:Repeater>
             </ItemTemplate>
         </asp:TemplateField>

            <asp:BoundField DataField="FechaVencimientoFormateada" HeaderText="Vence" />

      <asp:TemplateField HeaderText="Acciones">
          <ItemTemplate>
      
              <div class="d-flex align-items-center">
      
                  <a class="btn btn-sm btn-outline-secondary"
                     href='<%# "Ver.aspx?id=" + Eval("Id") %>'>
                      Ver
                  </a>
      
                  <asp:Panel ID="PanelAdmin" runat="server" Visible="false" CssClass="d-flex">
      
                      <a class="btn btn-sm btn-outline-primary ms-2"
                         href='<%# "Editar.aspx?id=" + Eval("Id") %>'>
                          Editar
                      </a>
      
                      <button type="button"
                              class="btn btn-sm btn-outline-danger ms-2"
                              onclick="AbrirModalEliminar('<%# Eval("Id") %>')">
                          Eliminar
                      </button>
      
                  </asp:Panel>
      
              </div>
      
          </ItemTemplate>
      </asp:TemplateField>


        </Columns>

    </asp:GridView>

    <asp:Panel ID="PanelSinTareas" runat="server" Visible="false" CssClass="text-center py-4">
        <p class="text-muted fs-5 mb-0">Aún no hay tareas cargadas.</p>
    </asp:Panel>

    <div class="modal fade" id="ModalEliminar" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Confirmar eliminación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    ¿Seguro querés eliminar esta tarea?
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
            document.getElementById("<%= IdTareaAEliminar.ClientID %>").value = id
            let modal = new bootstrap.Modal(document.getElementById('ModalEliminar'))
            modal.show()
        }
    </script>

</asp:Content>
