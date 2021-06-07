using System;

namespace CaculateShipFeeUsageDelegate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ShippingFeesDelegate theDel;
            ShippingDestination theDest;

            string theZone;
            do
            {
                // Lấy thông tin vận chuyển?
                Console.WriteLine("Bạn muốn vận chuyển tới đâu?");
                theZone = Console.ReadLine();

                // If nhập "exit" -> end program, ngược lại "continue"
                if (!theZone.Equals("exit"))
                {
                    // Lấy thông tin địa điểm giao hàng
                    theDest = ShippingDestination.GetDestinationInfo(theZone);

                    // Nếu địa điểm giao hàng không có hỗ trợ -> thông báo
                    // Ngược lại tiếp tục
                    if (theDest != null)
                    {
                        // Giá của hàng hóa bạn muốn vận chuyển
                        Console.WriteLine("Giá của hàng hóa bạn vận chuyển là bao nhiêu?");
                        string thePriceStr = Console.ReadLine();
                        decimal itemPrice = decimal.Parse(thePriceStr);

                        // Dùng delegate tính toán phí của địa điểm giao hàng
                        theDel = theDest.CalcFees;

                        // Đối với các khu vực rủi ro cao, tính thêm phí 25%
                        if (theDest.isHighRisk)
                        {
                            theDel += delegate (decimal thePrice, ref decimal itemFee)
                            {
                                // Charge more 25%
                                itemFee += 25.0m;
                            };
                        }

                        // Tính toán số tiền mà bạn phải trả cho mặt hàng
                        decimal theFee = 0.0m;
                        theDel(itemPrice, ref theFee);
                        Console.WriteLine("Giá ship của bạn là: {0}", theFee);
                    }
                    else
                    {
                        Console.WriteLine("Hmm, Chúng tôi không hỗ trợ điểm giao hàng. TVui lòng thử lại hoặc 'exit'");
                    }
                }
            } while (theZone != "exit");

        }
    }
}