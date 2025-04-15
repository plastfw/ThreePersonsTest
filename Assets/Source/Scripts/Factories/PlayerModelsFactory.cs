using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Factories
{
    public class PlayerModelsFactory
    {
        private const string SphereKey = "PlayerSphere";
        private const string CubeKey = "PlayerCube";

        private readonly IAddressableLoader _loader;
        private readonly Transform _startPosition;
        
        private List<PlayerModel> _models = new();

        public PlayerModelsFactory(IAddressableLoader loader, Transform startPosition)
        {
            _loader = loader;
            _startPosition = startPosition;
        }

        public async UniTask<List<PlayerModel>> Create()
        {
            var sphereModel = await _loader.LoadAssetAsync<PlayerModel>(SphereKey, _startPosition);
            var cubeModel = await _loader.LoadAssetAsync<PlayerModel>(CubeKey, _startPosition);

            _models.Add(sphereModel);
            _models.Add(cubeModel);
            
            return _models;
        }
    }
}