using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class chair : MonoBehaviour
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
    void Start()
    {
        // initialize RTcmix
        RTcmix.initRTcmix(objno);

        String path = Application.dataPath;

        //RTcmix.SendScore("rtinput = (\"" + path + "/movingChair.v02.wav\")", objno);
        RTcmix.SendScore("infile = \"" + path + "/movingChair.v02.wav\")", objno);

        //score = "rtsetparams(48000, 2) " +
            // "load(\"STEREO\") " +
            // "load(\"MROOM\") " +
            // "load(\"PANECHO\") " +
            // "infile =\"/Users/ercelwilliams/Downloads/movingChair.v02.wav/" " +
            // "rtinput(infile) " +
            // "sfdur = filedur(infile) " +
            // "bus_config(\"STEREO\", \"in 0-1\", \"aux 0-1 out\") " +
            // "bus_config(\"MROOM\", \"PANECHO\", \"aux 0-1 in\", \"out 0-1\") " +
            // "STEREO(outsk=0, insk=0, dur=sfdur, amp=5, pan=0.5) " +
            // "outskip = 0 " +
            // "inskip = 0 " +
            // "dur = DUR() " +
            // "amp = 0.6 " +
            // "xdim = 30 ydim = 80 rvbtime = 1.0 " +
            // "reflect = 90.0 innerwidth = 8.0 inchan = 0  " +
            // "quant = 2000 timeset(0, 0-xdim, 0-ydim) " +
            // "timeset(dur/2, xdim/8, ydim/8) " +
            // "timeset(dur, xdim, ydim) " +
            // "makegen(1, 24, 1000, 0,0, dur/8,1, dur-.5,1, dur,0) " +
            // "MROOM(outskip, inskip, dur, amp, xdim, ydim, rvbtime, reflect, innerwidth, inchan, quant) " +
            // "ampenv = maketable(\"line\", 1000, 0,0, 0.5,1, 3.5,1, 7,0) " +
            // "PANECHO(0, 0, 7, 0.7*ampenv, .14, 0.069, .7, 3.5) " +
            // "    " +
            // "ampenv= maketable(\"line\", 1000, 0,0, 0.1,1, 1.5,0.21, 3.5,1, 7,0) " +
            // "PANECHO(4.9, 0, 7, 1*ampenv, 3.14, 0.5, 0.35, 9.5) ";

        scorestring = scoretext.text;

        //RTcmix.SendScore(score, objno);
        RTcmix.SendScore(scorestring, objno);

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