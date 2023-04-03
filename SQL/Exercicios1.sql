use treinamento_pubs

/* TABELAS */
SELECT * FROM authors_gmoreira		/* au_id, au_lname, au_fname, city, state, zip */
SELECT * FROM discounts_gmoreira	/* discounttype, stor_id, lowqty, highqty, discount */
SELECT * FROM publishers_gmoreira	/* pub_id, pub_name, city, state, country */
SELECT * FROM sales_gmoreira		/* stor_id, ord_num, qty, title_id */
SELECT * FROM stores_gmoreira		/* stor_id, stor_name, city, state, zip */
SELECT * FROM titleauthor_gmoreira	/* au_id, title_id, au_ord, royaltyper */
SELECT * FROM titles_gmoreira		/* title_id, title, type, pub_id, price, advance, ytd_sales */


/*										EXERC�CIO 2										*/
-- A: Liste todas as categorias de t�tulos publicados antes de 12/06/1991, exibindo cada categoria somente uma vez;
select distinct type from titles_gmoreira
where pubdate < '12/06/1991'

-- B: Liste as lojas cujo estado seja Calif�rnia (CA), mas que a cidade n�o seja Los Gatos;
select * from stores_gmoreira
where state='ca' and city <> 'Los Gatos'

-- C: Liste os t�tulos cujo nome comece com a letra �C�, ordenando decrescentemente pelo pre�o;
select * from titles_gmoreira
where title like 'C%' order by price desc

-- D: Liste os t�tulos que venderam mais de mil c�pias e custam menos de R$ 10,00, nas categorias �business� e �modern cooking�.
select * from titles_gmoreira
where ytd_sales > 1000 and price < 10 and type like '[b-m]%'


/*										EXERC�CIO 3										*/
-- A: Qual a renda gerada por cada t�tulo? Coloque o nome �RENDA� para a coluna com o resultado e �C�DIGO� para title_id;
select (ytd_sales) RENDA, (title_id) C�DIGO from titles_gmoreira

-- B: Um aumento de 15% no pre�o por livro est� projetado para o ano que vem. Liste os pre�os antigos e novos usando �PRE�O_ANTIGO� e �PRE�O_NOVO� como cabe�alhos e �C�DIGO� para title_id;
select (title_id) C�DIGO, (price) PRE�O_ANTIGO, (price*1.15) PRE�O_NOVO from titles_gmoreira

-- C: Liste os t�tulos que tem renda inferior a 10.000,00, exibindo o �C�DIGO� e �RENDA� e ordenando pela renda;
select (title_id) C�DIGO, (ytd_sales) RENDA from titles_gmoreira
where ytd_sales < 100000 order by ytd_sales

-- D: Que t�tulos ainda n�o obtiveram um adiantamento?
select * from titles_gmoreira where advance is null


/*										EXERC�CIO 4										*/
-- A: Quantos editores publicaram livros?
select count(distinct stor_id) from sales_gmoreira

-- B: Quantos livros da categoria de neg�cios (business) foram vendidos?
select sum (ytd_sales) from titles_gmoreira
where type='business'

-- C: Se o pre�o de todos os livros sofrer um aumento de 15% pr�ximo ano, qual ser� o pre�o m�dio do custo de um livro no pr�ximo ano em compara��o a este ano?
select avg(price), avg(price*1.15) from titles_gmoreira

-- D: Suponha que qualquer adiantamento que ainda n�o foi decidido � de $5000. Encontre o adiantamento m�dio.
select avg(isnull(advance, 5000)) from titles_gmoreira


/*										EXERC�CIO 5										*/
-- A: Quantos livros cada editor vendeu este ano?
select stor_id, sum(qty) from sales_gmoreira
group by stor_id

-- B: Quantos livros existem em cada categoria?
select type, count(type) from titles_gmoreira
group by type

-- C: Para os editores que possuem t�tulos com pre�os inferiores a $10, calcule a m�dia dos pre�os dos seus livros;
select pub_id, avg(price) from titles_gmoreira
where pub_id in (select pub_id from titles_gmoreira where price <10)
group by pub_id

-- D: Qual � o custo do livro mais barato e do mais caro para cada categoria de livros?
select type, min(price), max(price) from titles_gmoreira
group by type


/*										EXERC�CIO 6										*/
-- A: Qual o nome do autor que escreveu o livro com o c�digo �MC2222�?
select au_fname, au_lname from authors_gmoreira
inner join titleauthor_gmoreira on titleauthor_gmoreira.au_id=authors_gmoreira.au_id
where titleauthor_gmoreira.title_id='MC2222'

-- B: Quantos livros cada autor vendeu?
select au_fname+au_lname, sum(qty) from authors_gmoreira
inner join titleauthor_gmoreira on authors_gmoreira.au_id=titleauthor_gmoreira.au_id
inner join sales_gmoreira on sales_gmoreira.title_id=titleauthor_gmoreira.title_id
group by au_fname+au_lname

-- C: Que descontos foram oferecidos � loja �Eric the Read Books�?
select * from discounts_gmoreira
where stor_id=(select stor_id from stores_gmoreira where stor_name='Bookbeat')

-- D: Um cliente telefonou para pedir um livro, eles apenas sabiam que era um livro de psicologia escrito por uma mulher chamada Ann ou Anne. Qual o nome completo da autora e o t�tulo do livro?
select au_fname, au_lname, title from authors_gmoreira
inner join titleauthor_gmoreira on authors_gmoreira.au_id=titleauthor_gmoreira.au_id
inner join titles_gmoreira on titles_gmoreira.title_id=titleauthor_gmoreira.title_id
where type='psychology' and au_fname like 'Ann%'

-- E: Liste todos os autores e, se existir um editor na cidade em que eles moram, liste o pub_name.
select * from authors_gmoreira
left outer join publishers_gmoreira on authors_gmoreira.city=publishers_gmoreira.city


/*										EXERC�CIO 7										*/
-- A: Que autores moram na mesma cidade da Algodata Infosystems?
select * from authors_gmoreira
where city=(select city from publishers_gmoreira where pub_name='Algodata Infosystems')

-- B: Quantos livros a loja 'Bookbeat' pediu?
select sum(qty) from sales_gmoreira
where stor_id=(select stor_id from stores_gmoreira where stor_name='Bookbeat')

-- C: Que livros n�o foram pedidos?
select * from titles_gmoreira
where title_id not in (select title_id from sales_gmoreira)


/*										EXERC�CIO 8										*/
-- A: Que livros os editores de Massachusetts imprimiram?
select * from titles_gmoreira
where exists (
	select * from publishers_gmoreira
	where titles_gmoreira.pub_id=publishers_gmoreira.pub_id and state='ma'
)

-- B: Que lojas pediram livros de psicologia?
select distinct stor_id from sales_gmoreira
where exists (
	select * from titles_gmoreira where sales_gmoreira.title_id=titles_gmoreira.title_id
	and type='psychology'
)

-- C: Que livros foram publicados, mas n�o foram vendidos?
select * from titles_gmoreira
where not exists (
	select * from sales_gmoreira
	where titles_gmoreira.title_id=sales_gmoreira.title_id
)

/*										EXERC�CIO 9										*/
-- A: Que estados aparecem no banco de dados?
select state from authors_gmoreira
union
select state from publishers_gmoreira
union
select state from stores_gmoreira

-- B: Que livros s�o ou escritos ou editados na Calif�rnia (CA)?
select title from titles_gmoreira
inner join titleauthor_gmoreira on titles_gmoreira.title_id=titleauthor_gmoreira.title_id
inner join authors_gmoreira on authors_gmoreira.au_id=titleauthor_gmoreira.au_id
where authors_gmoreira.state='ca'
union
select title from titles_gmoreira
inner join publishers_gmoreira on titles_gmoreira.pub_id=publishers_gmoreira.pub_id
where state='ca'

-- C: Que livros custam mais que o pre�o m�dio dos livros, ou tem um desconto superior ao desconto m�dio?
select title from titles_gmoreira
where price > (select avg(price) from titles_gmoreira)
union
select title from titles_gmoreira
inner join sales_gmoreira on sales_gmoreira.title_id=titles_gmoreira.title_id
inner join discounts_gmoreira on discounts_gmoreira.stor_id=sales_gmoreira.stor_id
where discount > (select avg(discount) from discounts_gmoreira)
