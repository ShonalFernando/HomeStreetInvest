using HomeStreetInvest.HousingIntel.Services;
using HomeStreetInvest.Model;

namespace HousingIntel.Services
{
    public class HouseIntelService
    {
        LogService _LogService;

        public HouseIntelService(LogService logService)
        {
            _LogService = logService;
        }

        public float Predict(HousePrice HousePrice)
        {
            try
            {
                var houseData = new HouseIntelML.ModelInput()
                {
                    Id = 0F,
                    MSSubClass = HousePrice.mSSubClass,
                    MSZoning = HousePrice.mSZoning.ToString(),
                    LotArea = HousePrice.lotArea,
                    LotConfig = HousePrice.lotConfig.ToString(),
                    BldgType = HousePrice.bldgType.ToString(),
                    OverallCond = HousePrice.overallCond,
                    YearBuilt = HousePrice.yearBuilt,
                    YearRemodAdd = HousePrice.yearRemodAdd,
                    Exterior1st = HousePrice.exterior1st.ToString(),
                    BsmtFinSF2 = HousePrice.bsmtFinSF2,
                    TotalBsmtSF = HousePrice.totalBsmtSF
                };                
                
                switch(HousePrice.bldgType)
                {
                    case BldgType.OneFam:
                        houseData.BldgType = "1Fam";
                        break;
                    case BldgType.TwofmCon:
                        houseData.BldgType = "2fmCon";
                        break;
                }

                if(HousePrice.exterior1st == Exterior1st.Wd_Sdng)
                {
                    houseData.Exterior1st = "Wd Sdng";
                }

                if (HousePrice.mSZoning == MSZoning.C)
                {
                    houseData.MSZoning= "C (all)";
                }

                var result = HouseIntelML.Predict(houseData);
                return result.Score;
            }
            catch (Exception IntellExc)
            {
                if(IntellExc.InnerException != null)
                {
                    _LogService.Log(IntellExc.InnerException.Message, LogWarning.Error);
                }
                else
                {
                    _LogService.Log(IntellExc.Message, LogWarning.Error);
                }

                return -1;
            }
        }
    }
}
