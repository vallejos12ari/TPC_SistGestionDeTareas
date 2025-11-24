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
    public partial class EditarTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];

                if (!int.TryParse(idStr, out int id) || id <= 0)
                {
                    Response.Redirect("Tags.aspx");
                    return;
                }

                CargarTag(id);
            }
        }

        private void CargarTag(int id)
        {
            TagNegocio negocio = new TagNegocio();
            Tag tag = negocio.ObtenerPorId(id);

            if (tag == null)
            {
                Response.Redirect("Tags.aspx");
                return;
            }

            hfIdTag.Value = tag.IdTag.ToString();
            txtNombre.Text = tag.Nombre;
            txtColor.Value = tag.Color;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(hfIdTag.Value, out int id) || id <= 0)
                {
                    Response.Redirect("Tags.aspx");
                    return;
                }

                Tag actualizado = new Tag
                {
                    IdTag = id,
                    Nombre = txtNombre.Text.Trim(),
                    Color = txtColor.Value
                };

                TagNegocio negocio = new TagNegocio();
                negocio.Modificar(actualizado);

                Response.Redirect("Tags.aspx");
            }
            catch (Exception ex)
            {
                vsErrores.HeaderText = "Error al actualizar el tag:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tags.aspx");
        }
    }
}