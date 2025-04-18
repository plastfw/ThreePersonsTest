using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Factories
{
    public class PlayerModelsFactory
    {
        private readonly IAddressableLoader _loader;
        private readonly Transform _startPosition;

        private readonly List<PlayerModel> _models = new();

        public PlayerModelsFactory(IAddressableLoader loader, Transform startPosition)
        {
            _loader = loader;
            _startPosition = startPosition;
        }

        public async UniTask<List<PlayerModel>> Create()
        {
            _models.AddRange(await _loader.LoadPlayerModels(_startPosition));
            return _models;
        }
    }
}