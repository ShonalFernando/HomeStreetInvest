using Microsoft.Extensions.Logging.Abstractions;

namespace HomeStreetInvest.Model
{
    public class HousePrice
    {
        public float? id { get; set; }
        public float mSSubClass { get; set; }
        public MSZoning mSZoning { get; set; }
        public float lotArea { get; set; }
        public LotConfig lotConfig { get; set; }
        public BldgType bldgType { get; set; }
        public float overallCond { get; set; }
        public float yearBuilt { get; set; }
        public float yearRemodAdd { get; set; }
        public Exterior1st exterior1st { get; set; }
        public float bsmtFinSF2 { get; set; }
        public float totalBsmtSF { get; set; }
    }

    public enum BldgType
    {
        OneFam,
        TwofmCon,
        Duplex,
        Twnhs,
        TwnhsE
    }

    public enum LotConfig
    {
        Corner,
        CulDSac,
        FR2,
        FR3,
        Inside,

    }

    public enum MSZoning
    {
        C,
        FV,
        RH,
        RL,
        RM
    }


    public enum Exterior1st
    {
        AsbShng,
        AsphShn,
        BrkComm,
        BrkFace,
        CBlock,
        CemntBd,
        HdBoard,
        ImStucc,
        MetalSd,
        Plywood,
        Stone,
        Stucco,
        VinylSd,
        Wd_Sdng,
        WdShing,
    }
}
