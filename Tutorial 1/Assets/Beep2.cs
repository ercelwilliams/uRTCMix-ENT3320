using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beep2 : MonoBehaviour
{
    int objno = 0;
    public rtcmixmain RTcmix;
    bool did_start = false;

    // Start is called before the first frame update
    void Start()
    {
        RTcmix.initRTcmix(objno);

        RTcmix.SendScore("WAVETABLE(0, 7.8, 20000, 8.05, 0.5)", objno);

        did_start = true;
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
     void OnAudioFilterRead(float[] data, int channels){
            if (!did_start) return;

            RTcmix.runRTcmix(data, objno, 0);
        }

        private void OnApplicationQuit(){
            RTcmix = null;
            did_start = false;
        }
}
