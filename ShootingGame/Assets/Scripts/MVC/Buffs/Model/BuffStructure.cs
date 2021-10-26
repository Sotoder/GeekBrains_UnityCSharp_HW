using UnityEngine;
using System;

namespace Model.ShootingGame
{
    [Serializable]
    public struct BuffStructure
    {
        [SerializeField] private BuffTypes _buffType;
        [Range(1, 5)]
        [SerializeField] private int _buffValue;
        [Range(1, 10)]
        [SerializeField] private int _buffDuration;

        public int BonusValue { get => _buffValue; }
        public int BonusDuration { get => _buffDuration; }
        public BuffTypes BuffType { get => _buffType; }

        public void SetValue(int buffValue, int buffDuration, BuffTypes buffType)
        {
            _buffDuration = buffDuration;
            _buffValue = buffValue;
            _buffType = buffType;
        }
    }
}