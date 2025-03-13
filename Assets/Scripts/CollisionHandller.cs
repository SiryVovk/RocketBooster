using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandller : MonoBehaviour
{
    [SerializeField] private AudioClip sucsesSFX;
    [SerializeField] private AudioClip crashingSFX;
    [SerializeField] private ParticleSystem sucsesParticles;
    [SerializeField] private ParticleSystem crashingParticles;

    [SerializeField] private float delayToLoadScene = 2f;

    private AudioSource audioSource;

    private const string friendlyTag = "Friendly";
    private const string finishTag = "Finish";

    private bool isControlebl = true;
    private bool isColidebel = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControlebl || !isColidebel)
        {
            return;
        }

        switch(collision.gameObject.tag)
        {
            case friendlyTag:
                Debug.Log("ќу воно непогане");
                break;
            case finishTag:
                StartNextLevelLoadSequence();
                break;
            default:
                StartCrashSceneSequence();
                break;
        }
    }

    private void RespondToDebugKey()
    {
        if(Keyboard.current.tKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        else if(Keyboard.current.yKey.wasPressedThisFrame)
        {
            isColidebel = !isColidebel;
        }
    }

    private void StartNextLevelLoadSequence()
    {
        EndGameSettings(sucsesSFX,sucsesParticles);
        Invoke("LoadNextScene", delayToLoadScene);
    }

    private void StartCrashSceneSequence()
    {
        EndGameSettings(crashingSFX, crashingParticles);
        Invoke("ReloadScene", delayToLoadScene);
    }

    private void EndGameSettings(AudioClip clipToPlay,ParticleSystem particlesToPaly)
    {
        GetComponent<PlayerMovement>().enabled = false;
        audioSource.Stop();
        isControlebl = false;
        particlesToPaly.Play();
        audioSource.PlayOneShot(clipToPlay);
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
