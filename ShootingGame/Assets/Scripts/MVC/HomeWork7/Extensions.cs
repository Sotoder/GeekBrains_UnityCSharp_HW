using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.ShootingGame
{
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

        public static string GroupByValues<T>(this T[] self) // ������
        {
            string strResult = "Linq ��������\n";

            //var groupList = from element in self
            //                group self by element into g
            //                orderby g.Key ascending
            //                select new { g.Key, DuplicateCount = g.Count() };

            var groupList = self.GroupBy(element => element).Select(s => new { s.Key, DuplicateCount = s.Count() }).OrderBy(g => g.Key);

            foreach (var element in groupList)
            {
                strResult += $"��������: {element.Key}, ���������: {element.DuplicateCount}\n";
            }

            return strResult;
        }

        public static void CheckOnRepeats<T>(this List<T> self) where T : IUniqObjectCollectionElement
        {

            var isDuplicated = self.IsObjectDuplicated();
            if (isDuplicated.Item1)
            {
                throw new ObjecDuplicateExeption($"������ � InstanceID = {isDuplicated.Item2} ����������� � ��������� {self.GetType()} ����� 1 ����");
            }
        }

        public static (bool, int) IsObjectDuplicated<T>(this List<T> self) where T: IUniqObjectCollectionElement
        {

            var groupList = self.GroupBy(element => element.Object.GetInstanceID()).Select(s => new { s.Key, DuplicateCount = s.Count() }).OrderBy(g => g.Key);

            foreach (var element in groupList)
            {
                if (element.DuplicateCount > 1) return (true, element.Key);
            }

            return (false, 0);
        }

        public static void CheckOnParentRepeats(this Dictionary<GameObject, float> self, GameObject targetObject)
        {
            foreach (var element in self)
            {
                if (element.Key.transform.parent.GetInstanceID() == targetObject.transform.parent.GetInstanceID())
                {
                    throw new ObjecDuplicateExeption($"������ � InstanceID = {targetObject.GetInstanceID()} ��� ������������ � PickUpObjects ��������� {self.GetType()}");
                }
            }            
        }
    }
}
