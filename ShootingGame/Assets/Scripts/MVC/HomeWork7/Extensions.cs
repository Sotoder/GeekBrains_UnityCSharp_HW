using System.Collections.Generic;
using System.Linq;

static public class Extensions
{
    public static int CharCount(this string self)
    {
        return self.Length;
    }

    public static int ElementsCount<T>(this List<T> self)
    {
        return self.Count;
    }

    public static int ElementsCount<T>(this T[] self)
    {
        return self.Length;
    }

    public static (int, string) DuplicateValues<T>(this List<T> self)
    {
        Dictionary<T, int> RepitsCount = new Dictionary<T, int>();
        string strResult = "";
        int duplicateCount = 0;

        foreach (T element in self)
        {
            if (RepitsCount.ContainsKey(element))
            {
                RepitsCount[element]++;
                duplicateCount++;
            }
            else
            {
                RepitsCount.Add(element, 1);
            }
        }

        foreach (var element in RepitsCount)
        {
            strResult += $"Значение: {element.Key}, вхождений: {element.Value}\n";
        }

        return (duplicateCount, strResult);
    }

    public static (int, string) DuplicateValues<T>(this T[] self) // перегрузка для массивов
    {
        Dictionary<T, int> RepitsCount = new Dictionary<T, int>();
        string strResult = "";
        int duplicateCount = 0;

        foreach (T element in self)
        {
            if (RepitsCount.ContainsKey(element))
            {
                RepitsCount[element]++;
                duplicateCount++;
            }
            else
            {
                RepitsCount.Add(element, 1);
            }
        }

        foreach (var element in RepitsCount)
        {
            strResult += $"Значение: {element.Key}, вхождений: {element.Value}\n";
        }

        return (duplicateCount, strResult);
    }

    public static string DuplicateValuesLinq<T>(this T[] self) // Линком
    {
        string strResult = "Linq запросом\n";

        //var groupList = from element in self
        //                group self by element into g
        //                orderby g.Key ascending
        //                select new { g.Key, DuplicateCount = g.Count() };

        var groupList = self.GroupBy(element => element).Select(s => new {s.Key, DuplicateCount = s.Count()}).OrderBy(g => g.Key);

        foreach (var element in groupList)
        {
            strResult += $"Значение: {element.Key}, вхождений: {element.DuplicateCount}\n";
        }

        return strResult;
    }
}
