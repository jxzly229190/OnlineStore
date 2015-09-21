USE goujiuwang
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[sp_Paging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
  DROP PROCEDURE [dbo].[sp_Paging]
GO

CREATE PROCEDURE [dbo].[sp_Paging]
(
	@tableName varchar(100),     -- ������
	@pageIndex int,				 -- ҳ��
	@pageSize int,				 -- ÿҳ��С
	@pageColumn varchar(50),	 -- ��ҳ����
	@columnSelect varchar(200),  -- ��ѯ���У��Ѷ��ŷָ���Ϊ�����ѯ������
	@columnOrderBy varchar(200), -- ������У�DESC / ASC
	@condition nvarchar(200),    -- ��ѯ����
	@totalCount int output       -- �ܼ�¼��
)
AS
	-- �ж϶����Ƿ����
	IF OBJECT_ID(@tableName) IS NULL
	BEGIN
		RAISERROR(N'���󲻴���', 1, 16, @tableName)
		RETURN
	END
	-- �ж϶��������Ƿ���ȷ
	IF OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTable') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsView') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTableFunction') = 0
	BEGIN
		RAISERROR(N'�������Ͳ���ȷ������Ϊ������ͼ���ֵ����', 1, 16, @tableName)
		RETURN
	END
	-- ����ҳ����
	IF ISNULL(@pageIndex, 0) < 1 SET @pageIndex = 1
	IF ISNULL(@pageSize, 0) < 1 SET @pageSize = 10
	-- ����ҳ����
	IF ISNULL(@pageColumn, N'') = ''
	BEGIN
		RAISERROR(N'��ҳ�ֶα���Ϊ������Ψһ��', 1, 16)
		RETURN
	END
	-- ����ѯ����
	IF ISNULL(@columnSelect, N'') = N'' SET @columnSelect = N'*'
	-- ����������
	IF ISNULL(@columnOrderBy, N'') = N'' SET @columnOrderBy = N''
	ELSE SET @columnOrderBy = N' ORDER BY ' + LTRIM(@columnOrderBy)
	-- ����ѯ����
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