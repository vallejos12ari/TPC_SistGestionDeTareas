--Usuarios de prueba
INSERT INTO USUARIO (NombreUsuario, Email, ContraseniaHash, Rol, Activo)
VALUES 
('Admin1', 'admi1@test.com', '1234', 'Admin', 1),
('Admin2', 'admin2@test.com', '1234', 'Usuario', 1),
('Admin3', 'admin3@test.com', '1234', 'Usuario', 1);
GO

--Asignacion de Tareas creadas por Admin 1 y delegadas a Admin2
INSERT INTO TAREA
(IdEstado, IdPrioridad, IdUsuarioCreador, IdUsuarioAsignado, Titulo, Descripcion, Activo, FechaCreacion, FechaVencimiento)
VALUES
(1, 3, 1, 2, 'Diseñar mockups', 'Pantallas del sistema', 1, GETDATE()-10, GETDATE()+2), -- pendiente
(2, 2, 1, 2, 'Corregir bugs', 'Errores menores', 1, GETDATE()-5, GETDATE()+1),     -- en progreso
(3, 1, 1, 2, 'Preparar reporte', 'Reporte final', 1, GETDATE()-8, GETDATE()-1);   -- completada
GO

--Asignacion de Tareas x Adm 2
INSERT INTO TAREA
(IdEstado, IdPrioridad, IdUsuarioCreador, IdUsuarioAsignado, Titulo, Activo, FechaCreacion, FechaVencimiento)
VALUES
(3, 2, 2, 2, 'Actualizar documentación', 1, GETDATE()-15, GETDATE()-5),
(1, 1, 2, 2, 'Revisar pruebas', 1, GETDATE()-1, GETDATE()+10);
GO

--Tareas sin asignar a Admin
INSERT INTO TAREA
(IdEstado, IdPrioridad, IdUsuarioCreador, Titulo, Activo, FechaCreacion)
VALUES
(1, 1, 3, 'Analizar requisitos', 1, GETDATE()-20),
(3, 3, 3, 'Deploy a producción', 1, GETDATE()-7);
GO

--Tareas vencidad
INSERT INTO TAREA
(IdEstado, IdPrioridad, IdUsuarioCreador, IdUsuarioAsignado, Titulo, Activo, FechaCreacion, FechaVencimiento)
VALUES
(1, 2, 1, 2, 'Entrega atrasada', 1, GETDATE()-12, GETDATE()-3);  -- vencida
GO
