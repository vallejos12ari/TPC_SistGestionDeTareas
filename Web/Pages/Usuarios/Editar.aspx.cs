using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Web.Pages.Usuarios
{
    public partial class Editar : System.Web.UI.Page
    {
        protected int IdUsuario;
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private UsuariosRelacionadosNegocio relacionesNegocio = new UsuariosRelacionadosNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Listar.aspx");
                return;
            }

            IdUsuario = int.Parse(Request.QueryString["id"]);
            IdUsuarioHidden.Value = IdUsuario.ToString();

            if (!IsPostBack)
            {
                CargarRoles();
                Usuario usuario = usuarioNegocio.BuscarPorId(IdUsuario);
                CargarUsuario(usuario);
                MostrarPanelAsignados();
            }
        }

        private void CargarRoles()
        {
            SelectRol.Items.Clear();
            SelectRol.Items.Add("ADMIN");
            SelectRol.Items.Add("SUPERVISOR");
            SelectRol.Items.Add("USER");
        }

        private void CargarUsuario(Usuario usuario)
        {
            TextoNombre.Text = usuario.Nombre;
            TextoEmail.Text = usuario.Email;
            SelectRol.SelectedValue = usuario.Rol;
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            Usuario usuario = usuarioNegocio.BuscarPorId(IdUsuario);

            usuario.Nombre = TextoNombre.Text;
            usuario.Email = TextoEmail.Text;
            usuario.Rol = SelectRol.SelectedValue;

            usuarioNegocio.Modificar(usuario);

            Response.Redirect("Listar.aspx");
        }

        protected void ClickBotonConfirmarReinicio(object sender, EventArgs e)
        {
            int id = int.Parse(IdUsuarioHidden.Value);
            Usuario usuario = usuarioNegocio.BuscarPorId(id);

            usuario.Password = "gestiondeturnos";

            usuarioNegocio.Modificar(usuario);

            Response.Redirect("Listar.aspx");
        }
        
        private string RolUsuario()
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            return usuario?.Rol ?? "";
        }
        
        private int IdUsuarioActual()
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            return usuario.Id;
        }

        private void MostrarPanelAsignados()
        {
            if (RolUsuario() != "ADMIN") {
                PanelAsignados.Visible = false;
                return;
            }

            PanelAsignados.Visible = true;

            CargarUsuariosParaAsignar(IdUsuarioActual());
            List<Usuario> usuariosAsignados = relacionesNegocio.ListarAsignados(IdUsuarioActual());
            CargarUsuariosAsignados(usuariosAsignados);
        }

        private void CargarUsuariosParaAsignar(int idSupervisor)
        {
            List<Usuario> lista = usuarioNegocio.Listar();
            List<Usuario> asignados = relacionesNegocio.ListarAsignados(idSupervisor);

            SelectUsuarioAsignar.Items.Clear();

            List<Usuario> disponibles = new List<Usuario>();

            foreach (Usuario u in lista)
            {
                bool esSupervisor = u.Id == idSupervisor;
                bool yaAsignado = asignados.Exists(a => a.Id == u.Id);

                if (!esSupervisor && !yaAsignado)
                {
                    disponibles.Add(u);
                }
            }

            if (disponibles.Count == 0)
            {
                SelectUsuarioAsignar.Items.Add(new ListItem("No hay usuarios disponibles", "0"));
                SelectUsuarioAsignar.Enabled = false;
                BotonAgregarAsignado.Enabled = false;
                return;
            }

            SelectUsuarioAsignar.Enabled = true;
            BotonAgregarAsignado.Enabled = true;

            foreach (Usuario u in disponibles)
            {
                SelectUsuarioAsignar.Items.Add(new ListItem(u.Nombre, u.Id.ToString()));
            }
        }

        private void CargarUsuariosAsignados(List<Usuario> usuarios)
        {
            bool tieneUsuarios = usuarios != null && usuarios.Count > 0;

            PanelSinAsignados.Visible = !tieneUsuarios;
            TablaAsignados.Visible = tieneUsuarios;

            if (tieneUsuarios)
            {
                TablaAsignados.DataSource = usuarios;
                TablaAsignados.DataBind();
            }
        }

        protected void ClickBotonAgregarAsignado(object sender, EventArgs e)
        {
            int idSupervisor = int.Parse(IdUsuarioHidden.Value);
            int idAsignar = int.Parse(SelectUsuarioAsignar.SelectedValue);

            relacionesNegocio.Asignar(idSupervisor, idAsignar);

            List<Usuario> usuarios = relacionesNegocio.ListarAsignados(idSupervisor);
            CargarUsuariosAsignados(usuarios);
            CargarUsuariosParaAsignar(idSupervisor);
        }


        protected void ClickBotonDesasignar(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            int idDesasignar = int.Parse(boton.CommandArgument);
            int idSupervisor = int.Parse(IdUsuarioHidden.Value);

            relacionesNegocio.Desasignar(idSupervisor, idDesasignar);

            List<Usuario> usuarios = relacionesNegocio.ListarAsignados(idSupervisor);
            CargarUsuariosAsignados(usuarios);
            CargarUsuariosParaAsignar(idSupervisor);
        }
    }
}