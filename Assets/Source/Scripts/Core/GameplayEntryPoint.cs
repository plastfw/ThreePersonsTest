using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Factories;
using Source.Scripts.Player;
using Source.Scripts.UI;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly HUDFactory _hudFactory;
        private readonly GameMenuFactory _menuFactory;
        private readonly PlayerModelsFactory _playerModelsFactory;
        private readonly SwitchModelObserver _observer;
        private readonly ExitZone _exit;
        private readonly SavesManager _saves;
        private readonly PlayerInput _playerInput;

        private List<PlayerModel> _playerModels;

        public GameplayEntryPoint(HUDFactory factory, GameMenuFactory menuFactory,
            PlayerModelsFactory playerModelsFactory, SwitchModelObserver observer, ExitZone exitZone,
            SavesManager saves, PlayerInput playerInput)
        {
            _hudFactory = factory;
            _menuFactory = menuFactory;
            _playerModelsFactory = playerModelsFactory;
            _observer = observer;
            _exit = exitZone;
            _saves = saves;
            _playerInput = playerInput;
        }

        public void Initialize() => Init().Forget();

        private async UniTaskVoid Init()
        {
            await _hudFactory.Create();
            await _menuFactory.Create();
            _playerModels = await _playerModelsFactory.Create();

            foreach (var model in _playerModels)
            {
                model.Construct(_exit, _saves);
                model.InitializeStats();
            }

            _observer.Construct(_playerModels);
            _playerInput.Construct(_playerModels);
        }
    }
}