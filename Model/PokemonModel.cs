using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pokemons.Model
{
    public class PokemonModel
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public  byte[] Image {  get; set; }
        [NotMapped] public BitmapImage? BitmapImage { get; set; }
    }
}
