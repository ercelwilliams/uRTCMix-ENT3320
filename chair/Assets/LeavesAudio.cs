using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeavesAudio : MonoBehaviour
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

        RTcmix.SendScore("rtinput = (\"" + path + "/leavesRustle.wav\")", objno2);
       // RTcmix.SendScore("infile = \"" + path + "/Assets/uRTcmix-0.91/revisedsounds/leavesRustle.wav\")", objno2);

    //    score = "rtsetparams(48000, 2) " +
    //         "load(\"STEREO\") " +
    //         "load(\"MOOGVCF\") " +
    //         "load(\"REVERBIT\") " +
    //         //"rtinput(\"\Users\super\Downloads\sound\leavesRustle.wav\") " +
    //         "STEREO(outsk=0, insk=0, dur=9999, amp=15, pan=0.5) " +
    //         "bus_config(\"MOOGVCF\", \"in 0-1\", \"aux 0-1 out\") " +
    //         "bus_config(\"REVERBIT\", \"aux 0-1 in\", \"out 0-1\") " +
    //         "freq = maketable(\"line\", \"nonorm\", 2000, 0,1000, 100,1500, 300,1800, 400,2000) " +
    //         "MOOGVCF(outsk=0.0, insk=0.0, dur=9999, amp=1.0, inputchan=0.0, pan=1, bypass=0, freq, filtresontable=0.9) " +
    //         "REVERBIT(outskip=0.0, inskip=0.0, dur=9999, amp=1.0, revtime=10, revpct=0.2, rtchandel=0.02, cutoff=5000.0) ";


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
        RTcmix.runRTcmix(data, objno2, 0); // set "0" to "1" for input processing

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