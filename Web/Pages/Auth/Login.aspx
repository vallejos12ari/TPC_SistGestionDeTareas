<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Pages.Auth.Login" %>

<!DOCTYPE html>
<html lang="es">

<head runat="server">
    <title>Login - Gestión de Tareas</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">

    
    <style>
        body {
            background-color: #f0f2f5;
            font-family: 'Poppins', sans-serif;
        }

        .login-wrapper {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 15px;
        }

        .login-card {
            width: 100%;
            max-width: 420px;
            background: #ffffff;
            border-radius: 12px;
            padding: 35px 30px;
            box-shadow: 0 4px 18px rgba(0,0,0,0.1);
        }

        .login-title {
            font-weight: 600;
            font-size: 24px;
            text-align: center;
            margin-bottom: 25px;
            color: #333;
        }
    </style>
    
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    
        <script>
            function AbrirModalPassword() {
                new bootstrap.Modal(document.getElementById('ModalNuevaPassword')).show();
            }
        </script>
</head>

<body>

    <form id="form1" runat="server">
        <div class="login-wrapper">

            <div class="login-card">

                <h3 class="login-title">Gestión de Tareas</h3>

                <asp:Label ID="lblError" runat="server"
                    CssClass="alert alert-danger d-none"></asp:Label>

                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control"
                        placeholder="nombre@correo.com" />
                </div>

                <div class="mb-4">
                    <label class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server"
                        CssClass="form-control"
                        TextMode="Password"
                        placeholder="••••••••" />
                </div>

                <asp:Button ID="btnLogin" runat="server"
                    CssClass="btn btn-primary w-100 py-2"
                    Text="Ingresar"
                    OnClick="BotonLoginClick" />

            </div>
            
            <div class="modal fade" id="ModalNuevaPassword" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
            
                        <div class="modal-header">
                            <h5 class="modal-title">Crear nueva contraseña</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
            
                        <div class="modal-body">
            
                            <asp:Label ID="lblErrorPassword" runat="server"
                                       CssClass="alert alert-danger w-100 d-none"></asp:Label>
            
                            <label class="form-label">Nueva contraseña</label>
                            <asp:TextBox ID="txtNuevaPassword"
                                         runat="server"
                                         TextMode="Password"
                                         CssClass="form-control mb-3" />
            
                            <label class="form-label">Repetir contraseña</label>
                            <asp:TextBox ID="txtRepetirPassword"
                                         runat="server"
                                         TextMode="Password"
                                         CssClass="form-control" />
            
                        </div>
            
                        <div class="modal-footer">
                            <asp:Button ID="btnGuardarPassword"
                                        runat="server"
                                        CssClass="btn btn-primary"
                                        Text="Guardar"
                                        OnClick="BotonGuardarPassword" />
                        </div>
            
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>

</html>