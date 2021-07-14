using Schedule.Moblie.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Schedule.Moblie
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm;

        public Button LastClickedButton { get; set; }

        public MainPage()
        {
            InitializeComponent();

            BindingContext = _vm = new MainPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LastClickedButton = monButton;
            monButton.TextColor = (Color)App.Current.Resources["clrAccent"];

            _vm.OnAppearing();
        }

        private void WeekdayButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            LastClickedButton.TextColor = Color.White;
            button.TextColor = (Color)App.Current.Resources["clrAccent"];

            _vm.OnWeekdayClicked(button);

            LastClickedButton = button;
        }
    }
}
