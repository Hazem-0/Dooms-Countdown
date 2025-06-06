
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float baseAttack = 10;
    [SerializeField] float baseDefence = 5;
    [SerializeField] float baseScore = 20;
    public float Attack { get; private set; }
    public float Defence { get; private set; }
    [SerializeField] int level  = 1;
    public float CurrentHealth { get; private set; }


    void Start()
    {
        CurrentHealth = maxHealth;
        Attack  = (float)(level * 1.5 + baseAttack);
        Defence = (float)(level * 0.5 + baseDefence);
        transform.Find("CanvasLVL").GetChild(0).GetComponent<TextMeshProUGUI>().text = $"LvL {level}";
        ChangeLvlColor();

    }
    void ChangeLvlColor()
    {
        if (level - LevelManager.Instance.Player.transform.GetComponent<PlayerStatus>().CurrentLevel > 10)
        {
            transform.Find("CanvasLVL").GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;

        }
        else
        {
            string hexColor = "#00C00A";
            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
                transform.Find("CanvasLVL").GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
        }
    }
    public void ChangeHealth(float health)
    {
        CurrentHealth += health;
        transform.Find("CanvasHealth").GetChild(0).GetComponent<UnityEngine.UI.Image>().fillAmount = CurrentHealth/maxHealth;
        Debug.Log("Current health : " + CurrentHealth + " / " + maxHealth);
        //Debug.Log("Current  : " + CurrentHealth / maxHealth);
        //if (CurrentHealth <= 0) Die();
    }
    public float DropScore()
    {
        float score = level * 3 + baseScore;
        return score;
    }
    public void Die()
    {
        //Debug.Log("Xp : " + DropScore());
        LevelManager.Instance.Player.GetComponent<PlayerStatus>().ChangeXP(DropScore());
        //Debug.Log("Current Xp : " + playerStatus.XP);
        Destroy(gameObject);
    }
}
