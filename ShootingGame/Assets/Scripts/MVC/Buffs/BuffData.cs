using UnityEngine;


namespace Model.ShootingGame
{
    [CreateAssetMenu(fileName = "BuffData", menuName = "Data/Buff")]
    public sealed class BuffData : ScriptableObject
    {
        public BuffStructure buffStructure;
    }
}
