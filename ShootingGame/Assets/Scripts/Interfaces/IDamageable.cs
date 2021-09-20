public interface IDamageable
{
    void TakeDamage(int damage);
    void SwapHP(int hpForSwap);
    public int MaxHP { get; }
    public int CurentHP { get; }
}
