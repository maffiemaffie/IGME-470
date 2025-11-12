using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySaltiness : MonoBehaviour
{
    public Image LowSalt;
    public Image MediumSalt;
    public Image HighSalt;

    [SerializeField]
    private TMP_Text SaltinessText;

    private void Awake()
    {
        SetSaltiness(0);
    }

    public void SetSaltiness(int saltiness)
    {
        HideSaltiness();

        if (saltiness > 0)
        {
            LowSalt.enabled = true;
            SaltinessText.text = "LIGHT SALT";

        }

        if (saltiness > 1)
        {
            MediumSalt.enabled = true;
            SaltinessText.text = "MEDIUM SALT";
        }

        if (saltiness > 2)
        {
            HighSalt.enabled = true;
            SaltinessText.text = "HEAVY SALT";
        }
    }

    public void HideSaltiness()
    {
        LowSalt.enabled = false;
        MediumSalt.enabled = false;
        HighSalt.enabled = false;
    }
}
