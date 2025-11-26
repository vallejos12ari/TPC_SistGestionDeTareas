
-- usuarios
INSERT INTO usuarios (nombre, email, password, rol, eliminado, verificado)
VALUES
    ('Admin General', 'admin@demo.com', 'admin123', 'ADMIN', 1, 1),
    ('Supervisor Demo', 'supervisor@demo.com', 'super123', 'SUPERVISOR', 1, 1),
    ('Usuario Demo', 'usuario@demo.com', 'user123', 'USER', 1, 1);


-- estados
INSERT INTO estados (nombre, orden, eliminado, color)
VALUES
    ('Pendiente',    1, 1, '#FF9800'),
    ('En Progreso',  2, 1, '#2196F3'),
    ('En Revisión',  3, 1, '#9C27B0'),
    ('Completada',   4, 1, '#4CAF50');


-- prioridades
INSERT INTO prioridades (nombre, eliminado, color)
VALUES
    ('Baja',  1, '#8BC34A'),
    ('Media', 1, '#FFC107'),
    ('Alta',  1, '#F44336');


-- tags
INSERT INTO tags (nombre, eliminado, color)
VALUES
    ('Backend',  1, '#3F51B5'),
    ('Frontend', 1, '#009688'),
    ('Bug',      1, '#F44336'),
    ('Mejora',   1, '#4CAF50');


-- tipo_relaciones
INSERT INTO tipo_relaciones (nombre, eliminado)
VALUES
    ('Relacionado', 1),
    ('Bloquea',     1),
    ('Depende de',  1),
    ('Duplicado de',1);