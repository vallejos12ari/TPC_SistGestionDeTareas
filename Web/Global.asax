<%@ Application Language="C#" Inherits="Web.Global" CodeBehind="Global.asax.cs" %>

<script runat="server">

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Server.ClearError();
        Response.Redirect("~/Pages/Error.aspx");
    }

</script>