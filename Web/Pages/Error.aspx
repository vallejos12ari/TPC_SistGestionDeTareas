<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Error</title>

    <link href="/Content/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f8f9fa;
        }

        .card-error {
            max-width: 500px;
            margin: 80px auto;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.05);
            background: white;
        }

        .card-error h3 {
            font-weight: 700;
        }

        .card-error p {
            font-size: 1.1rem;
        }
    </style>
</head>

<body>

    <div class="card-error text-center">

        <h3 class="text-danger mb-3">Ups... ocurrió un problema</h3>

        <p class="text-muted">
            Algo no salió como esperábamos.<br />
            Por favor intentá nuevamente en unos instantes.
        </p>

        <a href="/Default.aspx" class="btn btn-primary mt-3">
            Volver al inicio
        </a>

    </div>

</body>
</html>
