CREATE DATABASE IF NOT EXISTS eagletechtest CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE eagletechtest;

CREATE TABLE IF NOT EXISTS Usuarios (
    Matricula INT AUTO_INCREMENT PRIMARY KEY,
    NomeCompleto VARCHAR(40) NOT NULL,
    Senha VARCHAR(255) NOT NULL,
    Telefone VARCHAR(11) NOT NULL, 
    Funcao INT NOT NULL,
    Theme INT NOT NULL,
    Username VARCHAR(255) NOT NULL UNIQUE,
    FirstLogin BOOLEAN DEFAULT TRUE,
    Ativo BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS Chamados (
    NumeroChamado BIGINT AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(255) NOT NULL,
    Descricao TEXT NOT NULL,
    FechamentoJustificativa TEXT NULL,
    Status INT NOT NULL,
    Prioridade INT NOT NULL,
    Categoria INT NOT NULL,
    Abertura DATETIME NOT NULL,
    Fechamento DATETIME NULL, 
    SolicitanteMatricula INT NOT NULL,
    TecnicoMatricula INT NULL, 
    FOREIGN KEY (SolicitanteMatricula) REFERENCES Usuarios(Matricula),
    FOREIGN KEY (TecnicoMatricula) REFERENCES Usuarios(Matricula)
);

INSERT INTO Usuarios (
    NomeCompleto, 
    Senha, 
    Telefone, 
    Funcao, 
    Theme,
    Username, 
    firstLogin,
    Ativo
) VALUES (
    'João Gabriel', 
    '$2a$10$oE4CtbwQme3FnXlUbVL0iOj.J/xois0ZaES/3hthG.xG4IVuquBxy',
    '16999999999', 
    2,
    0,
    'joaoadmin@eagletech', 
    0,
    1
);

INSERT INTO Usuarios (
    NomeCompleto, 
    Senha, 
    Telefone, 
    Funcao, 
    Theme,
    Username, 
    firstLogin,
    Ativo
) VALUES (
    'Barbara Favero', 
    '$2a$10$uUDG9g6GOiotJo/TLDmT2uTyrtiMwdCQ92DTn72En8LoK2cr.oao6',
    '16988888888', 
    1,
    0,
    'barbara@eagletech', 
    0,
    1
);

INSERT INTO Usuarios (
    NomeCompleto, 
    Senha, 
    Telefone, 
    Funcao, 
    Theme
    Username, 
    firstLogin,
    Ativo
) VALUES (
    'Breno Soad', 
    '$2a$10$uUDG9g6GOiotJo/TLDmT2uTyrtiMwdCQ92DTn72En8LoK2cr.oao6',
    '16977777777', 
    1,
    0,
    'breno@eagletech', 
    0,
    1
);

INSERT INTO Usuarios (
    NomeCompleto, 
    Senha, 
    Telefone, 
    Funcao, 
    Theme,
    Username, 
    firstLogin,
    Ativo
) VALUES (
    'Vinicius Alves', 
    '$2a$10$uUDG9g6GOiotJo/TLDmT2uTyrtiMwdCQ92DTn72En8LoK2cr.oao6',
    '16966666666', 
    0,
    0,
    'vinicius@eagletech', 
    0,
    1
);

INSERT INTO Usuarios (
    NomeCompleto, 
    Senha, 
    Telefone, 
    Funcao, 
    Theme,
    Username, 
    firstLogin,
    Ativo
) VALUES (
    'Daniele Arruda', 
    '$2a$10$uUDG9g6GOiotJo/TLDmT2uTyrtiMwdCQ92DTn72En8LoK2cr.oao6',
    '16966666666', 
    0,
    0,
    'dani@eagletech', 
    0,
    1
);