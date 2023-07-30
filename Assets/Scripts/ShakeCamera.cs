using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    public float Amplitude;
    public float Duration;

    bool isShaking = false;
    CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin _perlin;
    // Start is called before the first frame update

    public bool dshake = false;
    void Awake() {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        _perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void StartShake() {
        if (isShaking)
            return;
        Debug.Log("Start coroutine");
        isShaking = true;
        StartCoroutine(Shake());
    }

    IEnumerator Shake() {
        Debug.Log("Coroutine");
        _perlin.m_AmplitudeGain = Amplitude;
        yield return new WaitForSeconds(Duration);
        _perlin.m_AmplitudeGain = 0;
        isShaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (dshake) {
        //    StartShake();
        //}
    }
}
