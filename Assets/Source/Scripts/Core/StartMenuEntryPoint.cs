using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Factories;
using Source.Scripts.UI;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class StartMenuEntryPoint : IInitializable
    {
        private readonly MenuSystemFactory _menuFactory;
        private readonly FireBaseInitializer _firebaseInitializer;
        private MainMenuModel _model;
        private IAdsInitializer _adsInitializer;
        private IAPInitializer _iapInitializer;

        public StartMenuEntryPoint(MenuSystemFactory menuFactory, FireBaseInitializer firebaseInitializer,
            MainMenuModel model, IAdsInitializer adsInitializer, IAPInitializer iapInitializer)
        {
            _menuFactory = menuFactory;
            _firebaseInitializer = firebaseInitializer;
            _model = model;
            _adsInitializer = adsInitializer;
            _iapInitializer = iapInitializer;
        }

        public void Initialize()
        {
            Init().Forget();
        }

        private async UniTaskVoid Init()
        {
            try
            {
                await UnityServices.InitializeAsync().AsUniTask();
                await UniTask.WaitUntil(() => UnityServices.State == ServicesInitializationState.Initialized);
                _iapInitializer.InitializeIAP();
                await _menuFactory.Create();

#if !UNITY_EDITOR
            _adsInitializer.Init();

            if (!_adsInitializer.IsInitialized)
                await UniTask.WaitUntil(() => _adsInitializer.IsInitialized);
#endif

                _model.UpdateFirebaseStatus(_firebaseInitializer.IsInitialized);

                if (!_iapInitializer.IsInitialized)
                    await UniTask.WaitUntil(() => _iapInitializer.IsInitialized);

                if (!_firebaseInitializer.IsInitialized)
                {
                    await UniTask.WaitUntil(() => _firebaseInitializer.IsInitialized);
                    _model.UpdateFirebaseStatus(true);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Initialization failed: {e}");
            }
        }
    }
}