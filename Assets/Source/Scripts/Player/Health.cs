public class Health
{
    private int _healthValue;

    public int HealthValue => _healthValue;

    public Health(int healthValue)
    {
        _healthValue = healthValue;
    }

    public void SetHealth(int health) => _healthValue = health;

    public void TakeDamage(int damage)
    {
        if (damage >= _healthValue)
            _healthValue = 0;
        else
            _healthValue -= damage;
    }
}