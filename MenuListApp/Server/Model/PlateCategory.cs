using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class PlateCategory
    {
        public PlateCategory()
        {
            Plates = new HashSet<Plate>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? Rowversion { get; set; }

        public virtual ICollection<Plate> Plates { get; set; }
    }
}
