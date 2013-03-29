SELECT 
o.name tableName,
c.name columnName,
c.is_nullable,
t.name as type,
c.max_length,
dc.definition defaulValue,
c.name + ' '
	+ t.name 
	+ case t.name 
		WHEN 'varchar'then ' (' + CONVERT(varchar,c.max_length) + ')' 
		WHEN 'nvarchar'then ' (' + CONVERT(varchar,c.max_length) + ')' 
		WHEN 'char'then ' (' + CONVERT(varchar,c.max_length) + ')'
		else ''
	  end
	+ case 
		WHEN c.is_nullable = 0 and not dc.object_id is null then ' NOT NULL DEFAULT ' + REPLACE(REPLACE(CONVERT(varchar,dc.definition), '(',''),')','')
		WHEN c.is_nullable = 0 then ' NOT NULL'
		else ''
	  end
	+ ','
as createStatement,
'@' + c.name + ' '
	+ UPPER(t.name 
	+ case t.name 
		WHEN 'varchar'then ' (' + CONVERT(varchar,c.max_length) + ')' 
		WHEN 'nvarchar'then ' (' + CONVERT(varchar,c.max_length) + ')' 
		WHEN 'char'then ' (' + CONVERT(varchar,c.max_length) + ')'
		else ''
	  end
	+ '= NULL,')
as vars,
c.name + '= ISNULL(@' + c.name + ', ' + c.name + '),'
as updateStatement,
c.name + ',' columnList,
'@' + c.name + ',' insertValues,
c.name + ' = st.' + c.name + ',' LinqSelect

FROM sys.objects o
inner join sys.columns c on c.object_id = o.object_id 
	and o.name like 'query'
	and c.name like '%%'
inner join sys.types t on t.user_type_id = c.user_type_id 
left join sys.default_constraints dc on dc.object_id = c.default_object_id
	and c.is_nullable = 0


