using UnityEngine;
using UnityEngine.EventSystems;

public class MenuClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private GameObject menuOverlay;
    
    [SerializeField]
    private StartGameHandler startGameHandler;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        menuOverlay.SetActive(false);
        startGameHandler.StartGame();
    }
}
