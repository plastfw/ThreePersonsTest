public interface IGamePauseListener
{
    void OnPause();
}

public interface IGameResumeListener
{
    void OnResume();
}


public interface IGameUpdateListener
{
    void OnUpdate();
}

public interface IGameListener
{
    void OnPause();
    void OnResume();
}