using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour, IPointerClickHandler
{
    private Rigidbody _rigidbody;
    private IGameOverHandler _gameOverHandler;
    private IBlockSlicer _blockSlicer;
    private MovingObject _movingObject;

    private bool _isAttached = false;

    [Inject]
    public void Construct(Vector3 startPoint, Vector3 finishPoint, IGameOverHandler gameOverHandler, IBlockSlicer blockSlicer, 
        Rigidbody rigidbody, MovingObject movingObject)
    {
        _gameOverHandler = gameOverHandler;
        _blockSlicer = blockSlicer;
        _rigidbody = rigidbody;
        _movingObject = movingObject;

        movingObject.Init(startPoint, finishPoint);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _movingObject.ShouldMove = false;
        _rigidbody.useGravity = true;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (_isAttached)
            return;
        
        if (other.gameObject.CompareTag(Tags.DestructionZone))
        {
            _gameOverHandler.GameOver();
            return;
        }

        _blockSlicer.Slice(transform, other.contacts);
        
        transform.SetParent(other.transform.parent, true);

        _isAttached = true;
        _rigidbody.isKinematic = true;
    }
    
    public class Factory : PlaceholderFactory<Vector3, Vector3, Block>
    {
    }
}
