<%@ Page Title="Editar Tarea" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Editar.aspx.cs"
    Inherits="Web.Pages.Tareas.Editar" %>

<asp:Content ID="ContenidoEditar" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Editar tarea</h3>

    <asp:Label ID="ErrorEditar" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

    <div class="row g-3">

        <div class="col-md-6">
            <label class="form-label">
                Título <span class="text-danger">*</span>
            </label>
            <asp:TextBox ID="TextoTitulo" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6" id="PanelUsuarioAsignado" runat="server">
            <label class="form-label">
                Usuario asignado <span class="text-danger">*</span>
            </label>
            <asp:DropDownList ID="SelectUsuarioAsignado" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <!-- DESCRIPCIÓN -->
        <div class="col-md-6">
            <label class="form-label">Descripción <span class="text-danger">*</span></label>
            <asp:TextBox ID="TextoDescripcion" runat="server"
                         CssClass="form-control" TextMode="MultiLine" Rows="4" />
        </div>
        
          <div class="col-md-6">
                    <label class="form-label">Tags</label>
                    <asp:ListBox ID="SelectTags" runat="server"
                                 CssClass="form-select"
                                 SelectionMode="Multiple"
                                 Rows="6"></asp:ListBox>
                </div>


        <div class="col-md-6">
            <label class="form-label">Prioridad <span class="text-danger">*</span></label>
            <asp:DropDownList ID="SelectPrioridad" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

            <div class="col-md-6">
                <label class="form-label">Vencimiento <span class="text-danger">*</span></label>
                <asp:TextBox ID="TextoVencimiento" runat="server"
                             CssClass="form-control" TextMode="Date" />
            </div>

            <div class="col-md-6">
                <label class="form-label">Tipo de relación</label>
                <asp:DropDownList ID="SelectTipoRelacion" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="form-label">Tarea relacionada</label>
                <asp:DropDownList ID="SelectTareaRelacionada" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
        
                <div class="col-md-6">
                        <label class="form-label">Horas estimadas</label>
                        <asp:TextBox ID="TextoHorasEstimadas" runat="server"
                                     CssClass="form-control"
                                     TextMode="Number" />
                    </div>
        
           <!-- IMÁGENES -->
                <div class="col-md-6">
                    <label class="form-label">Imágenes</label>
        
                    <div class="d-flex align-items-center gap-3">
                        <asp:FileUpload ID="InputImagenes"
                                        runat="server"
                                        CssClass="form-control"
                                        AllowMultiple="true" />
        
                        <button type="button"
                                class="btn btn-outline-danger btn-sm"
                                onclick="BorrarArchivos()">
                            Borrar
                        </button>
                    </div>
                </div>
        
        <h5 class="mt-4">Imágenes existentes</h5>
        <div class="d-flex gap-1 flex-wrap">
        <asp:Repeater ID="RepeaterImagenesExistentes" runat="server" OnItemCommand="RepeaterImagenesExistentes_ItemCommand">
            <ItemTemplate>
                <div class="d-flex flex-column justify-content-center align-content-start align-items-center gap-1 mb-2">
                    <img src="<%# Eval("Path") %>" 
                         style="width:80px;height:80px;object-fit:cover;border-radius:6px;" />
        
                    <asp:Button ID="BotonEliminarImagen"
                                runat="server"
                                CssClass="btn btn-outline-danger btn-sm ms-3"
                                CommandName="EliminarImagen"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="Eliminar" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
            </div>

        <div class="mt-4 d-flex gap-3">
            <asp:Button ID="BotonGuardar" runat="server"
                        Text="Guardar cambios"
                        CssClass="btn btn-success"
                        OnClick="ClickBotonGuardar" />

            <a href="Listar.aspx" class="btn btn-secondary">Volver</a>
        </div>
    </div>

    <script>
        const selectTipoRelacion = document.getElementById("<%= SelectTipoRelacion.ClientID %>");
        const selectTareaRelacionada = document.getElementById("<%= SelectTareaRelacionada.ClientID %>");

        function ActualizarEstadoRelacion() {
            if (selectTipoRelacion.value === "") {
                selectTareaRelacionada.value = "";
                selectTareaRelacionada.disabled = true;
            } else {
                selectTareaRelacionada.disabled = false;
            }
        }

        selectTipoRelacion.addEventListener("change", ActualizarEstadoRelacion);
        ActualizarEstadoRelacion();

        const inputArchivos = document.getElementById("<%= InputImagenes.ClientID %>");
        function BorrarArchivos() {
            inputArchivos.value = "";
        }
    </script>

</asp:Content>
