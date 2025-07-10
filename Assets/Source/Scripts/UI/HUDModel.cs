namespace Source.Scripts.UI
{
    public class HUDModel
    {
        private HUDView _view;

        private int _health;
        private float _distance;
        private int _count;

        public void Construct(HUDView view) => _view = view;

        public void SetHealth(int value) => _health = value;

        public void SetModelCount(int value) => _count = value;

        public void SetDistance(float value) => _distance = value;
    }
}