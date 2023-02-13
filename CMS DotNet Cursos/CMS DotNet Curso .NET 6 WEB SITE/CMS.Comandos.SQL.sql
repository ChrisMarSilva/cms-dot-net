
USE IWantDb;

SELECT * FROM AspNetRoles;

SELECT * FROM AspNetUsers;
-- DELETE FROM AspNetUsers;
-- chris.mar.silva@gmail.com
-- Senha@123

SELECT * FROM AspNetUserClaims;
-- DELETE FROM AspNetUserClaims;

SELECT U.Email, C.ClaimValue AS Name 
FROM AspNetUsers U 
  INNER JOIN AspNetUserClaims C ON (C.UserId = U.Id and C.ClaimType = 'Name') 
ORDER BY C.ClaimValue;


SELECT * FROM Categories ORDER BY Name;
-- DELETE FROM Categories;
-- INSERT INTO Categories (Name) VALUES ('Categoria 01');
-- update Categories set active = 1;

SELECT * FROM Products ORDER BY Name;
-- DELETE FROM Products;
-- update Products set active = 1;
-- update Products set isstock = 1;

SELECT * FROM Orders ORDER BY Id;
-- DELETE FROM Orders;

SELECT * FROM OrderProducts ORDER BY OrdersId, ProductsId;
-- DELETE FROM OrderProducts;


SELECT p.Id, p.Name, count(1) as Qtd
FROM Orders o
  INNER JOIN OrderProducts op on ( op.OrdersId = o.Id )
  INNER JOIN Products p on ( p.Id = op.ProductsId )
GROUP BY p.Id, p.Name
ORDER BY Qtd DESC



SELECT * FROM LogAPI;
-- TRUNCATE TABLE LogAPI;


------------------------------------------------------------------------------
------------------------------------------------------------------------------