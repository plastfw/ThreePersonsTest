using Cinemachine;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cineMachineVirtual;

        public void SwitchFollowTarget(Transform model) => _cineMachineVirtual.m_Follow = model;
    }
}