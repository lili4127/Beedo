using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Sprite checkMark;
    private Image image;
    private bool isFlashing = true;
    private Color transparent = new Color(1, 1, 1, 0);
    private string buttonName;
    private int buttonNumber;
    public static event System.Action<int> buttonPressed;

    private void Awake()
    {
        image = GetComponent<Image>();
        buttonName = this.gameObject.name;
        buttonNumber = int.Parse(buttonName.Substring(buttonName.Length - 1));
    }

    private void OnEnable()
    {
        LearnMenu.toggleBarChange += ChangeProgressBar;
        if (image.color != Color.green)
        {
            StartCoroutine(FlashBarCo());
        }
    }

    //private void Start()
    //{
    //    StartCoroutine(FlashBarCo());
    //}

    public void ButtonPressed()
    {
        isFlashing = false;
        buttonPressed.Invoke(buttonNumber);

}

    IEnumerator FlashBarCo()
    {
        while (isFlashing)
        {
            for (float i = 1; i >= 0f; i -= 0.005f)
            {
                image.color = new Color (1,1,1,i);
                yield return new WaitForEndOfFrame();
            }
            image.color = transparent;

            for (float i = 0f; i <= 1f; i += 0.005f)
            {
                image.color = new Color(1, 1, 1, i);
                yield return new WaitForEndOfFrame();
            }

            image.color = Color.white;
        }
    }

    private void ChangeProgressBar(int i)
    {
        if(buttonNumber == i)
        {
            image.sprite = checkMark;
            image.color = Color.green;
        }
    }

    private void OnDisable()
    {
        LearnMenu.toggleBarChange -= ChangeProgressBar;
    }
}
