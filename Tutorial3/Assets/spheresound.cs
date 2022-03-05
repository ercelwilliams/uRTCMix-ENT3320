using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spheresound : MonoBehaviour
{
    int objno = 0;
    public rtcmixmain RTcmix;
    bool did_start = false;

    public TextAsset scoretext; 
    string scorestring;


    // Start is called before the first frame update
    void Start()
    {
        RTcmix.initRTcmix(objno);

        scorestring = scoretext.text;

        //RTcmix.SendScore("WAVETABLE(0, 3.4, 20000, 789)", objno);

        RTcmix.SendScore(scorestring, objno);

        did_start = true;

        
    }

    private void OnAudioFilterRead(float[] data, int channels){
        if (did_start == false) return;

        RTcmix.runRTcmix(data, objno, 0);

        if (RTcmix.checkbangRTcmix(objno) == 1){
            RTcmix.SendScore(scorestring, objno);

        }
    }

    private void OnApplicationQuit(){
        RTcmix = null;
        did_start = false;
    }
}
