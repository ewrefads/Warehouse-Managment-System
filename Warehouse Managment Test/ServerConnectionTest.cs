using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Seeders;

namespace Warehouse_Management_Test
{
    public class ServerConnectionTest
    {
        [Fact]
        public void ServerConnectionCanBeEstablished()
        {
            try 
            {
                using(MySqlConnection con = new MySqlConnection("server = Localhost; port = 3306; user = root; password = test"))
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("CREATE DATABASE IF NOT EXISTS test;", con);
                    com.ExecuteNonQuery();
                    com = new MySqlCommand("DROP DATABASE IF EXISTS test;", con);
                    Assert.True(true);
                }
            }
            catch (Exception e)
            {
                Assert.Equal("", e.Message);
            }
        }
    }
}
