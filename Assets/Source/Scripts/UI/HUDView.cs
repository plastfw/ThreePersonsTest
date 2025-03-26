using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _modelsCounterField;
        [SerializeField] private TMP_Text _distanceField;
        [SerializeField] private TMP_Text _healthField;

        public void SetCounter(int value) => _modelsCounterField.text = $"{value} models left";

        public void SetDistance(float value) => _distanceField.text = $"Distance: {value:F2}";

        public void SetHealth(int value) => _healthField.text = $"{value} healths";
    }
}