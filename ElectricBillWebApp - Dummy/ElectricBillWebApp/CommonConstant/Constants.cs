using ElectricBillWebApp.Models.Pay;

namespace ElectricBillWebApp.CommonConstant
{
    public class Constants
    {
        public static List<PayMethod> PayMethod = new List<PayMethod>() { 
            new PayMethod(){ Id = 0, PayMethodName = "Cash" },
            new PayMethod(){ Id = 1, PayMethodName = "KPay"} ,
            new PayMethod(){ Id = 2, PayMethodName = "KBZ Bank"},
            new PayMethod(){ Id = 3, PayMethodName = "CB Bank"},
            new PayMethod(){ Id = 4, PayMethodName = "AYA Pay"},
            new PayMethod(){ Id = 5, PayMethodName = "AYA Bank" }
        };
    }
}
