using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class SpeedBonus : PickUps
    {
        [SerializeField] private int _bonusValue;

        protected override void OnTriggerEnter(Collider other)
        {
            //if (other.TryGetComponent<Player>(out Player player))
            //{
            //    player.GetBuffOrDebuff(BuffTypes.Speed, _bonusValue, 3);
            //    Destroy(gameObject);
            //}
        }
    }
}
