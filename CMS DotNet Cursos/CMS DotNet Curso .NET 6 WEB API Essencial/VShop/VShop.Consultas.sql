
----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE vs_shop_db;
-- SET SQL_SAFE_UPDATES = 0;


 select uuid();
 select now();
 
SELECT * FROM Categoria;
-- DELETE FROM Categoria;

SELECT * FROM Produto;
-- DELETE FROM Produto;
-- Insert Into Produto(nome,preco,descricao,estoque,imagem_url,CategoryId) Values('Caderno',7.55,'Caderno Espiral',10,'caderno1.jpg',1);
-- Insert Into Produto(nome,preco,descricao,estoque,imagem_url,CategoryId) Values('Lápis',3.45,'Lápis Preto',20,'lapis1.jpg',1);
-- Insert Into Produto(nome,preco,descricao,estoque,imagem_url,CategoryId) Values('Clips',5.33,'Clips para papel',50,'clips1.jpg',2);



----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE vs_shop_user_db;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUsers;
SELECT * FROM AspNetUserClaims;
SELECT * FROM AspNetUserRoles;


SELECT * FROM AspNetRoleClaims;
SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUserClaims;
SELECT * FROM AspNetUserLogins;
SELECT * FROM AspNetUserRoles;
SELECT * FROM AspNetUsers;
SELECT * FROM AspNetUserTokens;


----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE vs_shop_cart_db;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM CartHeaders;
SELECT * FROM CartItems;
SELECT * FROM Products;

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------


--  INSERT INTO xxxxx DEFAULT VALUES
--  GO 10000

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------








