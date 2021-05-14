using System;
using System.Collections.Generic;

#nullable disable

namespace LabManga
{
    public partial class ReadersManga
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public int MangaId { get; set; }
        public int StatusId { get; set; }
        public DateTime PlanReturn { get; set; }
        public DateTime? FactReturn { get; set; }

        public virtual Manga Manga { get; set; }
        public virtual Reader Reader { get; set; }
        public virtual Status Status { get; set; }
    }
}
