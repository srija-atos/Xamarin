using SQLite;
using Student_Management_1.DependencyInterfaces;
using Student_Management_1.Models;
using Student_Management_1.OfflineStorage;
using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Student_Management_1
{
    public partial class App : Application
    {
        private static SQLiteAsyncConnection Database=null;
        public static SQLiteAsyncConnection DatabaseConnection 
        {
            get
            {
                if (Database == null)
                    Database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                return Database;
            }

        }
        private void CreateDBTables()
        {
            DatabaseConnection.CreateTableAsync<Person>().Wait();
            DatabaseConnection.DropTableAsync<Departments>().Wait();
            DatabaseConnection.CreateTableAsync<Departments>().Wait();
            DepartmentOperations.CreateDepartmentsList();
        }
        public App()
        {
            InitializeComponent();
            CreateDBTables();
            CultureInfo culture = new CultureInfo("te");
            DependencyService.Get<ILocalize>().SetLocale(culture);
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
