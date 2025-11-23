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
    public partial class CrearPrioridad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Prioridad nueva = new Prioridad
                {
                    NombrePrioridad = txtNombre.Text.Trim(),
                    Nivel = int.Parse(txtNivel.Text.Trim()),
                    Color = txtColor.Value   
                };

                PrioridadNegocio negocio = new PrioridadNegocio();
                negocio.Agregar(nueva);

                Response.Redirect("Prioridades.aspx");
            }
            catch (Exception ex)
            {
                vsErrores.HeaderText = "Error al crear prioridad:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Prioridades.aspx");
        }
    }
}