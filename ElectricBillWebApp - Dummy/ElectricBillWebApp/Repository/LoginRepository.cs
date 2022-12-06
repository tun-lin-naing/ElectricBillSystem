using ElectricBillWebApp.Components;
using ElectricBillWebApp.Data;
using ElectricBillWebApp.Interface;
using ElectricBillWebApp.Models.Entities;
using ElectricBillWebApp.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace ElectricBillWebApp.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly EBSDBContext dbContext;

        public LoginRepository(EBSDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ResUserLogin? Login(ReqUserLogin userLogin)
        {
            #region Check with DB for LogIn

            try
            {
                string tempHashPw = GlobalFunction.CreateMD5Hash(userLogin.Password);               

                ResUserLogin? resUserLogin = new ResUserLogin();
                TblUser? tblUser= dbContext.TblUsers.Where(w => w.Email == userLogin.Email && w.Password == tempHashPw).FirstOrDefault();

                if (tblUser != null)
                {
                    resUserLogin.Name = tblUser.Name;
                    resUserLogin.Email = tblUser.Email;                    
                    resUserLogin.Id = tblUser.Id;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                return resUserLogin;

            }
            catch (Exception exc)
            {
                return null;
            }

            #endregion
        }
    }
}
