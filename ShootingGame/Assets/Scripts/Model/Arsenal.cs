using UnityEngine;

namespace Model.ShootingGame
{
    [System.Serializable]
    public struct Arsenal
    {
        public string name;
        public GameObject rightGun;
        public RuntimeAnimatorController controller;
    }
}
