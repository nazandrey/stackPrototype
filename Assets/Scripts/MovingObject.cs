using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Tests.PlayMode")]
public class MovingObject : MonoBehaviour
{
    [SerializeField] 
    internal float speed;
    
    private Vector3 _startPoint;
    private Vector3 _finishPoint;

    public bool ShouldMove { get; set; }

    public void Init(Vector3 startPoint, Vector3 finishPoint)
    {
        _startPoint = startPoint;
        _finishPoint = finishPoint;
        ShouldMove = true;
    }

    private void Start()
    {
        transform.position = _startPoint;
    }

    private void FixedUpdate()
    {
        if (!ShouldMove)
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
}