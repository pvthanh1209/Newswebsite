using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Extension
{
    public static class Helper
    {
        public static string NumberFormat(decimal m)
        {
            return Math.Ceiling(m).ToString("N", new CultureInfo("vi-VN")).Replace(",00", "");
        }
        public static string RandomCode()
        {
            string base64Guid = Guid.NewGuid().ToString("N");
            var stringRanDom = base64Guid.Substring(0, 12);
            return stringRanDom.ToString();
        }
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
                stringBuilder.Replace(" ", "");
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
