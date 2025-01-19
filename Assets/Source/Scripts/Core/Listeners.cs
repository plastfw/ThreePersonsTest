public class Listeners
{
  public interface IGamePauseListener
  {
    void OnPause();
  }

  public interface IGameResumeListener
  {
    void OnResume();
  }


  public interface IGameListener
  {
    void OnPause();
    void OnResume();
  }
}