using Model.ShootingGame;

public interface IDamageable
{
    public void TakeDamage(int damage);
    public void SwapHP(int hpForSwap);

    public PlayerData PlayerData { get; }
}
