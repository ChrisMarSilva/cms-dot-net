
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


SELECT * FROM Categories;
-- DELETE FROM Categories;
-- INSERT INTO Categories (Name) VALUES ('Categoria 01');

SELECT * FROM Products;
-- DELETE FROM Products;

SELECT * FROM LogAPI;
-- DELETE FROM LogAPI;

------------------------------------------------------------------------------
------------------------------------------------------------------------------