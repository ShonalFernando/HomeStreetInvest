using HomeStreetInvest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homsin.Session
{
    public static class SessionDAO
    {
        public static WizardPage currentWizardPage { get; set; }
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
