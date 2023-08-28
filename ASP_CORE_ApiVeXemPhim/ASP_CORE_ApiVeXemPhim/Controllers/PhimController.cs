using ASP_CORE_ApiVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_CORE_ApiVeXemPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        VePhimContext da = new VePhimContext();
        [HttpGet("get all Phim")]
        public IActionResult GetAllPhim()
        {
            var ds = da.Phims.ToList();
            return Ok(ds);
        }


        [HttpGet("get phim by id")]
        public IActionResult GetPhimById(int id)
        {
            var ds = da.Phims.FirstOrDefault(s => s.MaPhim == id);
            return Ok(ds);
        }
        [HttpPost("add new user")]
        public void AddPhim([FromBody] Phimlittle u1)
        {

            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    Phim u = new Phim();
                    u.MaPhim = u1.MaPhim;
                    u.TenPhim = u1.TenPhim;
                    u.ImgPhim = u1.ImgPhim;
                    u.DaoDien = u1.DaoDien;
                    u.Mota = u1.Mota;

                    da.Phims.Add(u);
                    da.SaveChanges();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }

        [HttpPut("Edit a Phim")]
        public void EditPhim([FromBody] Phimlittle u1)
        {

            Phim u = da.Phims.FirstOrDefault(s => s.MaPhim == u1.MaPhim);
            u.TenPhim = u1.TenPhim;
            u.ImgPhim = u1.ImgPhim;
            u.DaoDien = u1.DaoDien;

            da.Phims.Update(u);
            da.SaveChanges();
        }
        [HttpDelete("Delete a phim")]
        public void DeletePhim(int id)
        {

            Phim u = da.Phims.FirstOrDefault(s => s.MaPhim == id);


            da.Phims.Remove(u);
            da.SaveChanges();
        }

        private object SearchPhims(SearchPhimReq searchPhimReq)
        {
            // tim từ khóa  lấy  dss
            var phims = da.Phims.Where(x => x.TenPhim.Contains(searchPhimReq.Keyword));
            // xu li phân trang
            var offset = (searchPhimReq.Page - 1) * searchPhimReq.Size;
            var total = phims.Count();
            int totalPage = (total % searchPhimReq.Size) == 0 ? (int)(total / searchPhimReq.Size) :
                (int)(1 + (total / searchPhimReq.Size));

            var data = phims.OrderBy(x => x.MaPhim).Skip(offset).Take(searchPhimReq.Size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = searchPhimReq.Page,
                Size = searchPhimReq.Size,
            };
            return res;

        }

        [HttpPost("Search  phims")]
        public IActionResult SearchPhim([FromBody] SearchPhimReq searchPhimReq )
        {

            var ds = SearchPhims(searchPhimReq);
            return Ok(ds);
        }

        // Thông kê  sô lượng  User theo từng 

    }
}
