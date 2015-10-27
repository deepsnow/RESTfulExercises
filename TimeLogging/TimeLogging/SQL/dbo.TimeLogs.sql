CREATE TABLE [dbo].[TimeLogs] (
    [Id]        INT            NOT NULL,
    [UserId]    NVARCHAR (128) NOT NULL,
    [StartTime] DATETIME       NOT NULL,
    [EndTime]   DATETIME       NOT NULL,
    [Comment]   VARCHAR (MAX)  NOT NULL,
    [Billable] BIT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUsers_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

