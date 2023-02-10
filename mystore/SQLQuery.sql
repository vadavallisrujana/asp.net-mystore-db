CREATE TABLE [dbo].[mytable] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]       VARCHAR (100) NOT NULL,
    [email]      VARCHAR (150) NOT NULL,
    [phone]      VARCHAR (100) NULL,
    [address]    VARCHAR (100) NULL,
    [created_at] DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);

select * from mytable