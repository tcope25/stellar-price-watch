using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Stellar_Lumens_Price_Watch
{
    class GetLatestPrice
    {
        private const string API_URL = "https://stellarwidget.com/api/price/";

        string json = new WebClient().DownloadString(API_URL);
    }
}
