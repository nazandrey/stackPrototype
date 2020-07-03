using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float speed;

    private Vector3 _startPoint;
    private Vector3 _finishPoint;
    private Rigidbody _rigidbody;
    private IGameOverHandler _gameOverHandler;

    private bool _isMoving = false;
    private bool _isAttached = false;

    [Inject]
    public void Construct(Vector3 startPoint, Vector3 finishPoint, IGameOverHandler gameOverHandler)
    {
        _startPoint = startPoint;
        _finishPoint = finishPoint;
        _gameOverHandler = gameOverHandler;
        _isMoving = true;
    }

    private void Awake()
    {
        transform.position = _startPoint;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;
        
        var step = speed * Time.deltaTime;
        var position = Vector3.MoveTowards(transform.position, _finishPoint, step);
        transform.position = position;
        if (transform.position == _finishPoint)
        {
            var tempPosition = _finishPoint;
            _finishPoint = _startPoint;
            _startPoint = tempPosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isMoving = false;
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
        
        transform.SetParent(other.transform.parent);
        _isAttached = true;
        _rigidbody.isKinematic = true;
    }
    
    public class Factory : PlaceholderFactory<Vector3, Vector3, Block>
    {
    }
}
