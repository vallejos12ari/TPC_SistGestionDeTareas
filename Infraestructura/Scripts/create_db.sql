create table comentarios
(
    id             int identity
        primary key,
    tarea_id       int                        not null,
    usuario_id     int                        not null,
    texto          text                       not null,
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table estados
(
    id             int identity
        primary key,
    nombre         varchar(150)               not null,
    orden          int      default 0         not null,
    color          varchar(50),
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null,
    es_inicial     tinyint  default 0,
    es_final       tinyint  default 0
)
    go

create table estados_flujo
(
    id                int identity
        primary key,
    estado_origen_id  int                        not null,
    estado_destino_id int                        not null,
    fecha_creacion    datetime default getdate() not null
)
    go

create table historial
(
    id             int identity
        primary key,
    tarea_id       int                        not null,
    usuario_id     int                        not null,
    valor_anterior text,
    valor_nuevo    text,
    fecha          datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table horas
(
    id             int identity
        primary key,
    tarea_id       int                        not null,
    usuario_id     int                        not null,
    horas          decimal(5, 2)              not null,
    dia            date                       not null,
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table imagenes
(
    id             int identity
        primary key,
    tarea_id       int                        not null,
    nombre         varchar(255)               not null,
    mime           varchar(100),
    size           int,
    path           varchar(500)               not null,
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table prioridades
(
    id             int identity
        primary key,
    nombre         varchar(150)               not null,
    color          varchar(50),
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table tags
(
    id             int identity
        primary key,
    nombre         varchar(150)               not null,
    color          varchar(50),
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table tareas
(
    id                int identity
        primary key,
    titulo            varchar(255)                    not null,
    descripcion       text,
    usuario_id        int                             not null,
    creado_por        int                             not null,
    hs_estimadas      decimal(5, 2) default 0         not null,
    estado_id         int                             not null,
    prioridad_id      int                             not null,
    tipo_relacion_id  int,
    relacionado_id    int,
    fecha_vencimiento datetime,
    fecha_creacion    datetime      default getdate() not null,
    eliminado         tinyint       default 0         not null
)
    go

create table tareas_tags
(
    id             int identity
        primary key,
    tarea_id       int                        not null,
    tag_id         int                        not null,
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null,
    constraint uq_tarea_tag
        unique (tarea_id, tag_id)
)
    go

create table tipo_relaciones
(
    id             int identity
        primary key,
    nombre         varchar(150)               not null,
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table usuarios
(
    id             int identity
        primary key,
    nombre         varchar(150)               not null,
    email          varchar(255)               not null
        unique,
    password       varchar(255)               not null,
    rol            varchar(20)                not null
        check ([rol] = 'USER' OR [rol] = 'SUPERVISOR' OR [rol] = 'ADMIN'),
    verificado     tinyint  default 0         not null,
    fecha_creacion datetime default getdate() not null,
    eliminado      tinyint  default 0         not null
)
    go

create table usuarios_relacionados
(
    id_supervisor  int                        not null,
    id_usuario     int                        not null,
    fecha_creacion datetime default getdate() not null,
    constraint pk_usuarios_relacionados
        primary key (id_supervisor, id_usuario)
)
    go

