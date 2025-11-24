<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="SistemaDeGestionDeTareas.Login"
    UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        html, body { height: 100%; }
        body {
            display: flex; align-items: center;
            padding-top: 40px; padding-bottom: 40px;
            background-color: #f5f5f5;
        }
        .form-signin { width: 100%; max-width: 330px; padding: 15px; margin: auto; }
    </style>
</head>
<body>
    <main class="form-signin text-center">
        <form id="form1" runat="server" novalidate>
           
            <asp:ScriptManager runat="server" />

            <h1 class="h3 mb-3 fw-normal">Iniciar Sesión</h1>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2" />

            <div class="form-floating mb-3">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="nombre@ejemplo.com" />
                <label for="txtEmail">Correo Electrónico</label>

                <asp:RequiredFieldValidator runat="server" ID="rfvEmail"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio"
                    CssClass="text-danger d-block"
                    Display="Dynamic"
                    EnableClientScript="true"
                    ValidationGroup="Login" />

                <asp:RegularExpressionValidator runat="server" ID="revEmail"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Email inválido"
                    CssClass="text-danger d-block"
                    Display="Dynamic"
                    EnableClientScript="true"
                    ValidationGroup="Login" />
            </div>

            <div class="form-floating mb-3">
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Contraseña" />
                <label for="txtPassword">Contraseña</label>

                <asp:RequiredFieldValidator runat="server" ID="rfvPassword"
                    ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es obligatoria"
                    CssClass="text-danger d-block"
                    Display="Dynamic"
                    EnableClientScript="true"
                    ValidationGroup="Login" />
            </div>

            <div class="checkout mb-3 text-start">
                <label><asp:CheckBox runat="server" ID="chkRecordarme" /> Recordarme</label>
            </div>

            <asp:Button runat="server" ID="btnLogin" CssClass="w-100 btn btn-lg btn-primary"
                Text="Ingresar" OnClick="btnLogin_Click"
                ValidationGroup="Login" />
        </form>
    </main>
</body>
</html>
