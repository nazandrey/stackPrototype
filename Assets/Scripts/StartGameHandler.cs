using UnityEngine;

public class StartGameHandler : MonoBehaviour
{
    [SerializeField] 
    private BlockSpawner spawner;
    
    public void StartGame()
    {
        spawner.Spawn();
    }
}
