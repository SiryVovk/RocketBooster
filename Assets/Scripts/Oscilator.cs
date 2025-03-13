using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;

    [SerializeField] private float speed = 10f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float movmentFactor;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    private void Update()
    {
        movmentFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movmentFactor);
    }
}
