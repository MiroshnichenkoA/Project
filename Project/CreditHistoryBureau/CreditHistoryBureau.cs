using System;
using System.Collections;

namespace CreditHistoryBureau
{
    public static class CreditHistoryBureau
    {
        static void Main(string[] args)
        {     
        }
        
       private static int _statementsCounter = 0;
       private static ArrayList _history = new ArrayList();

       public static int CreateStatement(ArrayList profile)
        {
            _statementsCounter += 1;
            _history.AddRange(profile);
            int applicantInternalID = _history.IndexOf(profile);
            return applicantInternalID;
        }
       public static double SearchCredits(int applicantInternalID)
        {
            ArrayList profile = (ArrayList)_history[applicantInternalID];
            if (profile != null) return (double)profile[];
            else return 0;
        }
    }
}
