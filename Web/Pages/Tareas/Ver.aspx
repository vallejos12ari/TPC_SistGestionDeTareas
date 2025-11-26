<%@ Page Title="Ver Tarea" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Ver.aspx.cs"
    Inherits="Web.Pages.Tareas.Ver" %>

<asp:Content ID="ContenidoVer" ContentPlaceHolderID="MainContent" runat="server">

   <div class="d-flex justify-content-between align-items-center mb-4">
       <h3 id="LblTitulo" runat="server" class="fw-bold m-0"></h3>
   
       <div class="d-flex gap-2">
            <a href="Listar.aspx" class="btn btn-secondary">Volver</a>
           
           <asp:Panel ID="PanelCambiarEstado" runat="server">
               <button type="button" class="btn btn-warning" onclick="AbrirModalCambiarEstado()">Cambiar estado</button>
           </asp:Panel>
           
           <asp:Panel ID="PanelAgregarHoras" runat="server">
               <button type="button" class="btn btn-primary" onclick="AbrirModalHoras()">Agregar horas</button>
           </asp:Panel>
       
           <div id="PanelAccionesAdmin" runat="server" class="d-flex gap-2">
               <a id="BotonEditar" runat="server" class="btn btn-secondary">Editar</a>
               <button type="button" class="btn btn-danger" onclick="AbrirModalEliminar()">Eliminar</button>
           </div>
       </div>
   </div>

    <asp:Label ID="ErrorVer" runat="server" CssClass="text-danger fw-bold mb-3 d-block"></asp:Label>

    <div>

        <p class="mb-3">
            <strong>Descripción</strong><br />
            <asp:Label ID="LblDescripcion" runat="server" />
        </p>

        <div class="row mb-4">

            <div class="col-md-4 mb-3">
                <strong>Estado:</strong><br />
                <span id="LblEstado" runat="server"></span>
            </div>

            <div class="col-md-4 mb-3">
                <strong>Prioridad:</strong><br />
                <span id="LblPrioridad" runat="server"></span>
            </div>
            
            <div class="col-md-4 mb-3">
                      <strong>Fecha de vencimiento:</strong><br />
                      <asp:Label ID="LblVencimiento" runat="server" />
            </div>

            <div class="col-md-4 mb-3">
                <strong>Asignado a:</strong><br />
                <asp:Label ID="LblUsuarioAsignado" runat="server" />
            </div>
            
            
            <div class="col-md-4 mb-3">
                <strong>Informador:</strong><br />
                <asp:Label ID="LblUsuarioCreador" runat="server" />
            </div>
            
            <div class="col-md-4 mb-3">
                                    <strong>Tags:</strong>
                                    <div class="mt-2">
                                        <asp:Repeater ID="RepeaterTags" runat="server">
                                            <ItemTemplate>
                                                <span class="badge rounded-pill text-black me-1 mb-1"
                                                      style='background-color:<%# Eval("Color") %>'>
                                                    <%# Eval("Nombre") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
            </div>
            

            <div class="col-md-4 mb-3">
                <strong>Horas estimadas:</strong><br />
                <asp:Label ID="LblHoras" runat="server" /> <span> hs</span>
            </div>
            
            <div class="col-md-8 mb-3">
                <strong>Horas cargadas:</strong><br />
            
                <a id="LblHorasCargadas"
                   runat="server"
                   class="fw-bold text-decoration-none"
                   href="#"
                   onclick="AbrirModalHorasCargadas()">
                </a>
            </div>

            <div class="col-md-4 mb-3">
                <strong>Tipo de relación:</strong><br />
                <asp:Label ID="LblTipoRelacion" runat="server" />
            </div>

            <div class="col-md-4 mb-3">
                <strong>Tarea relacionada:</strong><br />
                    <a id="linkTareaRelacionada" runat="server" class="text-decoration-none"></a>
            </div>

        </div>

        

        <div class="mb-4">
            <strong>Imágenes:</strong>
            <div class="mt-3 d-flex flex-wrap gap-3">

                <asp:Repeater ID="RepeaterImagenes" runat="server">
                    <ItemTemplate>
                        <a href="<%# Eval("Path") %>" target="_blank">
                            <img src="<%# Eval("Path") %>"
                                 class="shadow-sm"
                                 style="width:120px;height:120px;object-fit:cover;border-radius:6px;" />
                        </a>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>

    </div>

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
                    <button class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="BotonEliminarConfirmado" runat="server"
                                CssClass="btn btn-danger"
                                Text="Eliminar"
                                OnClick="ClickEliminar" />
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="ModalEliminarComentario" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
    
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar comentario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
    
                <div class="modal-body">
                    ¿Seguro que querés eliminar este comentario?
                </div>
    
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
    
                    <asp:Button ID="BotonEliminarComentarioConfirmado"
                                runat="server"
                                CssClass="btn btn-danger"
                                Text="Eliminar"
                                OnClick="ClickEliminarComentarioConfirmado" />
                </div>
    
            </div>
        </div>
    </div>
    
    <input type="hidden" id="HiddenComentarioId" runat="server" />

    
    <hr class="my-4" />
    
    <h5 class="fw-bold mb-3">Comentarios</h5>
    
    <asp:Label ID="ErrorComentarios" runat="server"
               CssClass="text-danger fw-bold d-block mb-2"></asp:Label>
    
    <asp:Repeater ID="RepeaterComentarios" runat="server">
        <ItemTemplate>
            <div class="border rounded p-3 mb-2 bg-light d-flex justify-content-between align-items-start">
    
                <div>
                    <div class="fw-bold"><%# Eval("UsuarioNombre") %></div>
                    <div><%# Eval("Texto") %></div>
                    <div class="text-muted small"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></div>
                </div>
    
         <asp:LinkButton ID="BotonEliminarComentario"
                         runat="server"
                         CssClass="btn btn-sm btn-outline-danger"
                         Text="Eliminar"
                         CommandName="EliminarComentario"
                         CommandArgument='<%# Eval("Id") %>' />

            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    <div class="mt-4">
        <asp:TextBox ID="TextoComentario" runat="server"
                     CssClass="form-control mb-2"
                     TextMode="MultiLine" Rows="3"></asp:TextBox>
    
        <asp:Button ID="BotonAgregarComentario"
                    runat="server"
                    CssClass="btn btn-primary btn-sm"
                    Text="Agregar comentario"
                    OnClick="ClickAgregarComentario" />
    </div>
    
    <div class="modal fade" id="ModalHoras" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
    
                <div class="modal-header">
                    <h5 class="modal-title">Agregar horas</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
    
                <div class="modal-body">
    
                    <asp:Label ID="ErrorHoras" runat="server"
                               CssClass="text-danger fw-bold d-block mb-2"></asp:Label>
    
                    <label class="form-label">Cantidad de horas</label>
    
                    <asp:TextBox ID="InputHoras" runat="server"
                                 CssClass="form-control"
                                 TextMode="Number"
                                 step="0.01" />
                </div>
    
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
    
                    <asp:Button ID="BotonAgregarHoras" runat="server"
                                CssClass="btn btn-primary"
                                Text="Agregar"
                                OnClick="ClickAgregarHoras" />
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="ModalHorasCargadas" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
    
                <div class="modal-header">
                    <h5 class="modal-title">Horas cargadas</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
    
                <div class="modal-body">
    
                    <asp:Repeater ID="RepeaterHorasCargadas" runat="server">
                        <ItemTemplate>
                            <div class="border rounded p-2 mb-2 bg-light d-flex justify-content-between align-content-center">
                                
                                <div><%# Eval("UsuarioNombre") %></div>
                                <div><%# Eval("Horas", "{0:0.##}") %> hs</div>
                                <div class="text-muted small"><%# Eval("Dia", "{0:dd/MM/yyyy}") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
    
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
    
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="ModalCambiarEstado" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
    
                <div class="modal-header">
                    <h5 class="modal-title">Cambiar estado</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
    
                <div class="modal-body">
                    <asp:Label ID="ErrorCambioEstado" runat="server"
                               CssClass="text-danger fw-bold d-block mb-2"></asp:Label>
    
                    <label class="form-label">Nuevo estado</label>
    
                    <asp:DropDownList ID="SelectNuevoEstado"
                                      runat="server"
                                      CssClass="form-select">
                    </asp:DropDownList>
    
                </div>
    
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
    
                    <asp:Button ID="BotonConfirmarCambioEstado"
                                runat="server"
                                CssClass="btn btn-warning"
                                Text="Cambiar"
                                OnClick="ClickCambiarEstado" />
                </div>
    
            </div>
        </div>
    </div>

    <script>
        function AbrirModalEliminar() {
            new bootstrap.Modal(document.getElementById('ModalEliminar')).show();
        }
        
        function AbrirModalEliminarComentario(id) {
            document.getElementById("<%= HiddenComentarioId.ClientID %>").value = id;
            new bootstrap.Modal(document.getElementById('ModalEliminarComentario')).show();
        }
        
        function AbrirModalHorasCargadas() {
            new bootstrap.Modal(document.getElementById('ModalHorasCargadas')).show();
        }
        
        function AbrirModalHoras() {
            new bootstrap.Modal(document.getElementById('ModalHoras')).show();
        }
        
        function AbrirModalCambiarEstado() {
            new bootstrap.Modal(document.getElementById('ModalCambiarEstado')).show();
        }
    </script>

</asp:Content>
