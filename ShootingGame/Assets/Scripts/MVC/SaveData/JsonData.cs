using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Model.ShootingGame
{
    public class JsonData<T> : IData<T>
    {
        public void Save(T data, string path = null)
        {
            var str = JsonUtility.ToJson(data);
            File.WriteAllText(path, Encrypt(str));
        }

        private string Encrypt(string str)
        {
            List<Byte[]> bytes = new List<byte[]>();
            for (int i = 0; i < str.Length; i++)
            {
                bytes.Add(BitConverter.GetBytes(str[i]));
            }

            string newStr = "";
            for (int i = 0; i < bytes.Count; i++)
            {
                bytes[i][1] = (byte)(bytes[i][1] - 1);
                newStr = newStr + BitConverter.ToChar(bytes[i], 0);
            }

            return newStr;
        }

        private string Decrypt(string str)
        {
            List<Byte[]> bytes = new List<byte[]>();
            for (int i = 0; i < str.Length; i++)
            {
                bytes.Add(BitConverter.GetBytes(str[i]));
            }

            string newStr = "";
            for (int i = 0; i < bytes.Count; i++)
            {
                bytes[i][1] = (byte)(bytes[i][1] + 1);
                newStr = newStr + BitConverter.ToChar(bytes[i], 0);
            }

            return newStr;
        }

        public T Load(string path = null)
        {
            var str = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(Decrypt(str));
        }
    }
}
