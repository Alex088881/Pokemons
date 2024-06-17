using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Pokemons.Model
{
    static class DataWorker
    {
        /// <summary>
        /// Получить всех покемонов с БД
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<PokemonModel> GetAllPokemons()
        {
            using ApplicationContext db = new ApplicationContext();
            var collection = new ObservableCollection<PokemonModel>();
            foreach (var item in db.PokemonModels)
            {
                PokemonModel model = new PokemonModel();
                model.Name = item.Name;
                model.ID=item.ID;
                model.BitmapImage = Converter.DecodingArrayToImage(item.Image);
                collection.Add(model);
            }
            return collection;
        }

        /// <summary>
        /// Удалить выбранного покемона с БД
        /// </summary>
        /// <param name="idOfPokemon"></param>
        /// <returns></returns>
        public static bool DeleteCurrentPokemonFromDb(int idOfPokemon)
        {
            try
            {
                using var db = new ApplicationContext();
                if (db.PokemonModels.Any(el => el.ID == idOfPokemon))
                {
                    db.PokemonModels.Where(i => i.ID == idOfPokemon).ExecuteDelete();
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        /// <summary>
        /// Сохранить покемона в БД
        /// </summary>
        /// <param name="pokemonView"></param>
        /// <param name="name"></param>
        /// <param name="img"></param>
        public static void SavePokemonInDb(PokemonModel pokemonView, string name, BitmapImage img)
        {
                using var db = new ApplicationContext();
                if (db.PokemonModels.Any(el => el.ID == pokemonView.ID))
                {
                    db.PokemonModels.Find(pokemonView.ID).Name = name;
                    db.PokemonModels.Find(pokemonView.ID).Image = Converter.
                                     EncodingImageToArray(img);
                    db.SaveChanges();

                }
        }
    }
}
