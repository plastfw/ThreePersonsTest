using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
  [SerializeField] private TMP_Text _healthField;

  public void SetTextField(int value)
  {
    _healthField.text = $"{value} healths";
  }
}