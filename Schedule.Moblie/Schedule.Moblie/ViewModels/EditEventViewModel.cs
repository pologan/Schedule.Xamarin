using Schedule.Moblie.Models;
using Schedule.Moblie.Models.Entities;
using Schedule.Moblie.Services;
using Schedule.Moblie.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Schedule.Moblie.ViewModels
{
    public class EditEventViewModel : BaseViewModel
    {
        #region Fields
        private readonly IScheduleService _service;
        private EventBuilder _builder { get; set; }
        #endregion

        #region Propeties
        public Event Item { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Place { get; set; }
        public string PersonName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> Weekdays { get; set; }
        public KeyValuePair<int, string> SelectedWeekday { get; set; }
        public bool IsAdditionalVisible { get; set; } = false;
        public bool IsBaseVisible { get; set; } = false;
        public bool IsNotesVisible { get; set; } = false;
        #endregion

        #region Command
        public Command AddItemCommand { get; set; }
        public Command OnAdvancedClickedCommand { get; set; }
        public Command OnAddNoteClickedCommand { get; set; }
        public Command AcceptAddInfoCommand { get; set; }
        public Command AcceptNoteCommand { get; set; }
        public Command BackCommand { get; set; }
        #endregion
        public EditEventViewModel(Event item, INavigation navigation)
        {
            _service = DependencyService.Get<IScheduleService>();

            _builder = new EventBuilder();

            AddItemCommand = new Command(AddItem);
            OnAdvancedClickedCommand = new Command(OnAdvancedClicked);
            AcceptAddInfoCommand = new Command(AcceptAdditionalInfo);
            OnAddNoteClickedCommand = new Command(AddNoteClicked);
            AcceptNoteCommand = new Command(AcceptNote);
            BackCommand = new Command(Back);

            Item = item;

            StartTime = item.StartTime;
            EndTime = item.EndTime;
            Name = item.Name;
            Place = item.Place;
            PersonName = item.PersonToMeet;
            Note = item.Note;
        }

       

        public EditEventViewModel(INavigation navigation)
        {
            _service = DependencyService.Get<IScheduleService>();

            _builder = new EventBuilder();

            AddItemCommand = new Command(AddItem);
            OnAdvancedClickedCommand = new Command(OnAdvancedClicked);
            AcceptAddInfoCommand = new Command(AcceptAdditionalInfo);
            OnAddNoteClickedCommand = new Command(AddNoteClicked);
            AcceptNoteCommand = new Command(AcceptNote);
        }

        private void Back()
        {
            IsNotesVisible = false;
            IsAdditionalVisible = false;
            IsBaseVisible = true;
        }

        private void AcceptNote()
        {
            _builder.BuildNote(Note);

            Back();
        }

        private void AddNoteClicked()
        {
            IsBaseVisible = false;
            IsNotesVisible = true;
        }

        private void AcceptAdditionalInfo()
        {
            _builder.BuildAdditionalInfo(Place, PersonName);

            Back();
        }

        private void OnAdvancedClicked()
        {
            IsBaseVisible = false;
            IsAdditionalVisible = true;
        }

        private async void AddItem()
        {
            _builder.BuildBase(Name, (DayOfWeek)SelectedWeekday.Key, StartTime, EndTime);
            Item = _builder.GetEvent();
            if (Item == null)
            {
                await _service.AddEvent(Item);
            }
            else
            {
                await _service.UpdateEvent(Item);
            }
        }

        public void OnAppearing()
        {
            IsBaseVisible = true;
        }
    }
}
