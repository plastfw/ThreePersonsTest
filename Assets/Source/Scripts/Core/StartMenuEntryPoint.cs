using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Analytics;
using Source.Scripts.Factories;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class StartMenuEntryPoint : IInitializable
    {
        private MenuSystemFactory _menuFactory;

        public StartMenuEntryPoint(MenuSystemFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Initialize() => Init().Forget();

        private async UniTaskVoid Init()
        {
            var menuSystem = await _menuFactory.Create();

            try
            {
                if (!FireBaseInitializer.IsInitialized)
                {
                    menuSystem.View.ChangeButtonState(false);
                    await UniTask.WaitUntil(() => FireBaseInitializer.IsInitialized);
                    menuSystem.View.ChangeButtonState(true);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Initialization failed: {e}");
            }
        }
    }
}