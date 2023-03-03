
----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

USE DevEvents;

/*

DELETE FROM DevEventSpeakers;
DELETE FROM DevEvents;

*/


SELECT * FROM DevEvents WITH(NOLOCK);
--  CREATE INDEX IDX_DevEvents_01 ON DevEvents (IsDeleted);
--  CREATE INDEX IDX_DevEvents_02 ON DevEvents (Start_Date);
--  CREATE INDEX IDX_DevEvents_03 ON DevEvents (Start_Date, IsDeleted);
SELECT * FROM DevEventSpeakers WITH(NOLOCK);

 SELECT [d].[Id], [d].[Description], [d].[End_Date], [d].[IsDeleted], [d].[Start_Date], [d].[Title]
      FROM [DevEvents] AS [d]
      WHERE [d].[IsDeleted] = CAST(0 AS bit)
	  ORDER BY [d].[Start_Date]


----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------

SELECT [t].[Id], [t].[Description], [t].[End_Date], [t].[IsDeleted], [t].[Start_Date], [t].[Title], [d0].[Id], [d0].[DevEventId], [d0].[LinkedInProfile], [d0].[Name], [d0].[TalkDescription], [d0].[TalkTitle]
      FROM (
          SELECT TOP(2) [d].[Id], [d].[Description], [d].[End_Date], [d].[IsDeleted], [d].[Start_Date], [d].[Title]
          FROM [DevEvents] AS [d]
          WHERE [d].[Id] = '208b878a-2071-4e75-9bfe-635bc2361bac'
      ) AS [t]
      LEFT JOIN [DevEventSpeakers] AS [d0] ON [t].[Id] = [d0].[DevEventId]
      ORDER BY [t].[Id]

SELECT [t].[Id], [t].[Description], [t].[End_Date], [t].[IsDeleted], [t].[Start_Date], [t].[Title], [d0].[Id], [d0].[DevEventId], [d0].[LinkedInProfile], [d0].[Name], [d0].[TalkDescription], [d0].[TalkTitle]
      FROM (
          SELECT TOP(1) [d].[Id], [d].[Description], [d].[End_Date], [d].[IsDeleted], [d].[Start_Date], [d].[Title]
          FROM [DevEvents] AS [d]
          WHERE [d].[Id] ='208b878a-2071-4e75-9bfe-635bc2361bac'
      ) AS [t]
      LEFT JOIN [DevEventSpeakers] AS [d0] ON [t].[Id] = [d0].[DevEventId]
      ORDER BY [t].[Id]

	  
----------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------