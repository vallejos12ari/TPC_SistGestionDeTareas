using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Web.Pages.Estados
{
    public partial class Flujo : System.Web.UI.Page
    {
        EstadoNegocio estadoNegocio = new EstadoNegocio();
        EstadoFlujoNegocio flujoNegocio = new EstadoFlujoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarFlujos();
            }
        }

        private void CargarCombos()
        {
            List<Estado> estados = estadoNegocio.Listar();

            SelectOrigen.Items.Clear();
            SelectDestino.Items.Clear();
            SelectInicial.Items.Clear();

            foreach (Estado e in estados)
            {
                SelectOrigen.Items.Add(new ListItem(e.Nombre, e.Id.ToString()));
                SelectDestino.Items.Add(new ListItem(e.Nombre, e.Id.ToString()));
                SelectInicial.Items.Add(new ListItem(e.Nombre, e.Id.ToString()));
            }
        }
        
        protected void SelectInicial_SelectedIndexChanged(object sender, EventArgs e)
        {
            AsignarFlujo();
        }
        private void AsignarFlujo()
        {
            int estadoInicialId = int.Parse(SelectInicial.SelectedValue);

            estadoNegocio.AsignarInicial(estadoInicialId);

            CargarFlujos(); 
        }

        

        private void CargarFlujos()
        {
            List<EstadoFlujo> lista = flujoNegocio.Listar();
            TablaFlujo.DataSource = lista;
            TablaFlujo.DataBind();
        }

        protected void ClickBotonAgregarFlujo(object sender, EventArgs e)
        {
            int idOrigen = int.Parse(SelectOrigen.SelectedValue);
            int idDestino = int.Parse(SelectDestino.SelectedValue);

            if (idOrigen == idDestino)
            {
                ErrorFlujo.Text = "El estado origen y destino no pueden ser iguales.";
                return;
            }

            if (flujoNegocio.ExisteFlujo(idOrigen, idDestino))
            {
                ErrorFlujo.Text = "Este flujo ya existe.";
                return;
            }

            flujoNegocio.Agregar(idOrigen, idDestino);

            CargarFlujos();
        }


        protected void ClickBotonEliminar(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            int id = int.Parse(boton.CommandArgument);

            flujoNegocio.Eliminar(id);

            CargarFlujos();
        }
    }
}