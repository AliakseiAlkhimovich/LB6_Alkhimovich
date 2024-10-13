CREATE TABLE [dbo].[Table] (
    [CarId] INT IDENTITY(1,1) NOT NULL,
    [Marka] NVARCHAR (50) NOT NULL,
    [Model] NVARCHAR (50) NOT NULL,
    [Year]  INT NOT NULL,
    [Color] NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([CarId] ASC)
);
