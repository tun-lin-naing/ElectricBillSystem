
using ElectricBillWebApp.Models.User;

namespace ElectricBillWebApp.Interface
{
    public interface ILoginRepository
    {
        public ResUserLogin Login(ReqUserLogin userLogin);
    }
}
