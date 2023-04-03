use treinamento_pubs

/* TABELAS */
SELECT * FROM authors_gmoreira		/* au_id, au_lname, au_fname, city, state, zip */
SELECT * FROM discounts_gmoreira	/* discounttype, stor_id, lowqty, highqty, discount */
SELECT * FROM publishers_gmoreira	/* pub_id, pub_name, city, state, country */
SELECT * FROM sales_gmoreira		/* stor_id, ord_num, qty, title_id */
SELECT * FROM stores_gmoreira		/* stor_id, stor_name, city, state, zip */
SELECT * FROM titleauthor_gmoreira	/* au_id, title_id, au_ord, royaltyper */
SELECT * FROM titles_gmoreira		/* title_id, title, type, pub_id, price, advance, ytd_sales */


/*		1 - Escreva o SQL para as seguintes situações		*/

-- A: Que códigos dos livros foram pedidos por cada loja?
select stor_name, title_id from stores_gmoreira stores
	inner join sales_gmoreira sales on stores.stor_id=sales.stor_id

-- B: Que autores moram na mesma cidade que um editor?
select au_fname+au_lname 'Nome do autor' from authors_gmoreira authors
where exists (
	select city from publishers_gmoreira pub
	where authors.city=pub.city
)

-- C: Que autores moram na mesma cidade?
select distinct a1.au_fname+a1.au_lname 'Nome do autor', a1.city
from authors_gmoreira a1
	inner join authors_gmoreira a2 on a1.au_id <> a2.au_id
where a1.city=a2.city

-- D: Qual a média de preço dos livros de cada editor?
select pub_name Editor, avg(price) 'Média de preço' from titles_gmoreira titles
	inner join publishers_gmoreira pub on pub.pub_id=titles.pub_id
group by pub_name

-- E: Quantos livros cada autor vendeu?
select authors.au_id, sum(qty) Quantidade from authors_gmoreira authors
	inner join titleauthor_gmoreira t_au on t_au.au_id=authors.au_id
	inner join sales_gmoreira sales on sales.title_id=t_au.title_id
group by authors.au_id

-- F: Quais livros possuem mais de um autor?
select title from titles_gmoreira titles
	inner join titleauthor_gmoreira t_au on t_au.title_id=titles.title_id
group by title
having count(au_id) > 1

-- G: Quais autores não possuem livros que foram vendidos no estado da Califórnia (CA)?
select au_fname+au_lname Autor from authors_gmoreira authors
where not exists (
	select au_id from titleauthor_gmoreira t_au
		inner join sales_gmoreira sales on sales.title_id=t_au.title_id
		inner join stores_gmoreira stores on stores.stor_id=sales.stor_id
	where authors.au_id=t_au.au_id
	and state='ca'
)

/*		2 - Crie um banco de dados com as seguintes especificações		*/
-- A: Tabela Aluno_[SEU_LOGIN]
CREATE TABLE aluno_gmoreira (
	id_aluno int PRIMARY KEY,
	nome char(50) NOT NULL,
	matricula char(8) NOT NULL,
	nascimento date,
	email char(30)
)

-- B: Tabela Curso_[SEU_LOGIN]
CREATE TABLE curso_gmoreira (
	id_curso int PRIMARY KEY,
	nome char(50) NOT NULL,
	qtd_semestres int NOT NULL
)

-- C: Tabela AlunoCurso_[SEU_LOGIN]
CREATE TABLE alunocurso_gmoreira (
	id_aluno int FOREIGN KEY REFERENCES aluno_gmoreira(id_aluno),
	id_curso int FOREIGN KEY REFERENCES curso_gmoreira(id_curso),
	ingresso date NOT NULL
)

-- D: Tabela Disciplina_[SEU_LOGIN]
CREATE TABLE disciplina_gmoreira (
	id_disciplina int PRIMARY KEY,
	id_curso int FOREIGN KEY REFERENCES curso_gmoreira(id_curso),
	nome char(50) NOT NULL,
	semestre int NOT NULL
)

/*		3 - Popule as tabelas criadas no exercício anterior da seguinte forma		*/
-- A: Tabela Aluno_[SEU_LOGIN]
	-- 2 alunos com data de nascimento e sem e-mail;
	-- 2 alunos sem data de nascimento e com e-mail;
	-- 1 aluno com data de nascimento e com e-mail;
	-- 1 aluno sem data de nascimento e sem e-mail;
INSERT INTO aluno_gmoreira VALUES
	(1, 'Afonso', '12345678', '1980-01-01', NULL),
	(2, 'Bernardo', '22345678', '1990-01-01', NULL),
	(3, 'Carlos', '33345678', NULL, 'carlos@email.com'),
	(4, 'Diego', '44445678', NULL, 'diego@email.com'),
	(5, 'Eduardo', '55555678', '2000-01-01', 'eduardo@email.com'),
	(6, 'Gesael', '66666678', NULL, NULL);

-- B: Tabela Curso_[SEU_LOGIN]:
	-- 2 cursos com duração de 3 semestres;
	-- 1 curso com duração de 4 semestres;
INSERT INTO curso_gmoreira VALUES
	(1, 'Agronomia',  3),
	(2, 'Biotecnologia', 3),
	(3, 'Contabilidade', 4);

-- C: Tabela AlunoCurso_[SEU_LOGIN]:
	-- Distribua 5 alunos entre os 3 cursos;
INSERT INTO alunocurso_gmoreira VALUES
	(1, 1, '2020-01-01'),
	(2, 1, '2020-02-02'),
	(3, 2, '2020-03-03'),
	(4, 2, '2020-04-04'),
	(5, 3, '2020-05-05');

-- D: Tabela Disciplina_[SEU_LOGIN]:
	-- 3 disciplinas para cada curso de 3 semestres, sendo uma em cada semestre;
	-- 4 disciplinas para o curso de 4 semestres, sendo uma em cada semestre;
INSERT INTO disciplina_gmoreira VALUES
	(1, 1, 'Anatomia Vegetal 1', 1),
	(2, 1, 'Anatomia Vegetal 2', 2),
	(3, 1, 'Anatomia Vegetal 3', 3),
	(4, 2, 'Bioquimica 1', 1),
	(5, 2, 'Bioquimica 2', 2),
	(6, 2, 'Bioquimica 3', 3),
	(7, 3, 'Calculo 1', 1),
	(8, 3, 'Calculo 2', 2),
	(9, 3, 'Calculo 3', 3),
	(10, 3, 'Calculo 4', 4);

/*		4 - Agora escreva o SQL para as seguintes situações, utilizando as suas tabelas criadas			*/
-- A: Quais alunos possuem e-mail “...@gmail.com”?
select * from aluno_gmoreira where email like '%@gmail.com'

-- B: Quais cursos têm mais de um aluno matriculado?
select * from curso_gmoreira
where id_curso in (
	select id_curso from alunocurso_gmoreira
	group by id_curso
	having count(id_curso) > 1
)

-- C: Para cada aluno, exiba quantos cursos ele está associado, considerando também os alunos que não estão em nenhum curso.
select aluno.nome Aluno, count(curso.id_curso) 'Quantidade de Cursos' from aluno_gmoreira aluno
	left outer join alunocurso_gmoreira ac on ac.id_aluno=aluno.id_aluno
	left outer join curso_gmoreira curso on curso.id_curso=ac.id_curso
group by aluno.nome

-- D: Qual o aluno mais velho?
select * from aluno_gmoreira
where nascimento=(
	select min(nascimento) from aluno_gmoreira
)

-- E: Para cada curso, qual o aluno que ingressou primeiro?
select curso.nome, aluno.nome, ingresso from curso_gmoreira curso
	inner join alunocurso_gmoreira ac on curso.id_curso=ac.id_curso
	inner join aluno_gmoreira aluno on aluno.id_aluno=ac.id_aluno
where ingresso=(
	select min(ingresso) from alunocurso_gmoreira ac2
	where ac.id_curso=ac2.id_curso
)

-- F: Para cada curso, liste as disciplinas do último período.
select curso.nome, disciplina.nome from curso_gmoreira curso
	inner join disciplina_gmoreira disciplina on disciplina.id_curso=curso.id_curso
where semestre=qtd_semestres

-- G: Utilizando subconsultas, retorne os cursos que possuem alunos sem e-mail e que não possua disciplinas que o nome inicie com “Cálculo...”;
select * from curso_gmoreira
where id_curso in (
	select id_curso from alunocurso_gmoreira
	where id_aluno in (
		select id_aluno from aluno_gmoreira
		where email is null
	)
	and
	id_curso in (
		select id_curso from disciplina_gmoreira
		where nome not like 'calculo%'
	)
)

-- H: Utilizando EXISTS e NOT EXISTS, retorne os mesmos cursos da questão anterior;
select * from curso_gmoreira curso
where exists (
	select distinct id_curso from alunocurso_gmoreira ac
	where ac.id_curso=curso.id_curso
	and exists (
		select distinct id_aluno from aluno_gmoreira aluno
		where aluno.id_aluno=ac.id_aluno
		and email is null
	)
	and not exists (
		select distinct id_curso from disciplina_gmoreira disciplina
		where disciplina.id_curso=curso.id_curso
		and nome like 'calculo%'
	)
)

-- I: Liste os alunos nascidos entre 01/01/1980 e 31/12/1999 de forma que os dados sejam exibidos como o exemplo abaixo:
	-- Código do Aluno: 0001; Nome do aluno: João Dias;
	-- Código do Aluno: 0002; Nome do aluno: Maria Fernanda;
	-- Código do Aluno: 0003; Nome do aluno: Carlos Eduardo;
	-- Código do Aluno: 0004; Nome do aluno: Manoel Silva;
select 'Código do Aluno: ' + convert(char,id_aluno), 'Nome do aluno: ' + nome from aluno_gmoreira
where nascimento between '1980/01/01' and '1999/12/31'
