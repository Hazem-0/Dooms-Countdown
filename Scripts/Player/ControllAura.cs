using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllAura : MonoBehaviour
{
    ParticleSystem aura;
    private float tickInterval = 1f;
    bool isActive = false;
    bool isBoosted = false;
    // Update is called once per frame


    private void Start()
    {
        aura = transform.GetComponent<ParticleSystem>();
        DesActivateAura();

        StartCoroutine(ManaTick());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && aura.isStopped && ValidManaForUsed())
        {
           isActive = true;
        }
        else if ((Input.GetKeyDown(KeyCode.F) && aura.isPlaying)|| !ValidManaForUsed())
        {
            isActive = false;

        }
    }

    void ActiveAura()
    {
        aura.Play();
        var controllLoop = aura.main;
        controllLoop.loop = true;
    }
    void DesActivateAura()
    {
        var controllLoop = aura.main;
        controllLoop.loop = false;
        aura.Stop();
    }
    bool ValidManaForUsed()
    {
        return LevelManager.Instance.Player.GetComponent<PlayerStatus>().CurrentMana > 0 ;
    }
    IEnumerator ManaTick()
    {
        while (true) { 
            yield return new WaitForSeconds(tickInterval);
            if (isActive)
            {
                ActiveAura();

                LevelManager.Instance.Player.GetComponent<PlayerStatus>().ChangeMana(-5);

                if (!isBoosted)
                {
                    LevelManager.Instance.Player.GetComponent<PlayerStatus>().AcitveAura();
                    isBoosted = true;

                    Debug.Log("Current Attack is : " + LevelManager.Instance.Player.GetComponent<PlayerStatus>().attack);
                    Debug.Log("Current Defence is : " + LevelManager.Instance.Player.GetComponent<PlayerStatus>().defence);
                    Debug.Log("Current Movement is : " + LevelManager.Instance.Player.GetComponent<Movement>().movementSpeed);
                }
            }
            else 
            {

                DesActivateAura();
                LevelManager.Instance.Player.GetComponent<PlayerStatus>().ChangeMana(2);
                if (isBoosted)
                {
                    LevelManager.Instance.Player.GetComponent<PlayerStatus>().DesActivateAura();
                    isBoosted= false;
                    
                    Debug.Log("Current Attack is : " + LevelManager.Instance.Player.GetComponent<PlayerStatus>().attack);
                    Debug.Log("Current Defence is : " + LevelManager.Instance.Player.GetComponent<PlayerStatus>().defence);
                    Debug.Log("Current Movement is : " + LevelManager.Instance.Player.GetComponent<Movement>().movementSpeed);


                }

            }


            Debug.Log("Current Mana is : "+ LevelManager.Instance.Player.GetComponent<PlayerStatus>().CurrentMana);
        }
    }


}
