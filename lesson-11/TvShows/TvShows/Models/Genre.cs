using System.ComponentModel.DataAnnotations;

namespace TvShows.Models
{       
    public enum Genre
    {
        Drama,
        Comedy,
        Romance,
        [Display(Name = "Romantic Comedy")]
        RomCom,
        Crime,
        Mystery
    }
    
}
