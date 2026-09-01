using System.Text;
using System;
using System.Web;

namespace ViewModel.UtilityClasses
{
    public static class StringExtension
    {
        public static string Shuffle(this string str)
        {
            StringBuilder jumbleSB = new StringBuilder(str);
            int lengthSB = jumbleSB.Length;
            Random rand = new Random();
            for (int i = 0; i < lengthSB; ++i)
            {
                int index1 = (rand.Next() % lengthSB);
                int index2 = (rand.Next() % lengthSB);

                Char temp = jumbleSB[index1];
                jumbleSB[index1] = jumbleSB[index2];
                jumbleSB[index2] = temp;
            }
            return jumbleSB.ToString();
        }

        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

    }
}
