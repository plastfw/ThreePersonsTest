using Cysharp.Threading.Tasks;
using Source.Scripts.UI;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly HUDFactory _hudFactory;
        private readonly GameMenuFactory _menuFactory;

        public GameplayEntryPoint(HUDFactory factory, GameMenuFactory menuFactory)
        {
            _hudFactory = factory;
            _menuFactory = menuFactory;
        }

        public void Initialize() => Init().Forget();

        private async UniTaskVoid Init()
        {
            var hud = await _hudFactory.Create();
            var gameMenu = await _menuFactory.Create();
        }
    }
}