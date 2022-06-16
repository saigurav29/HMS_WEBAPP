using HMS_DATAACCESS.Database;
using HMS_Repository.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Repository.Interface
{
    public interface IUserRepository
    {
        Task<IList<UserResponse>> GetLoginMasters();
        Task<IList<UserResponse>> ValidateLogin(Userlogin userinfo);
        Task<bool> SaveAllAsync(LoginMaster userdata);
        void loginstatuschange(UserResponse empdata);
        Task<UserResponse> getuserInfoById(UserResponse userinfo);
    }
}
