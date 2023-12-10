using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace News.Base.Extension
{
    public static class CardHelper
    {
        private readonly static string character = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string ClearCard(string card)
        {
            string result = string.Empty;
            foreach (var item in card.ToCharArray())
            {
                if(item.ToString().IndexOfAny(character.ToCharArray()) != -1)
                {
                    result = string.Format("{0}{1}", result, item.ToString());
                }
            }
            return result;
        }
        public static bool ValidateCard(string networkCode, string numberCard, string seriCard)
        {
            bool flag = false;
            bool checkFormatNetwork = CheckCodeSeriAllNetwork(numberCard, seriCard);
            switch (networkCode.ToLower())
            {
                case "mobifone":
                    if (numberCard.Length == 12 && seriCard.Length == 15 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "vietnamobile":
                    if (numberCard.Length == 12 && seriCard.Length == 16 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "vinaphone":
                    if (numberCard.Length == 14 && seriCard.Length == 14 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else if (numberCard.Length == 12 && seriCard.Length == 14 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "viettel":
                    if (numberCard.Length == 13 && seriCard.Length == 11 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else if (numberCard.Length == 15 && seriCard.Length == 14 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "zing":
                    if (numberCard.Length == 9 && seriCard.Length == 12)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "garena":
                    if (numberCard.Length == 16 && seriCard.Length == 9 && checkFormatNetwork)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "gate":
                    if (numberCard.Length == 10 && seriCard.Length == 10)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case "vcoin":
                    if (numberCard.Length == 12 && seriCard.Length == 12)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
            }
            return flag;
        }
        private static bool CheckCodeSeriAllNetwork(string card_code, string seri_card)
        {
            if (!Regex.IsMatch(card_code, "^[0-9]*$"))
            {
                return false;
            }
            if (!Regex.IsMatch(seri_card, "^[0-9]*$"))
            {
                return false;
            }
            return true;
        }
        public static bool StringContains(string target, string list)
        {
            return target.IndexOfAny(list.ToCharArray()) != -1;
        }
    }
}
