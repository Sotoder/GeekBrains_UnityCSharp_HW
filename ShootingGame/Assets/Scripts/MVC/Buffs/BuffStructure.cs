using UnityEngine;
using System;

namespace Model.ShootingGame
{
    [Serializable]
    public struct BuffStructure
    {
        [SerializeField] private BuffTypes _buffType;
        [Range(0,5)]
        [SerializeField] private int _buffValue;
        [Range(0, 10)]
        [SerializeField] private int _buffDuration;

        public int BonusValue { get => _buffValue; }
        public int BonusDuration { get => _buffDuration; }
        public BuffTypes BuffType { get => _buffType; }
    }
}