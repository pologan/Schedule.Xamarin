using Schedule.Moblie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.Moblie.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEventPage : ContentPage
    {
        private readonly EditEventViewModel _vm;
        public EditEventPage()
        {
            InitializeComponent();

            BindingContext = _vm = new EditEventViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _vm.OnAppearing();
        }
    }
}