using System;
using System.Collections.Generic;

#nullable disable

namespace ASP_CORE_ApiVeXemPhim.Models
{
    public partial class Phim
    {
        public Phim()
        {
            Ves = new HashSet<Ve>();
        }

        public int MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string ImgPhim { get; set; }
        public string Mota { get; set; }
        public string DaoDien { get; set; }

        public virtual ICollection<Ve> Ves { get; set; }
    }
}
