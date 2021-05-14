using System;
using System.Collections.Generic;

#nullable disable

namespace LabManga
{
    public partial class Status
    {
        public Status()
        {
            ReadersMangas = new HashSet<ReadersManga>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ReadersManga> ReadersMangas { get; set; }
    }
}
