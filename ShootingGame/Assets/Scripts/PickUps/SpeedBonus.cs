using UnityEngine;

namespace Model.ShootingGame
{
    public class SpeedBonus : PickUps
    {
        [SerializeField] private int _bonusValue;
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().GetBuffOrDebuff(Unit.BuffsAndDebuffs.Speed, _bonusValue, 3);
                Destroy(gameObject);
            }
        }

    }
}
