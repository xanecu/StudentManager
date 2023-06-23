--CREATE TABLE cursos(
--codcurso CHAR(8)PRIMARY KEY,
--curso VARCHAR(120) NOT null

--);

--INSERT INTO cursos(codcurso, curso) VALUES ('info', 'Informática'),
--('multi','Multimédia'),
--('redes','Redes e comunicações'),('bd', 'Base de Dados');


--CREATE TABLE alunos (
--  num INT IDENTITY primary key,
--  naluno VARCHAR (120)NOT NULL,
--  codcurso CHAR(8) FOREIGN KEY REFERENCES cursos(codcurso) ON DELETE cascade ON UPDATE CASCADE,
--  datanasc DATE DEFAULT '2020-01-29',
--  idade AS DATEDIFF(YEAR,datanasc, GETDATE()),
--  media DECIMAL CHECK(media BETWEEN 0 AND 20)

--);


--INSERT INTO alunos (naluno,codcurso,media) VALUES ('Ana Francisco','bd','12.0'),
-- ('José Eduardo','info','10.0'),
--  ('Rui Pinto','info','19.0'),
--   ('Inês Godinho','multi','11.0'),
--    ('Diogo José','redes','18.0');

--SET IDENTITY_INSERT alunos ON,
--INSERT INTO ...
--SET IDENTITY_INSERT alunos OFF; Alterar numeração automatica para manual

select * from cursos;
select * from alunos;