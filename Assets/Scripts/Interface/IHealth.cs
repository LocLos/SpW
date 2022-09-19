namespace Assets.Scripts
{
    public interface IHealth
    {
        int Health { get; }
        void Construct(int health);
        void TakeDamage(int damage);
    }
}