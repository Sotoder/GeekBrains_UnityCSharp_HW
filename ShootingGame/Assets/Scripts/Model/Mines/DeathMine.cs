
namespace Model.ShootingGame
{
    public class DeathMine : Mine
    {
        protected override void InflictDamage(IDamageable target)
        {
            int damage = target.MaxHP;
            target.TakeDamage(damage);
        }
    }
}
