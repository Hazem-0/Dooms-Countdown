using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlStatusbarOfHero : MonoBehaviour
{
    PlayerStatus playerStatus ;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }
  
    public void ChangeHealthBar() {

        LevelManager.Instance.MainCanvas.Find("PanelHeroHealth").GetChild(0).GetComponent<Image>().fillAmount = playerStatus.CurrentHealth / playerStatus.maxHealth;
        LevelManager.Instance.MainCanvas.Find("PanelHeroHealth").GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{(playerStatus.CurrentHealth / playerStatus.maxHealth)*100} % ";
        ChangeHealthColor();
    }
    public void ChangeHealthColor()
    {
        if (playerStatus.CurrentHealth < playerStatus.maxHealth / 3)
        {
            LevelManager.Instance.MainCanvas.Find("PanelHeroHealth").GetChild(0).GetComponent<Image>().color = Color.red;

        }
        else
        {
            string hexColor = "#00C00A";
            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
                LevelManager.Instance.MainCanvas.Find("PanelHeroHealth").GetChild(0).GetComponent<Image>().color = color;
        }
    }
    public void ChangeManaBar() {

        LevelManager.Instance.MainCanvas.Find("PanelMana").GetChild(0).GetComponent<Image>().fillAmount = playerStatus.CurrentMana / playerStatus.maxMana;

    }
   public void ChangeXP() {


        LevelManager.Instance.MainCanvas.Find("PanelXP").GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{playerStatus.XP} / {playerStatus.NextXP} ";


   }
    public void ChangeLvl()
    {
        LevelManager.Instance.MainCanvas.Find("PanelXP").GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Lvl {playerStatus.CurrentLevel}";

    }

}
