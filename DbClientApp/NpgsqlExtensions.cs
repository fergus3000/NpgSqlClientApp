using Npgsql;
using NpgsqlTypes;
using System.Text;

namespace DbClientApp
{
    public static class NpgsqlExtensions
    {
        public static NpgsqlCommand CreateCommand(this NpgsqlConnection connection, string commandText)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = commandText;
            return cmd;
        }

        public static NpgsqlCommand CreateCommand(this NpgsqlConnection connection, int commandTimeoutSec)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandTimeout = commandTimeoutSec;
            return cmd;
        }

        public static void AddParameter(this NpgsqlCommand command, string parameterName, object value) => command.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);

        public static void AddParameter(this NpgsqlCommand command, string parameterName, NpgsqlDbType dbType, object value) => command.Parameters.AddWithValue(parameterName, dbType, value ?? DBNull.Value);

        public static void AddJsonParameter(this NpgsqlCommand command, string parameterName, string jsonString) => command.Parameters.AddWithValue(parameterName, NpgsqlDbType.Jsonb, jsonString);

        public static DateTime GetDateTimeAsUtc(this NpgsqlDataReader reader, int ordinal)
        {
            var dt = reader.GetDateTime(ordinal);
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }

        public static T GetFieldValueOrDefault<T>(this NpgsqlDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? default : reader.GetFieldValue<T>(ordinal);
        }

        public static string GetParameterString(this NpgsqlParameterCollection sqlParams)
        {
            if (!sqlParams.Any())
            {
                return "No parameters";
            }

            var sb = new StringBuilder();
            foreach (var param in sqlParams.ToArray())
            {
                sb.Append($"{param.ParameterName}:{param.Value};");
            }
            return sb.ToString();
        }


    }
}
