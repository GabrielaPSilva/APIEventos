using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.SQL
{
    public class GenericSQLCommands
    {
        public const string ACTIVATE_COMMAND = "UPDATE TABELA SET ativo = 1 WHERE id = @id";
        public const string DISABLE_COMMAND = "UPDATE TABELA SET ativo = 0 WHERE id = @id";
        public const string INSERT_COMMAND = "INSERT INTO TABELA (CAMPOS) VALUES (VALORES)";
        public const string DELETE_COMMAND = "DELETE FROM TABELA WHERE id = @id";
        public const string SELECT_TABLE_FIELDS = "SELECT c.name FROM sys.columns c INNER JOIN sys.tables t ON t.object_id = c.object_id AND t.name = 'TABELA' AND t.type = 'U' WHERE c.name not in (IGNORAR)";
        public const string UPDATE_COMMAND = "UPDATE TABELA SET VALORES WHERE id = @Id";
    }
}
