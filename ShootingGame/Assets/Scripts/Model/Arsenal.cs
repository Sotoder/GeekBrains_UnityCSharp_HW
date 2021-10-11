using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [Serializable]
    public struct Arsenal
    {
        public string name;
        public GameObject rightGun;
        public RuntimeAnimatorController controller;
    }
}
