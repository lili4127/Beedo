using UnityEngine;
using TMPro;

public class LearnMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject learnPanel;
    [SerializeField] private TextMeshProUGUI learnText;
    [SerializeField] private int buttonPressed;
    public static event System.Action<int> toggleBarChange;
    private string emText;
    private string sunText;
    private string beedoText;
    private string colorsText;
    private string gasText;
    private string[] texts;

    private void OnEnable()
    {
        ProgressBar.buttonPressed += SetActiveButton;
    }

    private void Awake()
    {
        buttonPanel.SetActive(true);
        learnPanel.SetActive(false);

        emText = "Electromagnetic (EM) radiation is energy that can travel and spread in the form of waves. For example, visible light from a lamp, and radio waves from a radio station, are 2 types of EM radiation. Waves are classified on a spectrum from short to long waves: Gamma rays, X-rays, ultraviolet, visible, infrared, microwave, and radio waves.";
        sunText = "The majority of energy that the Sun emits is in the visible part of the EM spectrum. When this energy (light) reaches the Earth, some is reflected while the rest is absorbed. Absorbed energy not only warms the surface of our planet, but is also reemitted in the form of infrared radiation. Collectively, this process of absorption, reflection and reemission establishes a global energy balance which is fundamental to Earths climate system.";
        beedoText = "Albedo is a term used in planetary science synonymous with reflection. It represents the amount of incoming radiation returned from a surface. Earths albedo is roughly 30% meaning 30 percent of incoming radiation hitting the surface of our planet is being reflected and not absorbed. This percentage is mainly thanks to the clouds that Earth has in its atmosphere.";
        colorsText = "Albedo differs by color with lighter surfaces being more reflective and darker ones being less reflective. Looking at the colors white, tan, green, and blue (which can represent landscapes such as snow, sand, grass, and water on Earths surface), reflectivity decreases in that order. As polar ice caps begin to melt due to climate change, our planet is losing its most valuable reflective material causing it to absorb even more energy leading to global warming.";
        gasText = "Greenhouse gasses (GG) are gasses that absorb and reemit radiation in all directions. GG live in our atmosphere absorbing and re-emitting the energy our planet emits after absorbing it. This phenomenon, known as the greenhouse effect, contributes greatly to the warming of our planets surface and the lower atmosphere. Two of the most important GG are carbon dioxide (CO2) and methane (CH4) which are created through the burning of fossil fuels such as coal and oil. The more humans continue to use these fuels, the more GG fill the atmosphere to trap and redisperse emitted radiation. This creates a feedback loop where radiation is captured and re-emitted, the planet heats up, ice melts, reflectivity lowers, and absorption and emission increase. Many animals, including ourselves, already struggle to survive. This loop which steadily raises the planets temperature over time will eventually make survival impossible.";
        texts = new string[] { emText, sunText, beedoText ,colorsText, gasText};
}

    public void ToggleButtonPanel()
    {
        buttonPanel.SetActive(true);
        learnPanel.SetActive(false);
        toggleBarChange?.Invoke(buttonPressed);
    }

    public void ToggleLearnPanel()
    {
        buttonPanel.SetActive(false);
        learnPanel.SetActive(true);
    }

    private void SetActiveButton(int i)
    {
        buttonPressed = i;
        learnText.text = texts[buttonPressed];
    }

    private void OnDisable()
    {
        ProgressBar.buttonPressed -= SetActiveButton;
    }
}
