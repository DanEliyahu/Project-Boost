using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private AudioClip successSfx;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;

    private AudioSource _audioSource;
    private bool _isTransitioning = false;
    private bool _isDebug = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _isDebug = !_isDebug;
        }

        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || _isDebug)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;
        var movementScript = GetComponent<Movement>();
        movementScript.StopJetParticles();
        movementScript.enabled = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashSfx);
        crashParticles.Play();
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        _isTransitioning = true;
        var movementScript = GetComponent<Movement>();
        movementScript.StopJetParticles();
        movementScript.enabled = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(successSfx);
        successParticles.Play();
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        var nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}