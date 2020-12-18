using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split_WPF.Helper
{
    class HelperFunctions
    {
        public static string RemovedUTF(string input)
        {

            int checkContainsInReplaceString(char input_char)
            {
                string toReplace = "ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýđð";
                for (int i = 0; i < toReplace.Length; ++i)
                {
                    if (toReplace[i] == input_char)
                    {
                        return i;
                    }
                }
                return -1;
            }
            string result = input;

            string toMatch = "aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyydd";

            for (int i = 0; i < result.Length; ++i)
            {
                int checkValue = checkContainsInReplaceString(result[i]);
                if (checkValue != -1)
                {
                    result = result.Replace(result[i], toMatch[checkValue]);
                }
            }

            return result;
        }

        public static int rateSearchResult(string searchText, string matchedText)
        {
            // longer match => higher rate
            int result = searchText.Length;
            string removedUTFSearchText = HelperFunctions.RemovedUTF(searchText);
            string removedUTFMatchedText = HelperFunctions.RemovedUTF(matchedText);




            int indexOfMatch = matchedText.IndexOf(searchText);
            if(indexOfMatch < 0 )
            {
                indexOfMatch = HelperFunctions.RemovedUTF(matchedText).IndexOf(HelperFunctions.RemovedUTF(searchText)); 
            }    

            // match at the beginning => higher rate
            result -= indexOfMatch;

            // match with better Vietnamse symbol => higher rate

            for (int i = 0; i < searchText.Length; ++i)
            {
                if (indexOfMatch >= 0  && searchText[i] == matchedText[indexOfMatch + i])
                {
                    result++;
                }
            }

            //string separator = " ";
            //string[] searchTextTokens = searchText.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            //string[] matchedTextTokens = matchedText.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);




            return result;
        }
        public static bool isNumericString(string input_string)
        {
            bool result = true;
            for (int i = 0; i < input_string.Length; i++)
            {
                if (input_string[i] < '0' || input_string[i] > '9')
                {
                    result = false;
                }
            }
            return result;
        }


    }
}
