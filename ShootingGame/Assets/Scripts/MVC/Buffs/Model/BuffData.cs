using UnityEngine;
using System;


namespace Model.ShootingGame
{
    [CreateAssetMenu(fileName = "BuffData", menuName = "Data/Buff")]
    [Serializable]
    public sealed class BuffData : ScriptableObject
    {
        [SerializeField] private BuffStructure _buffStructure;

        public BuffStructure BuffStruct { get => _buffStructure; }

        public void SetValue (int buffValue, int buffDuration, BuffTypes buffType)
        {
            _buffStructure.SetValue(buffValue, buffDuration, buffType);
        }
    }
}
