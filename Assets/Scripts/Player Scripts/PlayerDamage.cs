using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public Text lifeText;
    private int lifeScore;
    private bool canDamage;
    private Vector3 startPoint;

    private void Awake()
    {
        lifeScore = 3;
        lifeText.text = "x" + lifeScore;
        canDamage = true;

        transform.position = startPoint;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if (canDamage)
        {
            lifeScore--;
            if (lifeScore >= 0)
            {
                lifeText.text = "x" + lifeScore;
            }
            if (lifeScore == 0)
            {
                // restart the game
                Time.timeScale = 0;
                StartCoroutine(RestartGame());
            }
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Gameplay");
    }
}