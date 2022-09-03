/*
部署後指令碼樣板
--------------------------------------------------------------------------------------
 此檔案包含要附加到組建指令碼的 SQL 陳述式
 使用 SQLCMD 語法可將檔案包含在部署後指令碼中
 範例:      :r .\myfile.sql
 使用 SQLCMD 語法可參考部署後指令碼中的變數
 範例:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS(SELECT 1 FROM [dbo].[User] WHERE [Account] = 'admin')
BEGIN
    INSERT INTO [dbo].[User] ([Account],[Pwd],[Name],[Email],[Role],[Status],[CreateTime],[UpdateTime])
    VALUES (
        'admin','toolsdefault123', N'系統管理員', 'admin@gmail.com', 2, 1, GETDATE(), GETDATE())
END
GO