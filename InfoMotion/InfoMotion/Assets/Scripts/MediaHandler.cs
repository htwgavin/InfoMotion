using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MediaHandler : MonoBehaviour
{
    public AudioSource source;
    public AudioClip impact;
    public AudioClip impact1;
    public AudioClip impact2;
    public AudioClip impact3;
    public AudioClip attentionSound;
    // AudioSource audioSource;
    static bool attentionIsOn = false;

    void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        source = audioSource[0];
        source = audioSource[1];
        source = audioSource[2];
        source = audioSource[3];

        impact = audioSource[0].clip;
        impact1 = audioSource[1].clip;
        impact2 = audioSource[2].clip;
        impact3 = audioSource[3].clip;
        attentionSound = audioSource[4].clip;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attentionPlay();
            // Debug.Log("Space key was pressed.");
        }
    }
    public  void impactPlay()
        {

        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.PlayOneShot(impact);
        }

        }
    public void impact1Play()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.PlayOneShot(impact1);
        }
         }


    public void impact2Play()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.PlayOneShot(impact2);
        }
    }
    public void impact3Play()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.PlayOneShot(impact3);
        }
    }
    public static void playThisAttention()
    {
        attentionIsOn = true;
        // Debug.Log("Playing sound!");
    }

   

    public void attentionPlay()
    {
        // Debug.Log("attentionisOn = " +attentionIsOn);
        if(attentionIsOn==true)
        {
            source.Stop();
            source.PlayOneShot(attentionSound);
            attentionIsOn = false;
        }
 
    }
}

