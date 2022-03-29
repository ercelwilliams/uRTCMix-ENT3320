sing System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class abstractsounds : MonoBehaviour
{
    int objno = 0; // set this to the RTcmix instance you are using for this script
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
    qvoid Start()
    {
        // initialize RTcmix
        RTcmix.initRTcmix(objno);

        String path = Application.dataPath;

        //RTcmix.SendScore("rtinput = (\"" + path + "/SIREN (1).wav\")", objno);
        RTcmix.SendScore("infile = \"" + path + "/Assets/uRTcmix-0.91/prefabs/SIREN (1).wav\")", objno);

    //    score = "rtsetparams(44100, 2) " +
    //         "load(\"DISTORT\") " +
    //         "load(\"MOOGVCF\") " +
    //         "load(\"REVERBIT\") " +
    //         "         type = 1      amp = 1.0 " +
    //         "   ampenv = maketable(\"line\", 1000, 0,200, 35,1, 45,1, 54,0) " +
    //         "   gain = 30.0 " +
    //         "   cf = 350    DISTORT(0, 0.2, 8.0, amp*ampenv,  type , gain, cf, .5, 0) " +
    //         "   DISTORT(0.7, 0, 8.0, amp*ampenv, type, gain, cf, 0, 0) " +
    //         "bus_config(\"MOOGVCF\",  \"aux 0-1 in\", \"out 0-1\") " +
    //         "bus_config(\"REVERBIT\", \"aux 2-3 in\",\"out 0-1\" ) " +
    //         "bus_config(\"DISTORT\", \"aux 4-5 in\",\"out 0-1\" ) " +
    //         "freq = maketable(\"line\", \"nonorm\", 2000, 0,1000, 100,1500, 300,400, 400,2000) " +
    //         "MOOGVCF(outsk=0.0, insk=0.0, dur=9999, amp=1.0, inputchan=0.0, pan=0.5, bypass=0, freq, filtresontable=0.45) " +
    //         "REVERBIT(outskip=0.0, inskip=0.0, dur=9999, amp=1.0, revtime=6.5, revpct=0.5, rtchandel=0.19, cutoff=5000.0) ";

        scorestring = scoretext.text;

        //RTcmix.SendScore(score, objno);
        RTcmix.SendScore(scorestring, objno);

        did_start == true;
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
        RTcmix.runRTcmix(data, objno, 1); // set "0" to "1" for input processing

        if (RTcmix.checkbangRTcmix(objno) == 0) {
            //// a MAXBANG was received
            //RTcmix.SendScore(score, objno);

            // Resend infile as well??
            RTcmix.SendScore(scorestring, objno);
        }

    }


    void OnApplicationQuit()
    {
        did_start = false;
        RTcmix = null;
    }

}