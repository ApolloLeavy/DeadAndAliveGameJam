using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : PlayerProjectile
{
    public float waveDur;
    public Vector3 waveScale;
    // Start is called before the first frame update
    void Start()
    {
        waveDur = 1;
        waveScale = new Vector3(1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += waveScale;
    }
    protected new void OnTriggerEnter(Collider other)
    {
        WaveDur();
        Destroy(this.gameObject);
    }
    IEnumerator WaveDur()
    {
            yield return new WaitForSecondsRealtime(waveDur);  
    }
}
