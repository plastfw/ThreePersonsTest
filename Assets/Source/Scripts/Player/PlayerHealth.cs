using System;

public class PlayerHealth
{
  private int _health;

  public int Health => _health;

  public PlayerHealth(int health)
  {
    _health = health;
  }

  public void SetHealth(int health) => _health = health;

  public void TakeDamage(int damage)
  {
    _health -= damage;
  }
}