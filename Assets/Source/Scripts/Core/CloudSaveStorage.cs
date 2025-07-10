using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.SaveTypes;
using Unity.Services.CloudSave;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class CloudSaveStorage : ISaveStorage
    {
        private const string Key = "Cloud";

        public async UniTask Save(SavesData data)
        {
            try
            {
                var json = JsonUtility.ToJson(data);
                var dict = new Dictionary<string, object> { { Key, json } };
                await CloudSaveService.Instance.Data.Player.SaveAsync(dict);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Cloud save failed: {e}");
            }
        }

        public async UniTask<SavesData> Load()
        {
            try
            {
                var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { Key });
                if (result.TryGetValue(Key, out var cloudJson))
                    return JsonUtility.FromJson<SavesData>(cloudJson.Value.GetAs<string>());
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Cloud load failed: {e}");
            }

            return null;
        }
    }
}