namespace HousingIntel.Model
{
    public class HousePrice
    {
        public float? Id { get; set; }
        public float MSSubClass { get; set; }
        public string MSZoning { get; set; }
        public float LotArea { get; set; }
        public string LotConfig { get; set; }
        public string BldgType { get; set; }
        public float OverallCond { get; set; }
        public float YearBuilt { get; set; }
        public float YearRemodAdd { get; set; }
        public string Exterior1st { get; set; }
        public float BsmtFinSF2 { get; set; }
        public float TotalBsmtSF { get; set; }
    }
}
