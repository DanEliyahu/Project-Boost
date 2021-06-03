using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrust = 10f;
    [SerializeField] private float torque = 10f;
    [SerializeField] private AudioClip thrustSfx;
    [SerializeField] private ParticleSystem jetParticles;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * thrust);
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(thrustSfx);
            }

            if (!jetParticles.isPlaying)
            {
                jetParticles.Play();
            }
        }
        else
        {
            _audioSource.Stop();
            jetParticles.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddRelativeTorque(Vector3.forward * torque, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddRelativeTorque(Vector3.back * torque, ForceMode.Impulse);
        }
    }

    public void StopJetParticles()
    {
        jetParticles.Stop();
    }
}