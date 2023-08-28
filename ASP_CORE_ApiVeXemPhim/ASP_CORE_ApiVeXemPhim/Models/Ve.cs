using System;
using System.Collections.Generic;

#nullable disable

namespace ASP_CORE_ApiVeXemPhim.Models
{
    public partial class Ve
    {
        public int MaVe { get; set; }
        public int MaPhim { get; set; }
        public int MaRap { get; set; }
        public int MaGhe { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayXem { get; set; }
        public decimal GiaVe { get; set; }
        public int ThanhToan { get; set; }

        public virtual Phim MaPhimNavigation { get; set; }
    }
}
