using Source.Scripts.Core;

namespace Source.Scripts.Enemies
{
    public interface IEnemy
    {
        void Construct(BulletPool pool = null);
    }
}