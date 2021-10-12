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
            var saveString = JsonUtility.ToJson(data);
            File.WriteAllText(path, Encrypt(saveString));
        }

        private string Encrypt(string saveString)
        {
            List<Byte[]> bytes = new List<byte[]>();
            for (int i = 0; i < saveString.Length; i++)
            {
                bytes.Add(BitConverter.GetBytes(saveString[i]));
            }

            string encryptString = "";
            for (int i = 0; i < bytes.Count; i++)
            {
                if (i < bytes.Count * 0.3f)
                {
                    bytes[i][1] = (byte)~(bytes[i][1] + 20);
                    encryptString = encryptString + BitConverter.ToChar(bytes[i], 0);
                }
                else if(i >= bytes.Count * 0.3f && i < bytes.Count * 0.6f)
                {
                    bytes[i][1] = (byte)~(~(bytes[i][1]-10) + 2);
                    encryptString = encryptString + BitConverter.ToChar(bytes[i], 0);
                }
                else
                {
                    bytes[i][1] = (byte)(~bytes[i][1] - 12);
                    encryptString = encryptString + BitConverter.ToChar(bytes[i], 0);
                }
            }

            return encryptString;
        }

        private string Decrypt(string encryptedString)
        {
            List<Byte[]> bytes = new List<byte[]>();
            for (int i = 0; i < encryptedString.Length; i++)
            {
                bytes.Add(BitConverter.GetBytes(encryptedString[i]));
            }

            string decryptedString = "";
            for (int i = 0; i < bytes.Count; i++)
            {
                if (i < bytes.Count * 0.3f)
                {
                    bytes[i][1] = (byte)(~bytes[i][1] - 20);
                    decryptedString = decryptedString + BitConverter.ToChar(bytes[i], 0);
                }
                else if (i >= bytes.Count * 0.3f && i < bytes.Count * 0.6f)
                {
                    bytes[i][1] = (byte)(~(~bytes[i][1] - 2) + 10);
                    decryptedString = decryptedString + BitConverter.ToChar(bytes[i], 0);
                }
                else
                {
                    bytes[i][1] = (byte)~(bytes[i][1] + 12);
                    decryptedString = decryptedString + BitConverter.ToChar(bytes[i], 0);
                }
            }

            return decryptedString;
        }

        public T Load(string path = null)
        {
            var loadString = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(Decrypt(loadString));
        }
    }
}
