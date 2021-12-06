using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Project_A
{
    public class WWDB
    {
        string pg_sign_data = "Host=localhost;Username=postgres;Password=admin123;Database=GuitarLessons";
        private NpgsqlConnection con;

        public WWDB()
        {
            this.con = new NpgsqlConnection(pg_sign_data);
            this.con.Open();
        }
        public String get_version()
        {
            string com = "SELECT version()";
            var cmd = new NpgsqlCommand(com, this.con);

            var version = cmd.ExecuteScalar().ToString();
            return $"PostgreSQL version: {version}";
        }
    }
}
