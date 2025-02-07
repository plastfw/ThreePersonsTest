using UnityEngine;

[CreateAssetMenu(fileName = "ShootData", menuName = "ScriptableObjects/ShootData", order = 1)]
public class ShootData : ScriptableObject
{
  public int Damage;
  public float Radius;
}