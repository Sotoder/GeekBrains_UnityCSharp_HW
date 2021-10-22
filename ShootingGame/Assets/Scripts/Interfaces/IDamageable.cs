using Model.ShootingGame;

public interface IDamageable
{
    public void TakingDamage(int damage);
    public void SwapingHP(int hpForSwap);

    public PlayerData PlayerData { get; }
}
