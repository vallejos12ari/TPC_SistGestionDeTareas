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
    public partial class CrearTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Tag nuevo = new Tag
                {
                    Nombre = txtNombre.Text.Trim(),
                    Color = txtColor.Value
                };

                TagNegocio negocio = new TagNegocio();
                negocio.Agregar(nuevo);

                Response.Redirect("Tags.aspx");
            }
            catch (Exception ex)
            {
                vsErrores.HeaderText = "Error al crear el tag:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tags.aspx");
        }
    }
}