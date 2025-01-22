using UnityEngine;

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