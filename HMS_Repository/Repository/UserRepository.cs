using HMS_DATAACCESS.Database;
using HMS_Repository.Interface;
using HMS_Repository.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly HMSDBDbContext _context;

        public UserRepository(HMSDBDbContext context)
        {
            _context = context;
        }


        public async Task<IList<UserResponse>> GetLoginMasters()
        {
            var data = await _context.LoginMasters.ToListAsync();
           return processdata(data);

        }

        public async Task<UserResponse> getuserInfoById(UserResponse userinfo)
        {
            var data = await _context.LoginMasters.Where(x=>x.Id==userinfo.Id).ToListAsync();
            return processdata(data)[0];

        }

        public async Task<IList<UserResponse>> ValidateLogin(Userlogin userinfo)
        {
            try
            {
                var data = await _context.LoginMasters.Where(u => u.Username == userinfo.Username && u.Password == userinfo.Password).ToListAsync();
                if (data.Count > 0)
                {
                    UserResponse empdat = new UserResponse() { Id = data[0].Id, Isactive = true };
                    loginstatuschange(empdat);
                    return processdata(data);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }

        public void loginstatuschange(UserResponse empdata)
        {
            var getmpdat = _context.LoginMasters.Where(u => u.Id == empdata.Id).FirstOrDefault();
            getmpdat.Isactive = empdata.Isactive ;
            _context.Entry(getmpdat).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
        public Task<bool> SaveAllAsync(LoginMaster userdata)
        {
            bool status = false;
            if (userdata.Id == 0)
            {
                userdata.JoiningDate = DateTime.Now;
                userdata.CreatedDate = DateTime.Now;
                _context.LoginMasters.Add(userdata);
                _context.SaveChangesAsync();
                status = true;
            }
            else
            {
                var getmpdat = _context.LoginMasters.Where(u => u.Id == userdata.Id).FirstOrDefault();
                if(getmpdat != null)
                {
                    getmpdat.EmailId = userdata.EmailId ?? getmpdat.EmailId;
                    getmpdat.Password = userdata.Password ?? getmpdat.Password;
                    getmpdat.Mobile = userdata.Mobile ?? getmpdat.Mobile;
                    getmpdat.Name = userdata.Name ?? getmpdat.Name;
                    getmpdat.Role = userdata.Role ?? getmpdat.Role;
                    getmpdat.Isactive = userdata.Isactive ?? getmpdat.Isactive;
                    _context.Entry(getmpdat).State = EntityState.Modified;
                    _context.SaveChangesAsync();
                    status = true;
                }
                else { status = false; }
                
            }
            return Task.Run(()=>status);

        }

        public IList<UserResponse> processdata(IList<LoginMaster> logindata)
        {
            return (from u in logindata
                    select new UserResponse
                    {
                        EmailId = u.EmailId,
                        Id = u.Id,
                        Isactive = u.Isactive,
                        Mobile = u.Mobile,
                        Name = u.Name,
                        Role = u.Role,
                        Username = u.Username
                    }).ToList();
        }
    }
}
