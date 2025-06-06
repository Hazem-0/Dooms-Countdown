using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;
    public GameObject on, off;
    public void SetVolume(float volume=100)
    {
        
        audioMixer.SetFloat("volume", volume);
    }
    public void On()
    {
        audioMixer.SetFloat("volume", 0);
        on.SetActive(true);
        off.SetActive(false);
    }

    public void Off()
    {
        audioMixer.SetFloat("volume", -80);
        on.SetActive(false);
        off.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
