using System;

namespace NunitTest
{
    //假定人民币转美元的接口定义如下
    public interface IUSD_RMB_ExchangeRateFeed
    {
         int GetActualUSDValue();
    }
}