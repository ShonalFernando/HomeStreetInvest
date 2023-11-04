using System;
using System.Collections.Generic;
using System.Text;

namespace HomeStreetInvest.Model
{
    public class StockPredictionRequest
    {
        public int daterequested { get; set; }
        public Stock stock { get; set; }

    }

    public enum Stock
    {
        MSFT,
        TSLA,
        AAPL,
        GOOGL
    }
}
