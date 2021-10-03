using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Model.ShootingGame
{
    public class HomeWorkClass
    {
        public HomeWorkClass()
        {
            Task1();
            Task2And3();
            Task4a();
            Task4b();
        }

        private void Task1()
        {
            int count = "тут сколько то символов".CharCount();
            Debug.Log(count);
        }

        private void Task2And3()
        {
            int[] intArray = new int[10];
            System.Random rnd = new System.Random();

            string listStr = "";


            for (int i = 0; i < intArray.Length; i++)
            {
                intArray[i] = rnd.Next(1, 5);
                listStr += $" {intArray[i]}";
            }

            Debug.Log($"Список: {listStr}");

            var arrayInfo = intArray.DuplicateValues();

            Debug.Log(arrayInfo.Item2);
            Debug.Log($"Уникальных элементов: {intArray.Length - arrayInfo.Item1}.\nПовторяющих элементов: {arrayInfo.Item1}");

            Debug.Log(intArray.DuplicateValuesLinq()); //То же самое, но линком

            List<string> strList = new List<string>
            {
                "нож",
                "автомат",
                "граната",
                "граната",
                "пистолет",
                "нож",
                "патрон"
            };

            listStr = "";


            for (int i = 0; i < strList.Count; i++)
            {
                listStr += $" {strList[i]}";
            }

            Debug.Log($"Список: {listStr}");

            var listInfo = strList.DuplicateValues();

            Debug.Log(listInfo.Item2);
            Debug.Log($"Уникальных элементов: {strList.Count - listInfo.Item1}.\nПовторяющих элементов: {listInfo.Item1}");

            Debug.Log(intArray.DuplicateValuesLinq());
        }

        private void Task4a()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };

            //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            var d = dict.OrderBy(pair => pair.Value);

            foreach (var pair in d)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }

        }

        public delegate int PairValue(KeyValuePair<string, int> pair);    
        
        private void Task4b()
        {
            int GetPairValue(KeyValuePair<string, int> pair) => pair.Value;

            PairValue pVal;

            pVal = GetPairValue;

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };

            //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            var d = dict.OrderBy(pVal.Invoke);

            foreach (var pair in d)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }

            Func<KeyValuePair<string, int>, int> funcPVal;
            funcPVal = GetPairValue;

            var d1 = dict.OrderBy(funcPVal); // ну или так

            foreach (var pair in d1)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }

        }
    }
}