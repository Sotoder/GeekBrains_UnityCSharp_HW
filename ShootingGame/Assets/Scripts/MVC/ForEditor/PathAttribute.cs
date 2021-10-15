using System;
using UnityEngine;

namespace Model.ShootingGame
{
    [AttributeUsage(AttributeTargets.Field)]

    public sealed class PathAttribute : PropertyAttribute
    {
        public BuffData obj;
        public bool tgl;
    }
}
