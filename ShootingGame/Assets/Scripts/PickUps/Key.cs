using UnityEngine;

namespace Model.ShootingGame
{
    public class Key : PickUps
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().GetKey();
                Destroy(gameObject);
            }
        }
    }
}
