using Schedule.Moblie.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.Moblie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DependencyService.Register<ScheduleService>();
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
