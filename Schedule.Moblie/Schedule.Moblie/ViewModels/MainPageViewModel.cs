using Schedule.Moblie.Models.Entities;
using Schedule.Moblie.Models.Enums;
using Schedule.Moblie.Services;
using Schedule.Moblie.ViewModels.Base;
using Schedule.Moblie.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Schedule.Moblie.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        #region Fields
        private IScheduleService _service;
        private INavigation _navigation;
        #endregion

        #region Propeties
        public string Title { get; set; }
        public Button LastClickedButton { get; set; }
        public List<Event> AllEvents { get; set; }
        public ObservableCollection<Event> ShowedEvents { get; set; }
        #endregion

        #region Commands
        public Command OnAddCommand { get; set; }
        public Command<Button> OnWeekdayClickedCommand { get; set; }
        public Command<Event> DeleteItemTapped { get; }
        public Command<Event> EditItemTapped { get; }
        #endregion

        #region MainPageViewModel()
        public MainPageViewModel(INavigation nav)
        {
            _navigation = nav;
            _service = DependencyService.Get<IScheduleService>();

            OnAddCommand = new Command(OnAdd);
            OnWeekdayClickedCommand = new Command<Button>(OnWeekdayClicked);
            DeleteItemTapped = new Command<Event>(OnDeleteItemClicked);
            EditItemTapped = new Command<Event>(OnEditItemClicked);

            ShowedEvents = new ObservableCollection<Event>();
            AllEvents = new List<Event>();
        }

        public void OnWeekdayClicked(Button button)
        {
            var eventsToShow = new List<Event>();

            if (AllEvents != null)
            {
                switch (button.Text)
                {
                    case "Mon":
                        Title = "Monday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Tue":
                        Title = "Tuesday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Wed":
                        Title = "Wednesday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Thu":
                        Title = "Thursday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Fri":
                        Title = "Friday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Sat":
                        Title = "Saturday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Sun":
                        Title = "Sunday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                }
            }

            LastClickedButton = button;
        }

        public async void OnAppearing()
        {
            // AllEvents = new List<Event>(await _service.GetEventList());

            if (string.IsNullOrEmpty(Title))
            {
                Title = "Monday";

                if (AllEvents.Count > 0)
                {
                    var eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                    ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                }
            }
        }
        #endregion

        private async void OnAdd()
        {
            await _navigation.PushAsync(new EditEventPage());
        }

        private async void OnDeleteItemClicked(Event item)
        {
            if (item == null)
                return;

            bool delete = await App.Current.MainPage.DisplayAlert("Delete event", "Are You sure?", "Yes", "No");
            if (delete)
            {
                var deleted = await _service.RemoveEvent(item.Id);

                if (deleted)
                {
                    await ExecuteLoadProjectsCommand();
                }
            }
        }

        private async void OnEditItemClicked(Event obj)
        {
            if (obj == null)
                return;

            await _navigation.PushAsync(new EditEventPage());
        }

        private Task ExecuteLoadProjectsCommand()
        {
            throw new NotImplementedException();
        }
    }
}
