using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChange : MonoBehaviour
{
    public static int currentWave = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WaveReset()
    {
        currentWave++;

    }

}
