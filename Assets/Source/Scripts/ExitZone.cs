using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts
{
    public class ExitZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerModel playerModel))
            {
                playerModel.StayInSafe();
            }
        }
    }
}