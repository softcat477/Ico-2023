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
        OnPlayerDefeatedBoss();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnPlayerDefeatedBoss()
    {
        if (!animator.GetBool("powerUp"))
        {
            PlayPowerUpAnimationOnce();
        } else {
            StartCoroutine(ExampleCoroutine());
        }
    }

    void PlayPowerUpAnimationOnce()
    {
        PlayTextAnimation();
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
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StopTextAnimation();
    }
}
