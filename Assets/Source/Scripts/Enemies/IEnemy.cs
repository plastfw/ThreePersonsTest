using Source.Scripts.Core;

namespace Source.Scripts.Enemies
{
    public interface IEnemy
    {
        void Construct(GameStateManager manager, BulletPool pool = null);
    }
}