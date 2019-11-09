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
                return false;
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
            DateTime expiry = (DateTime)profile[9];
            if (expiry > DateTime.Now) return true;
            else return false;
        }

        private static string GetBirthdayCode(ArrayList profile)
        {
            DateTime birthday = (DateTime)profile[2];
            int sex=0;
            if ((string)profile[10] == Constants.male) sex = Constants.maleCode;
            else if ((string)profile[10] == Constants.female) sex = Constants.femaleCode;
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
            string id = (string)profile[7];
            char[] idStartCode = new char[7];
            for (int i = 0; i < idStartCode.Length; i++)
            {
                idStartCode[i] = id[i];
            }
            return idStartCode.ToString();           
        }
    }
}
