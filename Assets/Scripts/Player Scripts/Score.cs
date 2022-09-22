using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text coinText;
    private AudioSource audioManager;
    private int scoreCount;

    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.COIN))
        {
            collision.gameObject.SetActive(false);
            scoreCount++;

            coinText.text = "x" + scoreCount;
            audioManager.Play();
        }
    }
}