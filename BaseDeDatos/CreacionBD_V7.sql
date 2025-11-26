--las siguientes tablas fueron creadas o alteradas
--para crear em abm necesario para el administrador





-- =====================================
-- ALTER TAREA: Nuevos campos requeridos para ABM de tareas
-- =====================================
ALTER TABLE TAREA
ADD FechaVencimiento DATETIME NULL,
    HorasEstimadas INT NULL,
    TieneChecklist BIT NOT NULL DEFAULT 0;
GO




-- =====================================
-- Tabla TAREA_IMAGEN (si no existe)
-- =====================================
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TAREA_IMAGEN'
)
BEGIN
    CREATE TABLE TAREA_IMAGEN (
        IdImagen INT IDENTITY(1,1) PRIMARY KEY,
        IdTarea INT NOT NULL,
        Ruta VARCHAR(300) NOT NULL,
        FechaSubida DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_TareaImagen_Tarea FOREIGN KEY (IdTarea)
            REFERENCES TAREA(IdTarea)
            ON DELETE CASCADE
    );
END
GO



-- =====================================
-- Tabla TAREA_RELACIONADA (si no existe)
-- =====================================
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TAREA_RELACIONADA'
)
BEGIN
    CREATE TABLE TAREA_RELACIONADA (
        IdTarea INT NOT NULL,
        IdTareaRelacionada INT NOT NULL,

        CONSTRAINT PK_TareaRelacionada PRIMARY KEY (IdTarea, IdTareaRelacionada),

        CONSTRAINT FK_TareaRelacionada_Tarea1 FOREIGN KEY (IdTarea)
            REFERENCES TAREA(IdTarea)
            ON DELETE CASCADE,

        CONSTRAINT FK_TareaRelacionada_Tarea2 FOREIGN KEY (IdTareaRelacionada)
            REFERENCES TAREA(IdTarea)
            ON DELETE CASCADE
    );
END
GO





