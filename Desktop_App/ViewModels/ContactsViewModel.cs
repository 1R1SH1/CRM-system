using Desktop_App.Core;
using Desktop_App.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Desktop_App.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand ShowContactsWindow { get; set; }

        private object _currentView;
        private ContactsViewModel _contactsviewmodel;

        public ContactsViewModel ContactViewModel
        {
            get => _contactsviewmodel;
            set
            {
                _contactsviewmodel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContactViewModel)));
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentView)));
            }
        }

        public ContactsViewModel()
        {
            ContactViewModel = new ContactsViewModel("Contacts");

            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new(o => { Application.Current.MainWindow.DragMove(); });
            ShutDownWindowCommand = new(o => { Application.Current.Shutdown(); });
            MaximizeWindowCommand = new(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });

            MinimizeWindowCommand = new(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            ShowContactsWindow = new(o => { CurrentView = ContactViewModel; });
        }

        private RelayCommand _changedselection;
        private RelayCommand _deleteselection;
        private RelayCommand _getdatascomm;
        private RelayCommand _send;
        private Contacts _selected;
        private List<Contacts> _rawContacts = new();
        private List<Contacts> _contacts = new();
        private string _description;
        private string _image;
        private string _name;
        private string _surName;
        private string _lastName;
        private string _phone;
        private string _email;
        private string _address;


        public RelayCommand ChangedSelection => _changedselection ?? (_changedselection = new RelayCommand(obj => Update()));
        public RelayCommand GetDatasComm => _getdatascomm ?? (_getdatascomm = new RelayCommand(obj => GetDatas()));
        public RelayCommand DeleteSelection => _deleteselection ?? (_deleteselection = new RelayCommand(obj => Delete()));
        public RelayCommand Send => _send ?? (_send = new RelayCommand(obj => Create()));

        public Contacts Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public List<Contacts> RawContacts
        {
            get => _rawContacts;
            set
            {
                _rawContacts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RawContacts)));
            }
        }
        public List<Contacts> Contact
        {
            get => _contacts;
            set
            {
                _contacts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contact)));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string ContactInformation
        {
            get => _description;
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContactInformation)));
            }
        }
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }
        public string SurName
        {
            get => _surName;
            set
            {
                _surName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SurName)));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
            }
        }
        public string EMail
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EMail)));
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        private string ApiType { get; set; }
        private Dictionary<int, int> Ids { get; set; }
        public ContactsViewModel(string apitype)
        {
            ApiType = apitype;
            GetDatas();
        }

        public void GetDatas()
        {
            CRUD.Read(ApiType);
            RawContacts = JsonConvert.DeserializeObject<List<Contacts>>(CRUD.Read(ApiType));
            Show();
        }
        private void Create()
        {
            CRUD.Create(ApiType, JsonConvert.SerializeObject(new Contacts(0, Image, Name, SurName, LastName, Phone, EMail, Address, ContactInformation)));
            GetDatas();
        }
        private void Update()
        {
            CRUD.Update(ApiType, JsonConvert.SerializeObject(new Contacts(Ids[Selected.Id], Selected.Image, Selected.Name, Selected.SurName, Selected.LastName, Selected.Phone, Selected.EMail, Selected.Address, Selected.ContactsInformation)));
            GetDatas();
        }
        private void Delete()
        {
            CRUD.Delete($"{ApiType}/{Ids[Selected.Id]}");
            GetDatas();
        }
        private void Show()
        {
            Contact.Clear();
            Ids = new Dictionary<int, int>();
            for (int i = 0; i < RawContacts.Count; i++)
            {
                Ids.Add(i + 1, RawContacts[i].Id);
                Contact.Add(RawContacts[i]);
                Contact[Contact.Count - 1].Id = i + 1;
            }
        }
    }
}
