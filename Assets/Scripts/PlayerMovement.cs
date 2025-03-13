using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private AudioClip enginActiveSFX;
    [SerializeField] private ParticleSystem mainThrusterParticles;
    [SerializeField] private ParticleSystem rightTurnParticles;
    [SerializeField] private ParticleSystem leftTurnParticles;

    [SerializeField] private float thrustSpeed = 10f;
    [SerializeField] private float rotationStrength = 10f;

    private Rigidbody rb;
    private AudioSource audioSource;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
        AudioControll();
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        mainThrusterParticles.Stop();
        audioSource.Stop();
    }

    private void AudioControll()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(enginActiveSFX);
        }
    }

    private void ProcessRotation()
    {
        float rotationValue = rotation.ReadValue<float>();

        if (rotationValue < 0)
        {
            StartRotation(-rotationStrength, leftTurnParticles, rightTurnParticles);
        }
        else if (rotationValue > 0)
        {
            StartRotation(rotationStrength, rightTurnParticles, leftTurnParticles);
        }
        else
        {
            StopRotation();
        }
    }

    private void StartRotation(float rotationSide, ParticleSystem firstParticle, ParticleSystem secondParicle)
    {
        ApplyRotation(rotationSide);
        if (!firstParticle.isPlaying)
        {
            secondParicle.Stop();
            firstParticle.Play();
        }
    }

    private void StopRotation()
    {
        rightTurnParticles.Stop();
        leftTurnParticles.Stop();
    }

    private void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

}
