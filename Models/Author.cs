using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabManga
{
    public partial class Author
    {
        public Author()
        {
            AuthorsMangas = new HashSet<AuthorsManga>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Автор")]
        public string Name { get; set; }
        [Display(Name = "Информация")]
        public string Info { get; set; }

        public virtual ICollection<AuthorsManga> AuthorsMangas { get; set; }
    }
}
