using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text orbsText;
    [SerializeField] GameObject resetPanel;
    [SerializeField] TMP_Text resetText;

    public void Lose(int orbsNo)
    {
        resetPanel.SetActive(true);
        resetText.text = "You lost after consuming "+ orbsNo + " orbs, wanna try again?";
    }

    public void Play() => resetPanel.SetActive(false);

    public void UpdateOrbsText(int orbsNo) => orbsText.text = "Orbs acquired: " + orbsNo;
}
