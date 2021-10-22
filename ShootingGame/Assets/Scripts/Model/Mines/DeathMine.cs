
namespace Model.ShootingGame
{
    public class DeathMine : Mine
    {
        protected override void InflictDamage(IDamageable target)
        {
            int damage = target.PlayerData.Parameters.currentHP;
            target.TakeDamage(damage);
        }
    }
}
