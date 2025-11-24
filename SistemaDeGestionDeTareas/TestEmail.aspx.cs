using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class TestEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProbar_Click(object sender, EventArgs e)
        {

            try
            {
                string destino = txtDestino.Text.Trim();
                string titulo = txtTitulo.Text.Trim();
                string asignador = txtAsignador.Text.Trim();

                TareaNegocio negocio = new TareaNegocio();
                negocio.NotificarAsignacion(destino, titulo, asignador);  

                lblResultado.Text = "Correo enviado correctamente ✔️";
                lblResultado.CssClass = "text-success fw-bold";
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error: " + ex.Message + "<br><br>StackTrace: " + ex.StackTrace;
                if (ex.InnerException != null)
                    lblResultado.Text += "<br><br>Inner: " + ex.InnerException.Message;

                lblResultado.CssClass = "text-danger fw-bold";
            }

        }
    }
}