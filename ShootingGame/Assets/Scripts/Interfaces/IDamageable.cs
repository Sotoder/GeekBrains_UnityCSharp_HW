using Model.ShootingGame;

public interface IDamageable
{
    void TakeDamage(int damage);
    void SwapHP(int hpForSwap);

    public Parameters Parameters { get; }
    public int MaxHP { get; }
    public int MaxStamina { get; }
}
