using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabManga
{
    public partial class Category
    {
        public Category()
        {
            Mangas = new HashSet<Manga>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Категория")]
        public string Name { get; set; }

        public virtual ICollection<Manga> Mangas { get; set; }
    }
}
