using Dapper;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DUDS.Service
{
    public class GenericService<T>
    {
        protected string TableName { get; }

        protected readonly List<string> _ignoreFieldsInsert = new List<string> { "'id'", "'data_criacao'", "'ativo'" };
        protected readonly List<string> _ignorePropertiesInsert = new List<string> { "Id", "DataCriacao", "Ativo" };

        protected readonly List<string> _ignoreFieldsUpdate = new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" };
        protected readonly List<string> _ignorePropertiesUpdate = new List<string> { "Id", "DataCriacao", "UsuarioCriacao" };

        protected readonly List<string> _propertiesInsert = new List<string>();
        protected readonly List<string> _fieldsInsert = new List<string>();

        protected readonly List<string> _propertiesUpdate = new List<string>();
        protected readonly List<string> _fieldsUpdate = new List<string>();

        protected readonly int maxParallProcess = Environment.GetEnvironmentVariable("MAX_PARALLEL_JOBS") != null ?
            Convert.ToInt32(Environment.GetEnvironmentVariable("MAX_PARALLEL_JOBS")) :
            4; // Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.5) * 2.0));

        public GenericService(T item, string tableName)
        {
            TableName = tableName;

            PropertyInfo[] myPropertyInfo = item.GetType().GetProperties();
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                if (!_ignorePropertiesInsert.Contains(myPropertyInfo[i].Name))
                {
                    _propertiesInsert.Add("@" + myPropertyInfo[i].Name);
                }

                if (!_ignorePropertiesUpdate.Contains(myPropertyInfo[i].Name))
                {
                    _propertiesUpdate.Add("@" + myPropertyInfo[i].Name);
                }
            }
            using (var connection = SqlHelpers.ConnectionFactory.Conexao())
            {
                string query = GenericSQLCommands.SELECT_TABLE_FIELDS.Replace("IGNORAR", String.Join(",", _ignoreFieldsInsert)).Replace("TABELA", TableName);
                var resultado = connection.Query(query);
                foreach (var res in resultado)
                {
                    _fieldsInsert.Add(res.name);
                }

                query = GenericSQLCommands.SELECT_TABLE_FIELDS.Replace("IGNORAR", String.Join(",", _ignoreFieldsUpdate)).Replace("TABELA", TableName);
                resultado = connection.Query(query);
                foreach (var res in resultado)
                {
                    _fieldsUpdate.Add(res.name);
                }
            }
        }

        public DataTable ToDataTable(IEnumerable<T> self)
        {
            var properties = typeof(T).GetProperties();

            var dataTable = new DataTable();
            foreach (var info in properties)
                dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType);

            foreach (var entity in self)
                dataTable.Rows.Add(properties.Select(p => p.GetValue(entity)).ToArray());

            dataTable.TableName = TableName;
            return dataTable;
        }

        public SqlBulkCopy SqlBulkCopyMapping(SqlBulkCopy sqlBulkCopy)
        {
            var properties = typeof(T).GetProperties();
            foreach (var info in properties)
                if (_ignoreFieldsInsert.Contains(info.Name)) sqlBulkCopy.ColumnMappings.Add(info.Name, info.Name) ;
            sqlBulkCopy.DestinationTableName = TableName;
            return sqlBulkCopy;
        }
    }
}
