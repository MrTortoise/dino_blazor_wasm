using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DinoGame.Blazor.Pages.survival
{
    public class CreateGameModel
    {
        [Required]
        [StringLength(32)]
        public string GameName { get; set; }

        [Required]
        [Range(1,20)]
        public int BoardSize { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberOfHerbivores { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberOfCarnivores { get; set; }
    }
}
