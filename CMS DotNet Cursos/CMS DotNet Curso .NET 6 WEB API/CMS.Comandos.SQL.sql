
USE Products;

SELECT * FROM Categories;
-- DELETE FROM Categories;
-- INSERT INTO Categories (Name) VALUES ('Categoria 01');
-- INSERT INTO Categories (Name) VALUES ('Categoria 02');
-- INSERT INTO Categories (Name) VALUES ('Categoria 03');

SELECT * FROM Products;
-- DELETE FROM Products;

SELECT * FROM Tags;
-- DELETE FROM Tags;



 SELECT [t].[Id], [t].[CategoryId], [t].[Code], [t].[Description], [t].[Name], [t].[Id0], [t].[Name0], [t0].[Id], [t0].[Name], [t0].[ProductId]
      FROM (
          SELECT TOP(1) [p].[Id], [p].[CategoryId], [p].[Code], [p].[Description], [p].[Name], [c].[Id] AS [Id0], [c].[Name] AS [Name0]
          FROM [Products] AS [p]
          INNER JOIN [Categories] AS [c] ON [p].[CategoryId] = [c].[Id]
          WHERE [p].[Id] = 3
      ) AS [t]
      LEFT JOIN [Tags] AS [t0] ON [t].[Id] = [t0].[ProductId]
      ORDER BY [t].[Id], [t].[Id0]




-------------