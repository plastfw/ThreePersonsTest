using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromInstance(_mainMenuView).AsSingle().NonLazy();
            Container.Bind<MainMenuModel>().AsSingle().NonLazy();
            Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
        }
    }
}