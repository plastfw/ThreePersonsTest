public interface IGameUpdateListener
{
    void OnUpdate();
}

public interface IGamePauseListener
{
    void OnPause();
}

public interface IGameResumeListener
{
    void OnResume();
}

public interface IGameStartListener
{
    void OnStart();
}

public interface IGameDisposeListener
{
    void OnDispose();
}

public interface IGameListener
{
}