using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabManga
{
    public partial class Manga
    {
        public Manga()
        {
            AuthorsMangas = new HashSet<AuthorsManga>();
            ReadersMangas = new HashSet<ReadersManga>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Информация")]
        public string Info { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<AuthorsManga> AuthorsMangas { get; set; }
        public virtual ICollection<ReadersManga> ReadersMangas { get; set; }
    }
}
