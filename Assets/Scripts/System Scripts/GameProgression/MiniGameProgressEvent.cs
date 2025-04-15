

public class MiniGameProgressEvent : BaseProgressEvent
{
     private BaseMiniGameManager MiniGameManager;

     public MiniGameProgressEvent(string name, string targetScene, ProgressEventType type) : base(name, targetScene, type) { }

     public MiniGameProgressEvent(ProgressEventsSO data) : base(data) { }

     ~MiniGameProgressEvent()
     {
          MiniGameManager.MiniGameComplete -= OnEventCompleted;
     }

     public void OnEventCompleted(int score)
     {
          CompleteProgressEvent(score);
     }
}
