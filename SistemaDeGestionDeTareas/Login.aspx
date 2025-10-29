<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SistemaDeGestionDeTareas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Iniciar Sesion</title>
    
<link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>

        html, body {
            height: 100%;
        }
        body {
            display: flex;
            align-items: center;
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }
        .form-signin {
            width: 100%;
            max-width: 330px;
            padding: 15px;
            margin: auto;
        }
    </style>



</head>
<body>
    <main class="form-signin text-center">

        <form id="form1" runat="server">
            
    
            <h1 class="h3 mb-3 fw-normal"> Iniciar Sesion </h1>

            <div class="form-floating mb-3">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="nombre@ejemplo.com"/>
                <label for="txtEmail"> Correo Electronico </label>
                 </div>

                 <div class="form-floating mb-3">
                     <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Contraseña"/>
                     <label for="txtPassword"> Contraseña </label>
                     </div>
                     <div class="checkout mb-3">
                         <label>        
                            <asp:CheckBox runat="server" ID="chkRecordarme" /> Recordarme </label>

                         </div>


                         
                         
              <asp:Button runat="server" ID="btnLogin" CssClass="w-100 btn btn-lg btn-primary" Text="Ingresar" />

            <p class="mt-4 m-1 text-muted">
                <a href="#">¿Olvidaste tu contraseña? </a>
            </p>
            <p class="m-3 text-muted">
                &copy; <%:DateTime.Now.Year %>
            </p>
          </form>
        </main>
</body>
</html>
