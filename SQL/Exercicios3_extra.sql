/*		Usando a estrutura do banco treinamento_pubs (abaixo diagramado), escreva consultas que retornem os dados abaixo solicitados		*/

use treinamento_pubs

/* TABELAS */
SELECT * FROM authors_gmoreira		/* au_id, au_lname, au_fname, city, state, zip */
SELECT * FROM discounts_gmoreira	/* discounttype, stor_id, lowqty, highqty, discount */
SELECT * FROM publishers_gmoreira	/* pub_id, pub_name, city, state, country */
SELECT * FROM sales_gmoreira		/* stor_id, ord_num, qty, title_id */
SELECT * FROM stores_gmoreira		/* stor_id, stor_name, city, state, zip */
SELECT * FROM titleauthor_gmoreira	/* au_id, title_id, au_ord, royaltyper */
SELECT * FROM titles_gmoreira		/* title_id, title, type, pub_id, price, advance, ytd_sales */

-- 1. Autores que não possuem livros publicados.
select * from authors_gmoreira
where au_id not in (
	select au_id from titleauthor_gmoreira
)


-- 3. Livros que tenham sido vendidos em lojas que possuam desconto superior a 5% (desconsiderar a quantidade de volumes necessária para se obter o desconto).
select * from titles_gmoreira titles
	inner join sales_gmoreira sales on titles.title_id=sales.title_id
	inner join discounts_gmoreira disc on sales.stor_id=disc.stor_id
where discount>=5


-- 4. Uma lista, com uma única coluna, contendo todas as cidades existentes em tabelas do banco PUBS.
select city from authors_gmoreira
union
select city from stores_gmoreira
union
select city from publishers_gmoreira


-- 5. Uma lista com os pares de autores que moram na mesma cidade.
select a1.au_fname+a1.au_lname 'Nome do autor 1', a2.au_fname+a2.au_lname 'Nome do autor 2'
from authors_gmoreira a1
	inner join authors_gmoreira a2 on a1.au_id <> a2.au_id
where a1.city=a2.city


-- 6. O nome dos editores (publishers) que publicam livros de negócios (business) ou de culinária (mod_cook).
select distinct pub_name from publishers_gmoreira pub
	inner join titles_gmoreira tit on pub.pub_id=tit.pub_id
where type in ('mod_cook','business')


-- 7. A média de preço dos livros de cada editor (publisher).
select pub_name, avg(price) 'Média de preço' from titles_gmoreira titles
	left outer join publishers_gmoreira pub on pub.pub_id=titles.pub_id
group by pub_name


-- 9. Os títulos de livros cujo editor não se localiza na mesma cidade onde vivem os autores.
select distinct title from titles_gmoreira titles
	inner join publishers_gmoreira pub on titles.pub_id=pub.pub_id
	inner join titleauthor_gmoreira t_author on titles.title_id=t_author.title_id
	inner join authors_gmoreira authors on authors.au_id=t_author.au_id
where pub.city<>authors.city


-- 10. Autores (authors) que não possuem livros (titles) que foram vendidos (sales) no estado (state) da california (CA).
select au_fname+au_lname Autor from authors_gmoreira authors
where not exists (
	select au_id from titleauthor_gmoreira t_au
		inner join sales_gmoreira sales on sales.title_id=t_au.title_id
		inner join stores_gmoreira stores on stores.stor_id=sales.stor_id
	where authors.au_id=t_au.au_id
	and state='ca'
)


-- 11. Autores (authors) que venderam (sales) livros (titles) no ano de 1994.
select au_fname+au_lname Autor from authors_gmoreira authors
where exists (
	select au_id from titleauthor_gmoreira t_au
		inner join sales_gmoreira sales on sales.title_id=t_au.title_id
		inner join stores_gmoreira stores on stores.stor_id=sales.stor_id
	where authors.au_id=t_au.au_id
	and ord_date like '%1994%'
)
