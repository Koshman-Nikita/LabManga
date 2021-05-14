using System;
using System.Collections.Generic;

#nullable disable

namespace LabManga
{
    public partial class AuthorsManga
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Manga Manga { get; set; }
    }
}
