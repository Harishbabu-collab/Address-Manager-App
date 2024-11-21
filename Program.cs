using System;
using System.Data.Entity;
using Wisej.Web;

namespace WisejWebApplication2
{
    internal static class Program
    {
        static void Main()
        {
            Database.InitializeDatabase();
            Window1 window = new Window1();
            window.Show();
        }
    }
}