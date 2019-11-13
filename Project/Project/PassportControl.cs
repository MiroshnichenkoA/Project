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
            if (ValidID(profile) && ValidExpiry(profile))
            {
                Logger.Logger.Loging($"Passport is valid");
                return true;
            }
            else
            {
                Logger.Logger.Loging($"Passport is not valid/ ID Validation is {ValidID(profile)}, Expiry validation is {ValidExpiry(profile)}");
                return false;
            }
        }

        private static bool ValidID(ArrayList profile)
        {
            string birthdayCode = GetBirthdayCode(profile);
            string IdStartCode = GetIDStartCode(profile);
            if (birthdayCode.Equals(IdStartCode))
            {
                birthdayCode = null;
                IdStartCode = null;
                return true;
            }
            else
            {
                birthdayCode = null;
                IdStartCode = null;
                return false;
            }
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
            try
            {
                string sexDefenition = (string)SearchInProfilePassport(profile, (int)Field.Sex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Method: {ex.TargetSite}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Logger.Logger.Loging($"Exception - {ex.Message}, Method: {ex.TargetSite}, Stack Trace: {ex.StackTrace}");

            }
            if ((string)SearchInProfilePassport(profile, (int)Field.Sex) == Constants.Male) sex = Constants.MaleCode;
            else if ((string)SearchInProfilePassport(profile, (int)Field.Sex) == Constants.Female) sex = Constants.FemaleCode;
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
            id = null;
            return String.Join("", idStartCode);           
        }

        private static object SearchInProfilePassport(ArrayList profile, int index)
        {
            Applicant applicant = (Applicant)profile[Constants.Applicant];
            Passport passport = applicant.Passport;
            applicant = null;
            return passport.GetInfo(index);
        }
    }
}
