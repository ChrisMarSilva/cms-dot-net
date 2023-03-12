
----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE TarefasDB;




/*


Drop Table Tarefas;

Create Table Tarefas( 
  id UNIQUEIDENTIFIER  Not Null default NEWID(), -- NEWSEQUENTIALID()
  atividade NVARCHAR (255) Not Null, 
  status NVARCHAR(100) Not Null, 
  data_cadastro datetime2(7) Not Null default getdate(), 
  data_alteracao datetime2(7) NULL, 
  CONSTRAINT PK_Tarefas PRIMARY KEY (id)
);


Truncate Table Tarefas;
delete from Tarefas;
insert into Tarefas (atividade, status) values ('Tarefa 1', 'em andamento');
insert into Tarefas (atividade, status) values ('Tarefa 2', 'concluida');

*/

SELECT COUNT(1) FROM Tarefas with(nolock); 

SELECT top 10 * FROM Tarefas with(nolock);  
-- DELETE FROM Tarefas;

--  INSERT INTO Tarefas (atividade, status) VALUES ('Tarefa X', 'nada') 
GO 10000


----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

--  DROP INDEX IDX_Tarefas_01 ON Tarefas;
--  CREATE INDEX IDX_Tarefas_01 ON Tarefas (data_cadastro);

--  DROP INDEX IDX_Tarefas_02 ON Tarefas;
--  CREATE INDEX IDX_Tarefas_02 ON Tarefas (status);
	  

----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------








