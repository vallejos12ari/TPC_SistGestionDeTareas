CREATE DATABASE gestion_de_tareas;
GO

USE gestion_de_tareas;
GO

-- usuarios
CREATE TABLE usuarios (
                          id INT IDENTITY(1,1) PRIMARY KEY,
                          nombre VARCHAR(150) NOT NULL,
                          email VARCHAR(255) NOT NULL UNIQUE,
                          password VARCHAR(255) NOT NULL,
                          rol VARCHAR(20) NOT NULL
                              CHECK (rol IN ('ADMIN', 'SUPERVISOR', 'USER')),
                          eliminado TINYINT NOT NULL DEFAULT 1,
                          verificado TINYINT NOT NULL DEFAULT 0,
                          fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO


-- tags
CREATE TABLE tags (
                      id INT IDENTITY(1,1) PRIMARY KEY,
                      nombre VARCHAR(150) NOT NULL,
                      eliminado TINYINT NOT NULL DEFAULT 1,
                      color VARCHAR(50),
                      fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO


-- prioridades
CREATE TABLE prioridades (
                             id INT IDENTITY(1,1) PRIMARY KEY,
                             nombre VARCHAR(150) NOT NULL,
                             eliminado TINYINT NOT NULL DEFAULT 1,
                             color VARCHAR(50),
                             fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO


-- estados
CREATE TABLE estados (
                         id INT IDENTITY(1,1) PRIMARY KEY,
                         nombre VARCHAR(150) NOT NULL,
                         orden INT NOT NULL DEFAULT 0,
                         eliminado TINYINT NOT NULL DEFAULT 1,
                         color VARCHAR(50),
                         fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO


-- tipo_relaciones
CREATE TABLE tipo_relaciones (
                                 id INT IDENTITY(1,1) PRIMARY KEY,
                                 nombre VARCHAR(150) NOT NULL,
                                 eliminado TINYINT NOT NULL DEFAULT 1,
                                 fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO


-- tareas
CREATE TABLE tareas (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        titulo VARCHAR(255) NOT NULL,
                        descripcion TEXT NULL,
                        usuario_id INT NOT NULL,
                        creado_por INT NOT NULL,
                        hs_estimadas DECIMAL(5,2) NOT NULL DEFAULT 0,
                        estado_id INT NOT NULL,
                        prioridad_id INT NOT NULL,
                        tipo_relacion_id INT NULL,
                        relacionado_id INT NULL,
                        fecha_vencimiento DATETIME NULL,
                        fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),

                        CONSTRAINT fk_tareas_usuario FOREIGN KEY (usuario_id)
                            REFERENCES usuarios(id),

                        CONSTRAINT fk_tareas_creado_por FOREIGN KEY (creado_por)
                            REFERENCES usuarios(id),

                        CONSTRAINT fk_tareas_estado FOREIGN KEY (estado_id)
                            REFERENCES estados(id),

                        CONSTRAINT fk_tareas_prioridad FOREIGN KEY (prioridad_id)
                            REFERENCES prioridades(id),

                        CONSTRAINT fk_tareas_tipo_relacion FOREIGN KEY (tipo_relacion_id)
                            REFERENCES tipo_relaciones(id),

                        CONSTRAINT fk_tareas_relacionado FOREIGN KEY (relacionado_id)
                            REFERENCES tareas(id)
);
GO


-- tareas_tags
CREATE TABLE tareas_tags (
                             id INT IDENTITY(1,1) PRIMARY KEY,
                             tarea_id INT NOT NULL,
                             tag_id INT NOT NULL,
                             fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),

                             CONSTRAINT fk_tareas_tags_tarea FOREIGN KEY (tarea_id)
                                 REFERENCES tareas(id)
                                 ON DELETE CASCADE,

                             CONSTRAINT fk_tareas_tags_tag FOREIGN KEY (tag_id)
                                 REFERENCES tags(id)
                                 ON DELETE CASCADE,

                             CONSTRAINT uq_tarea_tag UNIQUE (tarea_id, tag_id)
);
GO


-- imagenes
CREATE TABLE imagenes (
                          id INT IDENTITY(1,1) PRIMARY KEY,
                          tarea_id INT NOT NULL,
                          nombre VARCHAR(255) NOT NULL,
                          mime VARCHAR(100),
                          size INT,
                          path VARCHAR(500) NOT NULL,
                          fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),

                          CONSTRAINT fk_imagenes_tarea FOREIGN KEY (tarea_id)
                              REFERENCES tareas(id)
                              ON DELETE CASCADE
);
GO


-- horas
CREATE TABLE horas (
                       id INT IDENTITY(1,1) PRIMARY KEY,
                       tarea_id INT NOT NULL,
                       usuario_id INT NOT NULL,
                       horas DECIMAL(5,2) NOT NULL,
                       dia DATE NOT NULL,
                       eliminado TINYINT NOT NULL DEFAULT 1,
                       fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),

                       CONSTRAINT fk_horas_tarea FOREIGN KEY (tarea_id)
                           REFERENCES tareas(id)
                           ON DELETE CASCADE,

                       CONSTRAINT fk_horas_usuario FOREIGN KEY (usuario_id)
                           REFERENCES usuarios(id)
                           ON DELETE CASCADE
);
GO


-- comentarios
CREATE TABLE comentarios (
                             id INT IDENTITY(1,1) PRIMARY KEY,
                             tarea_id INT NOT NULL,
                             usuario_id INT NOT NULL,
                             texto TEXT NOT NULL,
                             eliminado TINYINT NOT NULL DEFAULT 1,
                             fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),

                             CONSTRAINT fk_comentarios_tarea FOREIGN KEY (tarea_id)
                                 REFERENCES tareas(id)
                                 ON DELETE CASCADE,

                             CONSTRAINT fk_comentarios_usuario FOREIGN KEY (usuario_id)
                                 REFERENCES usuarios(id)
                                 ON DELETE CASCADE
);
GO


-- historial
CREATE TABLE historial (
                           id INT IDENTITY(1,1) PRIMARY KEY,
                           tarea_id INT NOT NULL,
                           usuario_id INT NOT NULL,
                           valor_anterior TEXT NULL,
                           valor_nuevo TEXT NULL,
                           fecha DATETIME NOT NULL DEFAULT GETDATE(),

                           CONSTRAINT fk_historial_tarea FOREIGN KEY (tarea_id)
                               REFERENCES tareas(id)
                               ON DELETE CASCADE,

                           CONSTRAINT fk_historial_usuario FOREIGN KEY (usuario_id)
                               REFERENCES usuarios(id)
                               ON DELETE CASCADE
);
GO
