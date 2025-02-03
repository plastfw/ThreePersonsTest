using TMPro;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthField;

        public void SetTextField(int value)
        {
            _healthField.text = $"{value} healths";
        }
    }
}