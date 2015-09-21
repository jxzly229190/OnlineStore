namespace V5.Automate
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Schematic
    {
        private readonly Library.Storage.DB.SqlServer sqlServer;

        private const string getDbTables = "use [{0}] select id,name from sysobjects where xtype='U' order by name";

        private const string getDbColumns = "use [{0}] select top 1 * from [{1}]";

        private const string getDbColumnSummary = "use [{0}] select value from ::fn_listextendedproperty(null,'user','dbo','table','{1}','column',null) where objname = '{2}'";

        private const string getDbTableSummary = "use [{0}] select value from ::fn_listextendedproperty(null,'user','dbo','table','{1}',default,null)";

        public Schematic()
        {
            this.sqlServer = new Library.Storage.DB.SqlServer();
        }

        public List<string> GetDbNames()
        {
            var dbNames = new List<string>();

            try
            {
                var dataReader = this.sqlServer.ExecuteDataReader(CommandType.Text, "use master select name from dbo.sysdatabases order by dbid desc", null, null);
                while (dataReader.Read())
                {
                    dbNames.Add(dataReader[0].ToString());
                }

                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return dbNames;
        }

        public List<DbTable> GetDbTables(string database)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new ArgumentNullException("database");
            }

            var dbTables = new List<DbTable>();

            try
            {
                var commandText = string.Format(getDbTables, database);

                var dataReader = this.sqlServer.ExecuteDataReader(CommandType.Text, commandText, null, null);
                while (dataReader.Read())
                {
                    var dbTable = new DbTable
                    {
                        Id = dataReader[0].ToString(),
                        Name = dataReader[1].ToString()
                    };

                    commandText = string.Format(getDbTableSummary, database, dbTable.Name);
                    var dbTableSummary = this.sqlServer.ExecuteScalar(CommandType.Text, commandText, null, null);
                    if (dbTableSummary == null)
                    {
                        dbTableSummary = "The " + dbTable.Name + " class.";
                    }

                    dbTable.Summary = dbTableSummary.ToString();
                    dbTable.DbColumns = this.GetDbColumns(database, dbTable.Name);

                    dbTables.Add(dbTable);
                }

                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return dbTables;
        }

        public DbTable GetDbTable(string database, string table)
        {
            var dbTables = this.GetDbTables(database);

            if (dbTables != null && dbTables.Count > 0)
            {
                return dbTables.Find(item => item.Name == table);
            }

            return null;
        }

        public List<DbColumn> GetDbColumns(string database, string dbTable)
        {
            if (string.IsNullOrEmpty(database))
            {
                throw new ArgumentNullException("database");
            }

            if (string.IsNullOrEmpty(dbTable))
            {
                throw new ArgumentNullException("dbTable");
            }

            var dbColumns = new List<DbColumn>();

            try
            {
                var commandText = string.Format(getDbColumns, database, dbTable);
                IDataReader dataReader = this.sqlServer.ExecuteDataReader(CommandType.Text, commandText, null, null);

                for (var i = 0; i < dataReader.FieldCount; i++)
                {

                    var dbColumn = new DbColumn
                    {
                        Id = i + 1,
                        Name = dataReader.GetName(i),
                        DbColumnType = dataReader.GetDataTypeName(i),
                        Type = dataReader.GetFieldType(i)
                    };

                    commandText = string.Format(getDbColumnSummary, database, dbTable, dbColumn.Name);
                    var summary = this.sqlServer.ExecuteScalar(CommandType.Text, commandText, null, null);
                    dbColumn.Summary = summary != null ? summary.ToString() : string.Empty;

                    dbColumns.Add(dbColumn);
                }

                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return dbColumns;
        }
    }
}