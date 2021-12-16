using Dapper;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class GenericService<T>
    {
        protected readonly string _tableName;

        protected readonly List<string> _ignoreFieldsInsert;
        protected readonly List<string> _ignorePropertiesInsert;

        protected readonly List<string> _ignoreFieldsUpdate;
        protected readonly List<string> _ignorePropertiesUpdate;

        protected readonly List<string> _propertiesInsert = new List<string>();
        protected readonly List<string> _fieldsInsert = new List<string>();

        protected readonly List<string> _propertiesUpdate = new List<string>();
        protected readonly List<string> _fieldsUpdate = new List<string>();

        protected readonly int maxParallProcess = 4; // Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.5) * 2.0));

        public GenericService(T item, string tableName, List<string> ignoreFieldsInsert, List<string> ignorePropertiesInsert, List<string> ignoreFieldsUpdate, List<string> ignorePropertiesUpdate)
        {
            _ignoreFieldsInsert = ignoreFieldsInsert;
            _ignorePropertiesInsert = ignorePropertiesInsert;
            _ignoreFieldsUpdate = ignoreFieldsUpdate;
            _ignorePropertiesUpdate = ignorePropertiesUpdate;
            _tableName = tableName;

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
                string query = GenericSQLCommands.SELECT_TABLE_FIELDS.Replace("IGNORAR", String.Join(",", _ignoreFieldsInsert)).Replace("TABELA", _tableName);
                var resultado = connection.Query(query);
                foreach (var res in resultado)
                {
                    _fieldsInsert.Add(res.name);
                }

                query = GenericSQLCommands.SELECT_TABLE_FIELDS.Replace("IGNORAR", String.Join(",", _ignoreFieldsUpdate)).Replace("TABELA", _tableName);
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
                dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType)
                   ?? info.PropertyType);

            foreach (var entity in self)
                dataTable.Rows.Add(properties.Select(p => p.GetValue(entity)).ToArray());

            return dataTable;
        }
    }
}
