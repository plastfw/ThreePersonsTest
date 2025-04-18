using UnityEngine;

namespace Source.Scripts.Enemies.Data
{
  [CreateAssetMenu(fileName = "FOVData", menuName = "ScriptableObjects/FOVData", order = 1)]
  public class FOVData : ScriptableObject
  {
    public int Damage;
    public float Speed;
  }
}