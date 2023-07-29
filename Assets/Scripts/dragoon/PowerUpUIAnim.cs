using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpUIAnim : MonoBehaviour
{
    public Animator animator;
    public GameObject textGO;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayPowerUpAnimationOnce();
    }

    void PlayPowerUpAnimationOnce()
    {
        PlayTextAnimation();

        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            StopTextAnimation();
        }

        // StartCoroutine(ExampleCoroutine());
    }

    void PlayTextAnimation()
    {
        animator.SetBool("powerUp", true);
    }

    void StopTextAnimation()
    {
        Debug.Log("stop");
        animator.SetBool("powerUp", false);
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
