

using UnityEngine.Events;

namespace Model.ShootingGame
{
    public interface IBuff
    {
        public UnityAction<BuffStructure> BuffCollected { get; set; }
        public BuffStructure Buff { get; }

    }
}