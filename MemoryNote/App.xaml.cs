using MemoryNote.Services;
using MemoryNote.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoryNote
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            //MainPage = new SplashPage();
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
