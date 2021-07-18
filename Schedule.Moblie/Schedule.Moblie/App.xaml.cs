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

            DependencyService.Register<IScheduleService, ScheduleService>();

            MainPage = new NavigationPage(new MainPage());
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
