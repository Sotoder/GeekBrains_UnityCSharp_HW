using System;
using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class PlayerHPExeption : Exception
    {
        public PlayerHPExeption (string message, int value)
        {
            Debug.LogWarning($"{message} {value}");
        }
    }
}
