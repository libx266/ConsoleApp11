using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public abstract class Repository<T>
    {
        private const string _connectionString = "";

        protected List<T> Select(string sql)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return Deserialize(dataTable).ToList();
                    }
                }
            }
        }

        protected int Save(T obj)
        {
            //мне лень писать сохранение в базу
            int id = Random.Shared.Next(0, byte.MaxValue); //типа сохранили запись и получили id из базы
            return id;
        }

        private static string Camel (string fieldName)
        {
            var upper = (char c) => c > 0x60 && c < 0x7B ? Convert.ToChar(c - 0x20) : c;

            var result = new List<char>();

            bool up = true;

            foreach (char c in fieldName)
            {
                if (up)
                {
                    result.Add(upper(c));
                    up = false;
                }
                else
                {
                    if (c == '_')
                    {
                        up = true;
                    }
                    else
                    {
                        result.Add(c);
                    }
                }
            }

            return new string(result.ToArray());
        }

        private static IEnumerable<T> Deserialize(DataTable table)
        {
            var type = typeof(T);

            foreach (DataRow row in table.Rows)
            {
                var result = Activator.CreateInstance(type);
                foreach (DataColumn column in table.Columns)
                {
                    type.GetProperty(Camel(column.ColumnName)).SetValue(result, row[column.ColumnName]);
                }

                yield return (T)result;
            }
        }
    }
}
