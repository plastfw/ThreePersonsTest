using Cysharp.Threading.Tasks;
using Source.Scripts.SaveTypes;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class LocalSaveStorage : ISaveStorage
    {
        private const string Key = "Local";

        public UniTask Save(SavesData data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
            return UniTask.CompletedTask;
        }

        public UniTask<SavesData> Load()
        {
            if (!PlayerPrefs.HasKey(Key))
                return UniTask.FromResult(new SavesData());

            var json = PlayerPrefs.GetString(Key);
            return UniTask.FromResult(JsonUtility.FromJson<SavesData>(json));
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(Key);
        }
    }
}