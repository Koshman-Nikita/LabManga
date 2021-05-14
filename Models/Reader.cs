using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabManga
{
    public partial class Reader
    {
        public Reader()
        {
            ReadersMangas = new HashSet<ReadersManga>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Информация")]
        public string Info { get; set; }

        public virtual ICollection<ReadersManga> ReadersMangas { get; set; }
    }
}
