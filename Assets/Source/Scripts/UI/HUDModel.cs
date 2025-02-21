public class HUDModel
{
    private HUDView _view;

    private int _health;
    private float _distance;
    private int _count;

    public HUDModel(HUDView view)
    {
        _view = view;
    }

    public void SetHealth(int value)
    {
        _health = value;
        _view.SetHealth(_health);
    }

    public void SetModelCount(int value)
    {
        _count = value;
        _view.SetCounter(_count);
    }

    public void SetDistance(float value)
    {
        _distance = value;
        _view.SetDistance(_distance);
    }
}