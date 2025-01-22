using System;
using UnityEngine;

public class ViewArea : MonoBehaviour
{
    public event Action<Transform> SeePlayer;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerModel playerModel))
            SeePlayer?.Invoke(playerModel.transform);
    }
}