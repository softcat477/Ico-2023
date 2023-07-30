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
        if (animator.GetBool("powerUp"))
        {
            StartCoroutine(ExampleCoroutine());
        }
    }

    public void OnPlayerDefeatedBoss()
    {
        if (!animator.GetBool("powerUp"))
        {
            PlayPowerUpAnimationOnce();
        } else {
            StartCoroutine(ExampleCoroutine());
        }
    }

    public void PlayPowerUpAnimationOnce()
    {
        PlayTextAnimation();
    }

    public void PlayTextAnimation()
    {
        animator.SetBool("powerUp", true);
    }

    public void StopTextAnimation()
    {
        Debug.Log("stop");
        animator.SetBool("powerUp", false);
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StopTextAnimation();
        Destroy(gameObject);
    }
}
