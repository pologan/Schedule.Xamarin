using Schedule.Moblie.Models.Entities;
using Schedule.Moblie.Models.Enums;
using Schedule.Moblie.SaveStrategy;
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
        private ISaveToFileStrategy _saveStrategy;
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
        public Command OnRefreshCommand { get; set; }
        public Command<Event> DeleteItemTapped { get; }
        public Command<Event> EditItemTapped { get; }
        public Command SaveToFileCommand { get; set; }
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
            OnRefreshCommand = new Command(async () => await ExecuteLoadEventsCommand());
            SaveToFileCommand = new Command(SaveToFile);

            ShowedEvents = new ObservableCollection<Event>();
            AllEvents = new List<Event>();
        }

        private async void SaveToFile(object obj)
        {
            bool xmlSelected = await App.Current.MainPage.DisplayAlert("Saving to file", "Select type", "Xml", "Json");

            if(xmlSelected)
            {
                _saveStrategy = new SaveToXMLStrategy();
                _saveStrategy.Save(AllEvents);
            }
            else
            {
                _saveStrategy = new SaveToJsonStrategy();
                _saveStrategy.Save(AllEvents);
            }
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
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Tuesday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Wed":
                        Title = "Wednesday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Wednesday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Thu":
                        Title = "Thursday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Thursday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Fri":
                        Title = "Friday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Friday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Sat":
                        Title = "Saturday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Saturday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                    case "Sun":
                        Title = "Sunday";
                        eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Sunday).ToList();
                        ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                        break;
                }
            }

            LastClickedButton = button;
        }

        public async void OnAppearing()
        {
            if (string.IsNullOrEmpty(Title))
            {
                Title = "Monday";

                if (AllEvents.Count > 0)
                {
                    var eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
                    ShowedEvents = new ObservableCollection<Event>(eventsToShow);
                }
            }
            await ExecuteLoadEventsCommand();
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
                    await ExecuteLoadEventsCommand();
                }
            }
        }

        private async void OnEditItemClicked(Event obj)
        {
            if (obj == null)
                return;
            
            bool copy = await App.Current.MainPage.DisplayAlert("Edit item", "Copy or edit?", "Copy", "Edit");

            if (copy)
            {
                var evt = obj.DeepCopy();
                await _service.AddEvent(evt);

                await ExecuteLoadEventsCommand();
            }
            else
            {
                await _navigation.PushAsync(new EditEventPage(obj));
            }
        }

        private async Task ExecuteLoadEventsCommand()
        {
            IsBusy = true;

            var list = await _service.GetEventList();
            AllEvents = new List<Event>(list);
            Title = "Monday";
            var eventsToShow = AllEvents.Where(e => e.Weekday == (int)WeekdayEnum.Monday).ToList();
            ShowedEvents = new ObservableCollection<Event>(eventsToShow);

            IsBusy = false;
        }
    }
}
