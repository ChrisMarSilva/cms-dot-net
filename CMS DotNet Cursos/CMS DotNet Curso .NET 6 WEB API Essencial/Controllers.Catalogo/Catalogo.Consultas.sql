
----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE catalogo_api;
-- SET SQL_SAFE_UPDATES = 0;

 select uuid();
 select now();
 
SELECT * FROM Categoria;
-- DELETE FROM Categoria;

SELECT * FROM Produto;
-- DELETE FROM Produto;


----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

--  SET SQL_SAFE_UPDATES = 0;

--  DROP INDEX IDX_Categoria_01 ON Categoria;
--  CREATE INDEX IDX_Categoria_01 ON Categoria (data_cadastro);

--  DROP INDEX IDX_Categoria_02 ON Categoria;
--  CREATE INDEX IDX_Categoria_02 ON Categoria (data_cadastro, id, data_alteracao, imagem_url, nome);

--  DROP INDEX IDX_Categoria_03 ON Categoria;
--  CREATE INDEX IDX_Categoria_03 ON Categoria (data_cadastro, id, nome);

--  MELHOR TEMPO
--  DROP INDEX IDX_Categoria_04 ON Categoria;
--  CREATE INDEX IDX_Categoria_04 ON Categoria (data_cadastro, id, nome, data_alteracao, imagem_url);


    Explain  SELECT c.id, c.data_alteracao, c.data_cadastro, c.imagem_url, c.nome
      FROM Categoria AS c
      where c.data_cadastro >= TIMESTAMP '2000-01-01 00:00:00'
      ORDER BY c.data_cadastro
      LIMIT 100 OFFSET 0
	  
----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

--  SET SQL_SAFE_UPDATES = 0;

--  DROP INDEX IDX_Produto_01 ON Produto;
--  CREATE INDEX IDX_Produto_01 ON Produto (data_cadastro);

--  DROP INDEX IDX_Produto_02 ON Produto;
--  CREATE INDEX IDX_Produto_02 ON Produto (data_cadastro, id, categoria_id, data_alteracao, descricao, estoque, imagem_url, nome, preco);

--  DROP INDEX IDX_Produto_03 ON Produto;
--  CREATE INDEX IDX_Produto_03 ON Produto (data_cadastro, id, categoria_id, estoque, nome, preco);

--  MELHOR TEMPO
--  DROP INDEX IDX_Produto_04 ON Produto;
--  CREATE INDEX IDX_Produto_04 ON Produto (data_cadastro, id, categoria_id, estoque, nome, preco, data_alteracao, descricao, imagem_url);


    Explain  SELECT p.id, p.categoria_id, p.data_alteracao, p.data_cadastro, p.descricao, p.estoque, p.imagem_url, p.nome, p.preco
      FROM Produto AS p
      WHERE p.data_cadastro >= TIMESTAMP '2000-01-01 00:00:00'
      ORDER BY p.data_cadastro
      LIMIT 100 OFFSET 0

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------


--  INSERT INTO xxxxx DEFAULT VALUES
--  GO 10000

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------








