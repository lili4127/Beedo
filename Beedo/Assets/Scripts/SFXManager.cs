using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Space]
    [Header("SFX")]
    [SerializeField] private AudioClip ballLost;
    [SerializeField] private AudioClip ballSaved;
    [SerializeField] private AudioClip ballBounce;
    [SerializeField] private AudioClip gasLaugh;
    [SerializeField] private AudioClip paddleChange;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Ball.ballLost += PlayBallLost;
        Ball.ballSaved += PlayBallSaved;
        Ball.ballBounced += PlayBallBounce;
        GreenhouseGas.gasLaugh += PlayGasLaugh;
        PlayerPaddle.paddleChange += PlayPaddleChange;
    }

    private void PlayBallBounce()
    {
        audioSource.PlayOneShot(ballBounce, 0.25f);
    }

    private void PlayBallLost()
    {
        audioSource.PlayOneShot(ballLost, 0.25f);
    }

    private void PlayBallSaved()
    {
        audioSource.PlayOneShot(ballSaved, 0.25f);
    }

    private void PlayGasLaugh()
    {
        audioSource.PlayOneShot(gasLaugh, 0.25f);
    }

    private void PlayPaddleChange()
    {
        audioSource.PlayOneShot(paddleChange, 0.25f);
    }

    private void OnDisable()
    {
        Ball.ballLost -= PlayBallLost;
        Ball.ballSaved -= PlayBallSaved;
        Ball.ballBounced -= PlayBallBounce;
        GreenhouseGas.gasLaugh -= PlayGasLaugh;
        PlayerPaddle.paddleChange -= PlayPaddleChange;
    }
}
