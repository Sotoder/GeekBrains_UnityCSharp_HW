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
            strResult += $"��������: {element.Key}, ���������: {element.Value}\n";
        }

        return (duplicateCount, strResult);
    }

    public static (int, string) DuplicateValues<T>(this T[] self) // ���������� ��� ��������
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
            strResult += $"��������: {element.Key}, ���������: {element.Value}\n";
        }

        return (duplicateCount, strResult);
    }

    public static string DuplicateValuesLinq<T>(this T[] self) // ������
    {
        string strResult = "Linq ��������\n";

        //var groupList = from element in self
        //                group self by element into g
        //                orderby g.Key ascending
        //                select g;

        var groupList = self.GroupBy(element => element).OrderBy(g => g.Key);

        foreach (var element in groupList)
        {
            strResult += $"��������: {element.Key}, ���������: {element.Count()}\n";
        }

        return strResult;
    }
}
