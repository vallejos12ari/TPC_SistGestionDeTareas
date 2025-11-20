using Dominio;
using Negocio;
using SistemaDeGestionDeTareas;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class OrdenEstados : Page
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
                CargarDropdowns();
                CargarOrdenEstados();
            }
        }

        private void CargarDropdowns()
        {
            EstadoNegocio negocio = new EstadoNegocio();
            List<Estado> estados = negocio.Listar();

            ddlEstadoActual.DataSource = estados;
            ddlEstadoActual.DataTextField = "NombreEstado";
            ddlEstadoActual.DataValueField = "IdEstado";
            ddlEstadoActual.DataBind();
            ddlEstadoActual.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            ddlEstadoDestino.DataSource = estados;
            ddlEstadoDestino.DataTextField = "NombreEstado";
            ddlEstadoDestino.DataValueField = "IdEstado";
            ddlEstadoDestino.DataBind();
            ddlEstadoDestino.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        private void CargarOrdenEstados()
        {
            EstadoNegocio negocio = new EstadoNegocio();
            gvOrdenEstados.DataSource = negocio.ListarOrdenEstados();
            gvOrdenEstados.DataBind();
        }

        protected void btnAgregarTransicion_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            int idEstadoActual = int.Parse(ddlEstadoActual.SelectedValue);
            int idEstadoDestino = int.Parse(ddlEstadoDestino.SelectedValue);

            if (idEstadoActual == idEstadoDestino)
            {
                vsOrdenEstado.HeaderText = "Error: El estado actual y el estado destino no pueden ser el mismo.";
                vsOrdenEstado.Visible = true;
                return;
            }

            EstadoNegocio negocio = new EstadoNegocio();
            OrdenEstado nuevaOrden = new OrdenEstado
            {
                EstadoActual = new Estado { IdEstado = idEstadoActual },
                EstadoDestino = new Estado { IdEstado = idEstadoDestino }
            };

            try
            {
                negocio.AgregarOrdenEstado(nuevaOrden);
                CargarOrdenEstados();
                ddlEstadoActual.SelectedIndex = 0;
                ddlEstadoDestino.SelectedIndex = 0;
                vsOrdenEstado.Visible = false; // Ocultar validación si todo fue bien
            }
            catch (Exception ex)
            {
                vsOrdenEstado.HeaderText = "Error al agregar la transición:";
                vsOrdenEstado.Controls.Add(new LiteralControl(ex.Message));
                vsOrdenEstado.Visible = true;
            }
        }

        protected void gvOrdenEstados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idOrden = Convert.ToInt32(e.CommandArgument);
                EstadoNegocio negocio = new EstadoNegocio();
                try
                {
                    negocio.EliminarOrdenEstado(idOrden);
                    CargarOrdenEstados();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowError", $"alert('Error al eliminar la transición:{ex.Message.Replace("'", "\\'")}')", true);
                }
            }
        }

        protected void btnVolverEstados_Click(object sender, EventArgs e)
        {
            Response.Redirect("Estados.aspx");
        }
    }
}