USE goujiuwang
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[sp_Paging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[sp_Paging]
GO

CREATE PROCEDURE [dbo].[sp_Paging]
(
	@tableName varchar(100),     -- 表名称
	@pageIndex int,				 -- 页码
	@pageSize int,				 -- 每页大小
	@pageColumn varchar(50),	 -- 分页的列
	@columnSelect varchar(200),  -- 查询的列，已逗号分隔，为空则查询所有列
	@columnOrderBy varchar(200), -- 排序的列，DESC / ASC
	@condition nvarchar(200),    -- 查询条件
	@totalCount int output       -- 总记录数
)
AS
	-- 判断对象是否存在
	IF OBJECT_ID(@tableName) IS NULL
	BEGIN
		RAISERROR(N'对象不存在', 1, 16, @tableName)
		RETURN
	END
	-- 判断对象类型是否正确
	IF OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTable') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsView') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTableFunction') = 0
	BEGIN
		RAISERROR(N'对象类型不正确，必须为：表、视图或表值函数', 1, 16, @tableName)
		RETURN
	END
	-- 检查分页参数
	IF ISNULL(@pageIndex, 0) < 1 SET @pageIndex = 1
	IF ISNULL(@pageSize, 0) < 1 SET @pageSize = 10
	-- 检查分页的列
	IF ISNULL(@pageColumn, N'') = ''
	BEGIN
		RAISERROR(N'分页字段必须为主键或唯一键', 1, 16)
		RETURN
	END
	-- 检查查询的列
	IF ISNULL(@columnSelect, N'') = N'' SET @columnSelect = N'*'
	-- 检查排序的列
	IF ISNULL(@columnOrderBy, N'') = N'' SET @columnOrderBy = N''
	ELSE SET @columnOrderBy = N' ORDER BY ' + LTRIM(@columnOrderBy)
	-- 检查查询条件
	IF ISNULL(@condition, N'') = N'' SET @condition= N''
	ELSE SET @condition = N' AND (' + @condition + N')'
	
	DECLARE @commandText nvarchar(1000)	
	
	SET @commandText = N'SELECT @totalCount = COUNT(' + @pageColumn + N') FROM ' + @tableName
	EXEC sp_executesql @commandText, N'@totalCount int output', @totalCount output
	SET @totalCount = @totalCount
	
	IF @pageIndex = 1
		BEGIN
			SET @condition = replace(@condition, 'AND', 'WHERE')
			EXEC( N'SELECT TOP ' + @pageSize
				+ N' ' + @columnSelect
				+ N' FROM '+ @tableName
				+ N' ' + @condition
				+ N' ' + @columnOrderBy )
		END
	ELSE
		BEGIN
			SET @commandText = N'SELECT TOP ' + ltrim(str(@pageSize)) + N' ' + @columnSelect + N' FROM ' + @tableName + N' WHERE ' + @pageColumn + N' > '
			+ N'(SELECT MAX(' + @pageColumn + N') FROM '
			+ N'(SELECT TOP ' + ltrim(str(@pageSize * (@pageIndex-1))) + N' ' + @pageColumn + N' FROM ' + @tableName + ' ORDER BY ' + @pageColumn
			+ N') AS T)' + @condition + @columnOrderBy
			EXEC(@commandText)
		END