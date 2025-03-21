using System.Collections.Generic;
using Reflex.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Source.Scripts.Core
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private List<PlayerModel> _playerModels;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private GameStateManager _gameStateManager;
        [SerializeField] private ExitZone _exit;
        [SerializeField] private Button _pauseButton;
        [FormerlySerializedAs("_gameMenuView")] [SerializeField] private GameMenu _gameMenu;
        [SerializeField] private HUDView _hudView;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder
                .AddSingleton(_gameStateManager)
                .AddSingleton(_gameMenu)
                .AddSingleton(_exit)
                .AddSingleton(_playerModels)
                .AddSingleton(_cameraController)
                .AddSingleton(_bulletPool)
                .AddSingleton(_hudView)
                .AddSingleton(typeof(HUDModel))
                .AddSingleton(new TickableService())
                .AddSingleton(new PauseService(_pauseButton))
                .AddSingleton(typeof(HUDPresenter), typeof(HUDPresenter), typeof(IGameListener),
                    typeof(IGameStartListener), typeof(IGameDisposeListener))
                .AddSingleton(new StandardInputService(), typeof(StandardInputService),
                    typeof(IInputService))
                .AddSingleton(typeof(PlayerInput), typeof(PlayerInput),
                    typeof(IGameListener),
                    typeof(IGameUpdateListener),
                    typeof(IGamePauseListener))
                .AddSingleton(typeof(SwitchModelObserver), typeof(SwitchModelObserver),
                    typeof(IGameListener),
                    typeof(IGameStartListener),
                    typeof(IGameDisposeListener))
                .AddSingleton(typeof(CompleteLevelObserver), typeof(CompleteLevelObserver),
                    typeof(IGameListener),
                    typeof(IGameStartListener),
                    typeof(IGameDisposeListener))
                .OnContainerBuilt += DoNonLazy;
        }

        // Я почитал в дискорде у создателя рефлекса, он типо такого способоа советуюет. Аналог зенжектовского не ленивого биндинга.
        // До этого я резолвил тоже все это именно для этого. Но я по каличному делал.
        private void DoNonLazy(Container container)
        {
            container.Resolve<HUDPresenter>();
            container.Resolve<PlayerInput>();
            container.Resolve<CompleteLevelObserver>();
            container.Resolve<SwitchModelObserver>();
            container.Resolve<StandardInputService>();
        }
    }
}