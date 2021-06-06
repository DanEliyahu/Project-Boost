using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;
    
    private Vector3 _startingPos;
    private float _movementFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        _startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        var cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        var rawSinWave = Mathf.Sin(cycles * tau);
        _movementFactor = (rawSinWave + 1) / 2f; // 0 to 1 values
        var offset = movementVector * _movementFactor;
        transform.position = _startingPos + offset;
    }
}
