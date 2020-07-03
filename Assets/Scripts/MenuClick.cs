using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MenuClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private GameObject menuOverlay;

    private IStartGameHandler _startGameHandler;

    [Inject]
    public void Construct(IStartGameHandler startGameHandler)
    {
        _startGameHandler = startGameHandler;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        menuOverlay.SetActive(false);
        _startGameHandler.StartGame();
    }
}
