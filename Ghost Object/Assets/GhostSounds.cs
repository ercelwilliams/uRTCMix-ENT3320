using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhostSounds : MonoBehaviour
{
    int objno2 = 0; // set this to the RTcmix instance you are using for this script
    rtcmixmain RTcmix;
    private bool did_start = false;

    //String score;
    public TextAsset scoretext;
    String scorestring;

    private void Awake()
    {
        // find the RTcmixmain object with the RTcmix function definitions
        RTcmix = GameObject.Find("RTcmixmain").GetComponent<rtcmixmain>();
    }


    // Use this for initialization
    void Start()
    {
        // initialize RTcmix
        RTcmix.initRTcmix(objno2);

        String path = Application.dataPath;

        RTcmix.SendScore("rtinput = (\"" + path + "/softWindBlowing.wav\")", objno2);
        //RTcmix.SendScore("infile = \"" + path + "/Assets/uRTcmix-0.91/revisedsounds/softWindBlowing.wav\")", objno2);

        // score = "reset 48000 " +
        //         "load(\"STEREO\") " +
        //         "load(\"DISTORT\") " +
        //        // "rtinput(\"\Users\super\Downloads\sound\softWindBlowing.wav\") " +
        //         "bus_config(\"STEREO\", \"in 0-1\", \"aux 0-1 out\") " +
        //         "bus_config(\"DISTORT\", \"aux 0-1 in\", \"out 0-1\") " +
        //         "STEREO(outsk=0, insk=1, dur=9999, amp=5, pan=0.5) " +
        //         "ampenv = maketable(\"window\", 1000, \"hanning\") " +
        //         "DISTORT(0.5, 0, 8.0, 2*ampenv, 1, 4, 50, 0, 1) " +
        //         "DISTORT(0, 0, 5.0, 2*ampenv, 1, 3, 50, 0, 0) ";

        scorestring = scoretext.text;

        //RTcmix.SendScore(score, objno2);
        RTcmix.SendScore(scorestring, objno2);

        did_start = true;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //}

    // called once for each sample-buffer. data[] contains incoming and outgoing samples
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!did_start) return;

        // compute sound samples
        RTcmix.runRTcmix(data, objno2, 1); // set "0" to "1" for input processing

        if (RTcmix.checkbangRTcmix(objno2) == 1) {
            //// a MAXBANG was received
            //RTcmix.SendScore(score, objno2);

            // Resend infile as well??
            RTcmix.SendScore(scorestring, objno2);
        }

    }


    void OnApplicationQuit()
    {
        did_start = false;
        RTcmix = null;
    }

}