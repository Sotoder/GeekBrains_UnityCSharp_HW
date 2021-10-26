using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct Weapon
    {
        public string name;
        public GameObject rightGun;
        public RuntimeAnimatorController controller;
    }
}
