using System;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class ViewArea : MonoBehaviour
    {
        public event Action<Transform> SeePlayer;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerModel playerModel))
                SeePlayer?.Invoke(playerModel.transform);
        }
    }
}