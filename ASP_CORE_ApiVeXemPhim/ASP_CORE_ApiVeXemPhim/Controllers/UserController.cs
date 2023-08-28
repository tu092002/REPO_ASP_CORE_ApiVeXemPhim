using ASP_CORE_ApiVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_CORE_ApiVeXemPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        VePhimContext da = new VePhimContext(); 
        [HttpGet ("get all users")]
        public IActionResult GetAllUsers()
        {
            var ds = da.Users.ToList();
            return Ok(ds);
        }


        [HttpGet ("get user by id")]
        public IActionResult GetUserById(int id)
        {
            var ds = da.Users.FirstOrDefault(s => s.MaUser == id);
            return Ok(ds);
        }
        [HttpPost("add new user")]
        public void AddUser ([FromBody] UserLittle u1)
        {

            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    User u = new User();
                    u.Username = u1.Username;
                    u.Password = u1.Password;
                    u.Role = u1.Role;

                    da.Users.Add(u);
                    da.SaveChanges();
                }
                catch (Exception)
                {
                    tran.Rollback();
                } 
            }
        }

        [HttpPut("Edit a user")]
        public void EditUser([FromBody] UserLittle u1)
        {

            User u = da.Users.FirstOrDefault(s => s.MaUser ==  u1.MaUser);
            u.Username = u1.Username;
            u.Password = u1.Password;
            u.Role = u1.Role;

            da.Users.Update(u);
            da.SaveChanges();
        }
        [HttpDelete("Delete a user")]
        public void EditUser(int id)
        {

            User u = da.Users.FirstOrDefault(s => s.MaUser == id);
            

            da.Users.Remove(u);
            da.SaveChanges();
        }

        private object SearchUsers(SearchUserReq searchUserReq)
        {
            // tim từ khóa  lấy  dss
            var users = da.Users.Where(x => x.Username.Contains(searchUserReq.Keyword));
            // xu li phân trang
            var offset = (searchUserReq.Page - 1) * searchUserReq.Size;
            var total = users.Count();
            int totalPage = (total % searchUserReq.Size) == 0 ? (int)(total / searchUserReq.Size) :
                (int)(1 + (total/searchUserReq.Size));

            var data = users.OrderBy(x => x.MaUser).Skip(offset).Take(searchUserReq.Size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = searchUserReq.Page,
                Size = searchUserReq.Size,
            };
            return res; 
                
        }

        [HttpPost("Search  users")]
        public IActionResult SearchUser([FromBody]  SearchUserReq searchUserReq)
        {

            var ds = SearchUsers(searchUserReq);
            return Ok(ds);
        }

        // Thông kê  sô lượng  User theo từng 











    }
}

