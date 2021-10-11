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
    }
}
