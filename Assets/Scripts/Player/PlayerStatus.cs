
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update


    ControlStatusbarOfHero controlStatusbarOfHero;

    public float maxHealth = 100;
    public float maxMana = 50;
    public float attack = 10;
    public float defence = 10;
    
    
     float AttackAura = 15;
     float DefenceAura = 10;

    float MovementAura = 10; 





    public float XP { get; private set; }
    public float NextXP { get; private set; }
    public int CurrentLevel { get; private set; }
    public float CurrentHealth { get; private set; }
    public float CurrentMana { get; private set; }

    void Start()
    {
        XP = 0;
        NextXP = 100;
        CurrentLevel = 1;
        CurrentHealth = maxHealth;
        CurrentMana = maxMana;
        controlStatusbarOfHero = GetComponent<ControlStatusbarOfHero>();
        
    }

    public void ChangeHealth(float health)
    {
        CurrentHealth += health;
        MaxHealth();
        controlStatusbarOfHero.ChangeHealthBar();
        Debug.Log("Current Health : "+ CurrentHealth);

    }
   
    void MaxHealth()
    {
        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
    }
    public void ChangeXP(float xp) {
        XP += xp;
        ChangeNextXP();
        controlStatusbarOfHero.ChangeXP();
    }
    void ChangeNextXP()
    {
        if (XP > NextXP)
        {
            UpgradeLevle();
        }
    }
    void UpgradeLevle()
    {
        CurrentLevel++;
        NextXP += XP * 2;
        LevelManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("LvlUP");
        Instantiate(LevelManager.Instance.Particles[1] , transform.position , transform.rotation);
        UpgradeProperties();
    }
     void UpgradeProperties()
    {
        maxHealth = maxHealth*(CurrentLevel/2)+maxHealth;
        attack += (float)(CurrentLevel*1.5);
        defence += (float)(CurrentLevel*0.5);
        maxMana = maxMana * (CurrentLevel / 2) + maxMana;
        CurrentHealth = maxHealth;
        CurrentMana = maxMana;
        controlStatusbarOfHero.ChangeHealthBar();
        controlStatusbarOfHero.ChangeManaBar();
        controlStatusbarOfHero.ChangeLvl();

    }
    public void ChangeMana(float mana)
    {
        CurrentMana += mana;
        MaxMana();
        controlStatusbarOfHero.ChangeManaBar();
    }
    void MaxMana()
    {
        if (CurrentMana > maxMana)
            CurrentMana = maxMana;
    }
    public void AcitveAura()
    {
        LevelManager.Instance.Player.GetComponent<Movement>().movementSpeed += MovementAura;
        attack += AttackAura;
        defence += DefenceAura;
    }
    public void DesActivateAura()
    {
        LevelManager.Instance.Player.GetComponent<Movement>().movementSpeed -= MovementAura;

        attack -= AttackAura;
        defence -= DefenceAura;
    }
}
