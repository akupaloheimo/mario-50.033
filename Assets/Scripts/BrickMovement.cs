using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class BrickMovement : MonoBehaviour
{
    public Animator brickAnimator;
    public Animator coinAnimator;
    public AudioSource coinAudio;
    public AudioClip coin;
    public bool boxOn = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with brick!");
            brickAnimator.SetTrigger("onTouch");
            coinAnimator.Play("bounce");
            if (boxOn)
            {
                coinAnimator.Play("bounceQ");
                coinAudio.PlayOneShot(coin);
            }
            boxOn = false;
        }
    }
}