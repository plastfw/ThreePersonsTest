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
        private IIAPService _iap;

        public StartMenuEntryPoint(
            MenuSystemFactory menuFactory,
            FireBaseInitializer firebaseInitializer,
            IAdsInitializer adsInitializer,
            IIAPService iap)
        {
            _menuFactory = menuFactory;
            _firebaseInitializer = firebaseInitializer;
            _adsInitializer = adsInitializer;
            _iap = iap;
        }

        public void Initialize()
        {
            Init().Forget();
        }

        private async UniTaskVoid Init()
        {
            try
            {
                await UniTask.WaitUntil(() => UnityServices.State == ServicesInitializationState.Initialized);
                _model = await _menuFactory.Create();

#if !UNITY_EDITOR
            _adsInitializer.Init();

            if (!_adsInitializer.IsInitialized)
                await UniTask.WaitUntil(() => _adsInitializer.IsInitialized);
#endif

                _model.UpdateFirebaseStatus(_firebaseInitializer.IsInitialized);
                _model.UpdateIAPStatus(_iap.IsInitialized);

                if (!_iap.IsInitialized)
                {
                    await UniTask.WaitUntil(() => _iap.IsInitialized);
                    UniTask.Delay(5000);
                    _model.UpdateIAPStatus(true);
                }

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