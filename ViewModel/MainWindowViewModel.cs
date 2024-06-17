using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Pokemons.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;


namespace Pokemons.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {
        public MainWindowViewModel()
        {
            OpenImageCommand = new DelegateCommand(OpenImage);
            AddnewPokemonCommand = new DelegateCommand(AddNewPokemon);
            SavePokemonCommand = new DelegateCommand(SaveCurrentPokemon);
            DeletePokemonCommand = new DelegateCommand(DeleteCurrentPokemon);
        }
        
        public DelegateCommand OpenImageCommand { get; }
        public DelegateCommand AddnewPokemonCommand { get; }
        public DelegateCommand SavePokemonCommand { get; }
        public DelegateCommand DeletePokemonCommand { get; }
        public ObservableCollection<PokemonModel> AllPokemons { get;private set; } = DataWorker.GetAllPokemons();
        public BitmapImage PokemonImage { get; set; } = new BitmapImage();

        private PokemonModel _currentItem;

        public PokemonModel CurrentItem
        {
            get { return _currentItem; }
            set {
                _currentItem = value;
                if (value!=null && value.BitmapImage!= null)
                {
                    PokemonImage = value.BitmapImage;
                }
                RaisePropertyChanged(nameof(PokemonImage));
                if ((value != null && value.Name != null))
                {
                    PokemonName = value.Name;
                }
                RaisePropertyChanged(nameof(PokemonName));
            }
        }

        public string PokemonName { get; set; }

        /// <summary>
        /// Открытие картинки
        /// </summary>
        private void OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|" +
                                                 "*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (openFileDialog.ShowDialog()==true)
            {
                PokemonImage = new BitmapImage(new Uri(openFileDialog.FileName));
                RaisePropertyChanged(nameof(PokemonImage));
            }
        }
        /// <summary>
        /// Добавление нового покемона
        /// </summary>
        private void AddNewPokemon()
        {
            if (!string.IsNullOrEmpty(PokemonName) && PokemonImage.UriSource != null)
            {
                var pokemonModel = new PokemonModel();
                pokemonModel.Name = PokemonName;
                pokemonModel.Image = Converter.EncodingImageToArray(PokemonImage);
                using ApplicationContext db = new ApplicationContext();
                db.PokemonModels.Add(pokemonModel);
                db.SaveChanges();
                var newPokemon = new PokemonModel();
                newPokemon.ID = pokemonModel.ID;
                newPokemon.Name = pokemonModel.Name;
                newPokemon.BitmapImage = PokemonImage;
                AllPokemons.Add(newPokemon);
            }
        }
        /// <summary>
        /// Удаление выбранного покемона
        /// </summary>
        private void DeleteCurrentPokemon()
        {
            if (CurrentItem!=null)
            {
                if (DataWorker.DeleteCurrentPokemonFromDb(CurrentItem.ID))
                {
                    AllPokemons.Remove(CurrentItem);
                    PokemonImage = new BitmapImage();
                    RaisePropertyChanged(nameof(PokemonImage));
                    PokemonName = "";
                    RaisePropertyChanged(nameof(PokemonName));
                }
            }
        }
        /// <summary>
        /// Сохранение выбранного покемона
        /// </summary>
        private void SaveCurrentPokemon()
        {
            if (CurrentItem!=null)
            {
                DataWorker.SavePokemonInDb(CurrentItem, PokemonName, PokemonImage);
                AllPokemons = DataWorker.GetAllPokemons();
                RaisePropertyChanged(nameof(AllPokemons));
            }
        }
    }
}
