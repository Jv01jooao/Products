using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Using_ViewModels_in_MVVM.Model;
using Using_ViewModels_in_MVVM.MVVM;

namespace UsingViewModelsinMVVM.ViewModel
{

    class MainWindowsViewModel : ViewModelBase
    {

        public MainWindowsViewModel() 
        {
            
            string json = JsonSerializer.Serialize(Items);
            Items = new ObservableCollection<Item>();
            Load();
            
        }

        public ObservableCollection<Item> Items { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());

        private Item selectedItem;

        public Item SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private void AddItem()
        {
            Items.Add(new Item
            {
                Name = "NEW ITEM",
                SerialNumber = "XXXXX",
                Quantity = 0
            });
        }

        private void DeleteItem()
        {
            Items.Remove(SelectedItem);
            Save();
        }
        
        private void Save()
        {
            string json = JsonSerializer.Serialize(Items);
            File.WriteAllText("dados.json", json);
        }

        private bool CanSave() 
        {
            return true;
        }

        private void Load()
        {
            if (!File.Exists("dados.json"))
            {
                Items = new ObservableCollection<Item>();
                return;
            }

            string json = File.ReadAllText("dados.json");
            var lista = JsonSerializer.Deserialize<ObservableCollection<Item>>(json);

            // ESTA É A FORMA CORRETA:
            // Substitui a lista inteira pelos dados do arquivo
            Items = lista ?? new ObservableCollection<Item>();

            // Avisa a interface que a lista "Items" agora é outra
            OnPropertyChanged(nameof(Items));

            // DELETE TODO O CÓDIGO ABAIXO (o loop for):
            // for (int i = 0; i < quantidade; i++) { ... } 
        }

    }
}
