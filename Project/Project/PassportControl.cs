using Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Underwriter
{
    abstract class PassportControl
    {
        internal static bool ValidPassport(ArrayList profile)
        {
            if (ValidID(profile) && ValidExpiry(profile)) return true;
            else return false;
        }

        private static bool ValidID(ArrayList profile)
        {
            string birthdayCode = GetBirthdayCode(profile);
            string IdStartCode = GetIDStartCode(profile);
            if (birthdayCode.Equals(IdStartCode)) return true;
            else return false;
        }

        private static bool ValidExpiry(ArrayList profile)
        {
            DateTime expiry = (DateTime)SearchInProfilePassport(profile, (int)Field.Expiry);
            if (expiry > DateTime.Now) return true;
            else return false;
        }

        private static string GetBirthdayCode(ArrayList profile)
        {
            DateTime birthday = (DateTime)SearchInProfilePassport(profile, (int)Field.Bitrhday);
            int sex=0;
            if ((string)SearchInProfilePassport(profile, (int)Field.Sex) == Constants.Male) sex = Constants.MaleCode;
            else if ((string)SearchInProfilePassport(profile, (int)Field.Sex) == Constants.Female) sex = Constants.FemaleCode;
            else Console.WriteLine("Smth goes wrong!!");
            string birthdayCode = birthday.ToString();
            string[] code = birthdayCode.Split(' ');
            birthdayCode = code[0];
            code = birthdayCode.Split('.');
            string helper = code[2];
            code[2] = String.Concat(helper[2], helper[3]);
            return String.Concat(sex, code[0], code[1], code[2]);
        }

        private static string GetIDStartCode(ArrayList profile)
        {
            string id = (string)SearchInProfilePassport(profile, (int)Field.ID);
            char[] idStartCode = new char[7];
            for (int i = 0; i < idStartCode.Length; i++)
            {
                idStartCode[i] = id[i];
            }
            return String.Join("", idStartCode);           
        }

        public static void InsertIntoProfile<T>(ArrayList profile, T info)
        {
            profile.Add(info);
        }

        public static ArrayList UpdateProfile<T>(ArrayList profile, T info)
        {
            int index = profile.IndexOf(info);
            profile[index] = info;
            return profile;
        }

        private static object SearchInProfilePassport(ArrayList profile, int index)
        {
            Applicant applicant = (Applicant)profile[Constants.Applicant];
            Passport passport = applicant.Passport;
            return passport.GetInfo(index);
        }
    }
}
