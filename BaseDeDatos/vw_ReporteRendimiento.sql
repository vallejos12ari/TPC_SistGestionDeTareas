USE GESTION_TAREAS_DB;
GO

CREATE VIEW vw_ReporteRendimiento AS
SELECT 
    U.IdUsuario,
    U.NombreUsuario,

    (SELECT COUNT(*) 
     FROM TAREA T 
     WHERE T.IdUsuarioCreador = U.IdUsuario) AS TareasCreadas,

    (SELECT COUNT(*) 
     FROM TAREA T 
     WHERE T.IdUsuarioAsignado = U.IdUsuario) AS TareasAsignadas,

    (SELECT COUNT(*) 
     FROM TAREA T 
     WHERE T.IdUsuarioAsignado = U.IdUsuario 
       AND T.IdEstado = 3) AS TareasCompletadas,

    (SELECT COUNT(*) 
     FROM TAREA T 
     WHERE T.IdUsuarioAsignado = U.IdUsuario 
       AND T.IdEstado <> 3) AS TareasPendientes,

    (SELECT COUNT(*) 
     FROM TAREA T 
     WHERE T.IdUsuarioAsignado = U.IdUsuario
       AND T.FechaVencimiento < GETDATE()
       AND T.IdEstado <> 3) AS TareasVencidas

FROM USUARIO U;
GO

