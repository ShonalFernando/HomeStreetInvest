namespace HousingIntel.Services
{
    public class HouseIntelService
    {

        public void Predict()
        {
            //Load sample data
            var sampleData = new HouseIntelML.ModelInput()
            {
                Id = 1F,
                MSSubClass = 20F,
                MSZoning = @"RL",
                LotArea = 9600F,
                LotConfig = @"FR2",
                BldgType = @"1Fam",
                OverallCond = 8F,
                YearBuilt = 1976F,
                YearRemodAdd = 1976F,
                Exterior1st = @"MetalSd",
                BsmtFinSF2 = 0F,
                TotalBsmtSF = 1262F,
            };

            //Load model and predict output
            var result = HouseIntelML.Predict(sampleData);

        }
    }
}
