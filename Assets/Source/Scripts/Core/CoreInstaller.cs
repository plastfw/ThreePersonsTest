using Reflex.Core;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder
                .AddSingleton(_mainMenuView)
                .AddSingleton(typeof(MainMenuModel))
                .AddSingleton(typeof(MainMenuPresenter));
        }
    }
}