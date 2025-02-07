namespace Source.Scripts.Player
{
    public class Health
    {
        public int value { get; private set; }

        public Health(int newValue)
        {
            value = newValue;
        }

        public void SetHealth(int health) => value = health;

        public void TakeDamage(int damage)
        {
            if (damage >= value)
                value = 0;
            else
                value -= damage;
        }
    }
}