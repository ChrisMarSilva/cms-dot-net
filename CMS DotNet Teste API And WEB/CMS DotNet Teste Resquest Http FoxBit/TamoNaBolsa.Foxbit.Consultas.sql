----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE tamo_na_bolsa_foxbit;
-- SET SQL_SAFE_UPDATES = 0;

SELECT count(1) FROM TbTnBFoxbit_SystemTime;
SELECT * FROM TbTnBFoxbit_SystemTime;
-- TRUNCATE TABLE TbTnBFoxbit_SystemTime;
-- DELETE FROM TbTnBFoxbit_SystemTime;
-- Insert Into TbTnBFoxbit_SystemTime(nome,preco,descricao,estoque,imagem_url,CategoryId) Values('Caderno',7.55,'Caderno Espiral',10,'caderno1.jpg',1);

SELECT count(1) FROM TbTnBFoxbit_Currencies;
SELECT * FROM TbTnBFoxbit_Currencies;

SELECT count(1) FROM TbTnBFoxbit_Markets;
SELECT * FROM TbTnBFoxbit_Markets;
SELECT * FROM TbTnBFoxbit_Markets where quote_name = 'Real';

SELECT count(1) FROM TbTnBFoxbit_MarketQuotes;
SELECT * FROM TbTnBFoxbit_MarketQuotes;

SELECT count(1) FROM TbTnBFoxbit_MemberInfos;
SELECT * FROM TbTnBFoxbit_MemberInfos;


SELECT COUNT(1) as QTD, MIN(created_at) as MenorData, MAX(created_at) as MaiorData FROM TbTnBFoxbit_Trades;
-- 578  -- 2023-09-15 18:40:10.364 -- 2023-12-06 12:13:27.173 

SELECT market_symbol, COUNT(1) as QTD FROM TbTnBFoxbit_Trades GROUP BY market_symbol ORDER BY 2;

SELECT * FROM TbTnBFoxbit_Trades;
-- DELETE FROM TbTnBFoxbit_Trades;
-- TRUNCATE TABLE TbTnBFoxbit_Trades;




----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

 select uuid();
 select now();
 
--  INSERT INTO xxxxx DEFAULT VALUES
--  GO 10000

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------



----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------








