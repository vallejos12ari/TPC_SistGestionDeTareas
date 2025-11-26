<%@ Page Title="Crear Tarea" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="Crear.aspx.cs"
    Inherits="Web.Pages.Tareas.Crear" %>

<asp:Content ID="ContenidoCrear" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="fw-bold mb-4">Crear tarea</h3>

    <asp:Label ID="ErrorCrear" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

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
            <label class="form-label">
                Descripción <span class="text-danger">*</span>
            </label>
            <asp:TextBox ID="TextoDescripcion" runat="server"
                         CssClass="form-control" TextMode="MultiLine" Rows="4" />
        </div>
        
         <div class="col-md-6">
                    <label class="form-label">
                        Tags <span class="text-danger">*</span>
                    </label>
                    <asp:ListBox ID="SelectTags" runat="server"
                                 CssClass="form-select"
                                 SelectionMode="Multiple"
                                 Rows="6">
                    </asp:ListBox>
                </div>


        <div class="col-md-6">
            <label class="form-label">
                Prioridad <span class="text-danger">*</span>
            </label>
            <asp:DropDownList ID="SelectPrioridad" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>

        <!-- TAGS + VENCIMIENTO + HORAS ESTIMADAS -->

        <div class="col-md-6">
            <label class="form-label">
                Vencimiento <span class="text-danger">*</span>
            </label>
            <asp:TextBox ID="TextoVencimiento" runat="server"
                         CssClass="form-control" TextMode="Date" />
        </div>
        
         <div class="col-md-6">
                <label class="form-label">
                    Tipo de relación
                </label>
                <asp:DropDownList ID="SelectTipoRelacion" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
    
            <div class="col-md-6">
                <label class="form-label">
                    Tarea relacionada
                </label>
                <asp:DropDownList ID="SelectTareaRelacionada" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

           <div class="col-md-6">
               <label class="form-label">
                   Horas estimadas
               </label>
               <asp:TextBox ID="TextoHorasEstimadas" runat="server"
                            CssClass="form-control"
                            TextMode="Number" />
               </div>

        <div class="col-md-6">
            <label class="form-label">Imágenes *</label>
        
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

        <!-- BOTONES -->
        <div class="mt-4 d-flex gap-3">
            <asp:Button ID="BotonCrear" runat="server"
                        Text="Crear"
                        CssClass="btn btn-success"
                        OnClick="ClickBotonCrear" />

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

