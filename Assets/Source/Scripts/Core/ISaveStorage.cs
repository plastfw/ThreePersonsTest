using Cysharp.Threading.Tasks;
using Source.Scripts.SaveTypes;

namespace Source.Scripts.Core
{
    public interface ISaveStorage
    {
        UniTask Save(SavesData data);
        UniTask<SavesData> Load();
    }
}