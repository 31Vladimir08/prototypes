using System.Data.Common;

using Microsoft.Data.SqlClient;

using Npgsql;

namespace DeliveryDatasService.Extensions
{
    public static class DbConnectionExtensions
    {
        public static DbCommand CreateCommand(this DbConnection connection, string sqlQuery)
        {
            if (connection is NpgsqlConnection npgConnection)
            {
                sqlQuery = sqlQuery.Replace('[', '"').Replace(']', '"');
                return new NpgsqlCommand(sqlQuery, npgConnection);
            }
            else if (connection is SqlConnection sqlConnection)
            {
                return new SqlCommand(sqlQuery, sqlConnection);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static DbDataAdapter CreateAdapter(this DbConnection connection, string sqlQuery)
        {
            if (connection is NpgsqlConnection npgConnection)
            {
                sqlQuery = sqlQuery.Replace('[', '"').Replace(']', '"');
                return new NpgsqlDataAdapter(sqlQuery, npgConnection);
            }
            else if (connection is SqlConnection sqlConnection)
            {
                return new SqlDataAdapter(sqlQuery, sqlConnection);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
