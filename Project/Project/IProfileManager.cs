using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    interface IProfileManager
    {
        public static void InsertIntoProfile<T>(ArrayList profile, T info)
        {
            profile.Add(info);
            Logger.Logger.Loging($"Information {info} added to {profile}.");
        }
        
        public static ArrayList UpdateProfile<T>(ArrayList profile, T info)
        {
            int index = profile.IndexOf(info);
            profile[index] = info;
            Logger.Logger.Loging($"Information {info} updated in {profile}.");
            return profile;
        }

    }
}
