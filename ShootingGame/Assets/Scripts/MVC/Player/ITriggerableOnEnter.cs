using UnityEngine;

namespace Model.ShootingGame
{
    public interface ITriggerableOnEnter
    {
        public void TriggerEnter(Collider collider);
    }
}