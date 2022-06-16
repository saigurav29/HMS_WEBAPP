using HMS_DATAACCESS.Database;
using HMS_Repository.Interface;
using HMS_Repository.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        List<LoginMaster> em = new List<LoginMaster>();
        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetLoginMasters")]
        public async Task<IList<UserResponse>> GetLoginMasters()
        {
            return await _userRepository.GetLoginMasters();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IList<UserResponse>> ValidateLogin(Userlogin userlogin)
        {
            return await _userRepository.ValidateLogin(userlogin);
        }
        [HttpPost]
        [Route("SaveUser")]
        public async Task<bool> SaveEmploye(LoginMaster userinfo)
        {
            return await _userRepository.SaveAllAsync(userinfo);
        }

        [HttpPost]
        [Route("userloginStatuschange")]
        public void userloginStatuschange(UserResponse userinfo)
        {
              _userRepository.loginstatuschange(userinfo);
        }

        [HttpPost]
        [Route("getuserInfoById")]
        public async Task<UserResponse> getuserInfoById(UserResponse userinfo)
        {
           return await _userRepository.getuserInfoById(userinfo);
        }

    }
}
