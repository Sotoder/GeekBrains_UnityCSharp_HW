using Model.ShootingGame;

public interface IDamageable
{
    public void TakeDamage(int damage);
    public void SwapHP(int hpForSwap);

    public Parameters Parameters { get; }
    public int MaxHP { get; }
    public int MaxStamina { get; }
}
