using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathSongScript : MonoBehaviour {
    public AudioClip MathSongClip;
    public AudioSource MathSongSource;

    bool enter_pressed = false;
    bool space_pressed = false;

    public int getSongPosition()
    {
        return MathSongSource.timeSamples;
    }

    public int getAudioClipFrequency ()
    {
        return MathSongClip.frequency;
    }

    // Use this for initialization
    void Start()
    {
        MathSongSource.clip = MathSongClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enter_pressed && Input.GetKey(KeyCode.Return))
        {
            MathSongSource.Play();
            enter_pressed = true;
        }
        else if (space_pressed == false && Input.GetKey(KeyCode.Space))
        {
            space_pressed = true;
            if (MathSongSource.isPlaying)
            {
                MathSongSource.Pause();
            }
            else
            {
                MathSongSource.UnPause();
            }
        }
        if (space_pressed == true && !Input.GetKey(KeyCode.Space))
        {
            space_pressed = false;
        }
    }
}
