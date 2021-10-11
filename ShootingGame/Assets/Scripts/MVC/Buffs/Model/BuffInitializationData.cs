using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct BuffInitializationData
    {
        [SerializeField] private List<BuffObject> _buffObjectCollection;

        public List<BuffObject> BuffObjectCollection { get => _buffObjectCollection; }
    }
}
