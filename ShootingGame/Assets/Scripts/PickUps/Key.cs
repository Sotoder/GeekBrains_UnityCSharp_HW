using UnityEngine;

namespace Model.ShootingGame
{
    public class Key : PickUps
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                player.GetKey();
                Destroy(gameObject);
            }
        }
    }
}
