using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class EditarPrioridad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idStr = Request.QueryString["id"];
                if (!int.TryParse(idStr, out int id) || id <= 0)
                {
                    Response.Redirect("Prioridades.aspx");
                    return;
                }

                CargarPrioridad(id);
            }

        }

        private void CargarPrioridad(int id)
        {
            PrioridadNegocio negocio = new PrioridadNegocio();
            var prioridad = negocio.Listar().Find(p => p.IdPrioridad == id);

            if (prioridad == null)
            {
                Response.Redirect("Prioridades.aspx");
                return;
            }

            // Guardamos el ID oculto
            hfIdPrioridad.Value = prioridad.IdPrioridad.ToString();

            // Cargamos los datos
            txtNombre.Text = prioridad.NombrePrioridad;
            txtNivel.Text = prioridad.Nivel.ToString();
            txtColor.Value = prioridad.Color;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                var prioridad = new Prioridad
                {
                    IdPrioridad = int.Parse(hfIdPrioridad.Value),
                    NombrePrioridad = txtNombre.Text.Trim(),
                    Nivel = int.Parse(txtNivel.Text.Trim()),
                    Color = txtColor.Value
                };

                PrioridadNegocio negocio = new PrioridadNegocio();
                negocio.Modificar(prioridad);

                Response.Redirect("Prioridades.aspx");
            }
            catch (Exception ex)
            {
                vsErrores.HeaderText = "Error al actualizar la prioridad:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Prioridades.aspx");
        }
    }
}