using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class beep : MonoBehaviour {
    int objno = 0;
    rtcmixmain RTcmix;
    private bool did_start = false;


    private void Awake()
    {
        RTcmix = GameObject.Find("RTcmixmain").GetComponent<rtcmixmain>();
    }


    // Use this for initialization
    void Start ()
    {
    
        RTcmix.initRTcmix(objno);
        RTcmix.SendScore("WAVETABLE(0, 0.5, 20000, 8.07, 0.5) MAXBANG(1.0)", objno);

        did_start = true;
    }
	

	// Update is called once per frame
	void Update ()
    {		
	}


    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!did_start) return;

      
        RTcmix.runRTcmix(data, objno, 0);

        if (RTcmix.checkbangRTcmix(objno) == 1) {
            RTcmix.SendScore("WAVETABLE(0, 0.5, 20000, 8.07, 0.5) MAXBANG(1.0)", objno);

        }
    }


    void OnApplicationQuit()
    {
        did_start = false;
        RTcmix = null;
    }
}
