
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Ordinacia.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Ordinacia.Data_Access.AuthenticationDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }
    } 
}