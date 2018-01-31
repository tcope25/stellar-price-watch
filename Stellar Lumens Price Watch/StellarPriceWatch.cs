using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading;

namespace Stellar_Lumens_Price_Watch
{
    public partial class StellarPriceWatch : Form
    {

        private const string API_URL = "https://api.coinmarketcap.com/v1/ticker/stellar/";

        public StellarPriceWatch()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Task timerTask = RunPeriodically(TimeSpan.FromMilliseconds(60000), tokenSource.Token);

        }

        private string GoGetPrice()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(API_URL));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }

        async Task RunPeriodically(TimeSpan interval, CancellationToken token)
        {
            

            while (true)
            {
                string jsonString = GoGetPrice();

                List<PriceObject> items = JsonConvert.DeserializeObject<List<PriceObject>>(jsonString);

                string price = items.Select(i => i.Price_usd).First().ToString();

                label1.Text = price;
               

                await Task.Delay(interval, token);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StellarPriceWatch_Load(object sender, EventArgs e)
        {

        }
    }

    public class PriceObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Rank { get; set; }
        public string Price_usd { get; set; }
        public string Price_btc { get; set; }
        public string Volume_24_usd { get; set; }
        public string Market_cap_usd { get; set; }
        public string Available_supply { get; set; }
        public string Total_supply { get; set; }
        public object Max_supply { get; set; }
        public string Percent_change_1h { get; set; }
        public string Percent_change_24h { get; set; }
        public string Percent_change_7d { get; set; }
        public string Last_updated { get; set; }
    }



}
