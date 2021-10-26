using System;
using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class ObjecDuplicateExeption : Exception
    {
        public ObjecDuplicateExeption(string message) : base(message)
        {
            Debug.LogError($"{message}");
        }
    }
}