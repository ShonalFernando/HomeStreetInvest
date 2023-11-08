using HomeStreetInvest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homsin.Service
{
    //House Intel API Consumer Service

    public class HouseIntelService
    {
        public HousePrice housePrice { get; set; } = new HousePrice ();
        public int currentWizardPage { get; set; }

        //Also Get Preferences from dictionary to map Currency , etc Later

        public void GetPrediction()
        {
            throw new NotImplementedException();
        }

        public void DestroySessionData()
        {
            housePrice.bsmtFinSF2 = 0;
            housePrice.id = 0;
            housePrice.yearBuilt= 0;
            housePrice.totalBsmtSF = 0;
            housePrice.lotArea = 0;
            housePrice.mSSubClass= 0;
            housePrice.yearRemodAdd = 0;
            housePrice.overallCond= 0;
            currentWizardPage =0;
        }

        public enum WizardPage
        {
            Zoning,
            Location,
            Category, 
            Exterior,  
            Numericals,            
            Results
        }
    }
}
