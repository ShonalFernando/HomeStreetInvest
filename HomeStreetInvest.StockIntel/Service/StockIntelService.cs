using HomeStreetInvest.Model;
using HomeStreetInvest.StockIntel.Services;
using HomeStreetInvest_StockIntel;

namespace HomeStreetInvest.StockIntel.Service
{
    public class StockIntelService
    {
        LogService _LogService;

        public StockIntelService(LogService logService)
        {
            _LogService = logService;
        }

        public float[] Predict(int horizon, Stock stock)
        {
            // Load model and predict the next set values.
            // The number of values predicted is equal to the horizon specified while training.
            float[] Returnable;

            try
            {
                switch (stock)
                {
                    case Stock.AAPL:
                        AAPL.ModelOutput resultAAPL = AAPL.Predict(null, horizon);
                        Returnable = resultAAPL.Close;
                        break;
                    case Stock.TSLA:
                        // Load model and predict the next set of values.
                        // The number of values predicted is equal to the horizon specified while training.
                        var resultTSLA = TSLA.Predict(null, horizon);
                        Returnable = resultTSLA.Close;
                        break;
                    case Stock.MSFT:
                        // Load model and predict the next set of values.
                        // The number of values predicted is equal to the horizon specified while training.
                        var resultMSFT = MSFT.Predict(null, horizon);
                        Returnable = resultMSFT.Close;
                        break;
                    case Stock.GOOGL:
                        // Load model and predict the next set values.
                        // The number of values predicted is equal to the horizon specified while training.
                        var resultGOOGL = GOOGL.Predict(null, horizon);
                        Returnable = resultGOOGL.Close;
                        break;
                    default:
                        Returnable = new float[0]; // Handle an unknown stock or default case
                        break;
                }
            }
            catch (Exception SIException)
            {
                _LogService.Log(SIException.Message, LogWarning.Error);
                Returnable = new float[0];
            }
            return Returnable;
        }
    }
}
