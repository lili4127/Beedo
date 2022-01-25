using UnityEngine;

public class Mute : MonoBehaviour
{
    [SerializeField] private GameObject speakerImage;
    private bool isMute;

    private void Awake()
    {
        if (AudioListener.volume == 0)
        {
            isMute = true;
            speakerImage.SetActive(false);
        }

        else
        {
            isMute = false;
            speakerImage.SetActive(true);
        }
    }

    public void MuteAudio()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
        speakerImage.SetActive(!isMute);
    }
}
