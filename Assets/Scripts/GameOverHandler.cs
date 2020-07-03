using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
