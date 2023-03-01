using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class Menu
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Plate { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual Plate PlateNavigation { get; set; } = null!;
    }
}
