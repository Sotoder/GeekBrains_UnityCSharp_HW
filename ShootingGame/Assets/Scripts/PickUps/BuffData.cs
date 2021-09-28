using UnityEngine;


namespace Model.ShootingGame
{
    [CreateAssetMenu(fileName = "BuffData", menuName = "Data/Buff")]
    public class BuffData : ScriptableObject
    {
        public BuffStructure buffStructure;
    }
}
