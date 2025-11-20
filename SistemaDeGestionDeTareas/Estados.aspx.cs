using Dominio;
using Negocio;
using SistemaDeGestionDeTareas;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class Estados : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario actual es administrador
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            if (usuario == null || usuario.Rol != "Admin")
            {
                Response.Redirect("Login.aspx"); // Redirigir si no es admin
            }

            if (!IsPostBack)
            {
                CargarEstados();
            }
        }

        private void CargarEstados()
        {
            EstadoNegocio negocio = new EstadoNegocio();
            gvEstados.DataSource = negocio.Listar();
            gvEstados.DataBind();
        }

        
             protected void gvEstados_RowCommand(object sender, GridViewCommandEventArgs e)
             {
                 int idEstado = Convert.ToInt32(e.CommandArgument);
                 EstadoNegocio negocio = new EstadoNegocio();
    
                 if (e.CommandName == "Editar")
                 {
                     Estado estado = negocio.Listar().Find(x => x.IdEstado == idEstado);
                     if (estado != null)
                     {
                         hfIdEstado.Value = estado.IdEstado.ToString();
                         txtNombreEstado.Text = estado.NombreEstado;
                         litModalTitle.Text = "Editar Estado";
                         ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openEstadoModal();", true);
                     }
                 }
                 else if (e.CommandName == "Eliminar")
                 {
                     try
                     {
                         negocio.Eliminar(idEstado);
                         CargarEstados();
                     }
                 catch (Exception ex)
                     {
                         // Manejar error, por ejemplo, mostrar un mensaje al usuario                                              
                         // lblError.Text = ex.Message;                                                                            
                         // lblError.Visible = true;                                                                               
                         ScriptManager.RegisterStartupScript(this, GetType(), "ShowError", $"alert('Error al eliminar el estado:{ex.Message.Replace("'", "\\'")}')", true);                                                                                   
                     }                                                                                                             
                 }                                                                                                                 
             }
        protected void btnNuevoEstado_Click(object sender, EventArgs e)
             {
                 hfIdEstado.Value = "0"; // Indicar que es un nuevo estado
                 txtNombreEstado.Text = string.Empty;
                 litModalTitle.Text = "Nuevo Estado";
                 ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openEstadoModal();", true);
             }
    
             protected void btnGuardarEstado_Click(object sender, EventArgs e)
             {
                 if (!IsValid) return;
    
                 EstadoNegocio negocio = new EstadoNegocio();
                 Estado estado = new Estado
                 {
                     NombreEstado = txtNombreEstado.Text.Trim()
                 };
    
                 if (int.TryParse(hfIdEstado.Value, out int id) && id > 0)
                 {
                     // Modificar
                     estado.IdEstado = id;
                     try
                     {
                         negocio.Modificar(estado);
                     }
                 catch (Exception ex)
                     {
                         ScriptManager.RegisterStartupScript(this, GetType(), "ShowError", $"alert('Error al modificar el estado: {ex.Message.Replace("'", "\\'")}')", true);
                     return;
                 }
                 }
                 else
                {
                     // Agregar
                    try
                    {                                                                                                                                                                                                                    
                        negocio.Agregar(estado);                                                                                                                                                                                         
                    }                                                                                                                                                                                                                    
                    catch (Exception ex)                                                                                                                                                                                                 
                    {                                                                                                                                                                                                                    
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowError", $"alert('Error al agregar el estado: {ex.Message.Replace("'", "\\'")}')", true);                                                               
                        return;                                                                                                                                                                                                          
                    }                                                                                                                                                                                                                    
                }                                                                                                                                                                                                                        
   
                 CargarEstados();
                 ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "var myModal = bootstrap.Modal.getInstance(document.getElementById('estadoModal')); if(myModal) myModal.hide();", true);
             }

            protected void btnGestionarOrden_Click(object sender, EventArgs e)
            {
                 Response.Redirect("OrdenEstados.aspx");
            }
     }
    }
