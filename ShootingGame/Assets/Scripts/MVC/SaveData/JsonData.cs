using System.IO;
using UnityEngine;

namespace Model.ShootingGame
{
    public class JsonData<T> : IData<T>
    {
        private Encryptor _encryptor;
        public void Save(T data, string path = null)
        {
            var saveString = JsonUtility.ToJson(data);
            _encryptor = new Encryptor();
            File.WriteAllText(path, _encryptor.Encrypt(saveString));
        }

        public T Load(string path = null)
        {
            var loadString = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(_encryptor.Decrypt(loadString));
        }
    }
}
