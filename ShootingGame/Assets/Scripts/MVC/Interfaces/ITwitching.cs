using UnityEngine;

namespace Model.ShootingGame
{
    internal interface ITwitching
    {
        void TwitchBuf(GameObject buffObject, float baseY, float fixedTime);
    }
}