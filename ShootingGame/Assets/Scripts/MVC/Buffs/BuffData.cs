using UnityEngine;


namespace Model.ShootingGame
{
    [CreateAssetMenu(fileName = "BuffData", menuName = "Data/Buff")]
    public sealed class BuffData : ScriptableObject
    {
        [SerializeField] private BuffStructure _buffStructure;

        public BuffStructure BuffStruct { get => _buffStructure; }
    }
}
