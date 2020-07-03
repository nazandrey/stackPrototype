using UnityEngine.SceneManagement;

public class GameOverHandler : IGameOverHandler
{
    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}