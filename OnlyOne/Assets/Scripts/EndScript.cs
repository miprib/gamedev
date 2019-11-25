using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{

    public Transform redPlayer;
    public Transform bluePlayer;

    // Start is called before the first frame update
    void Start()
    { 

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == redPlayer.tag ) {
            ProgressInformation.winnerPlayerTag = redPlayer.tag.ToString();
            SceneManager.LoadScene("WinningScene");
            Debug.Log("Red won");
        }

        if (collision.gameObject.tag == bluePlayer.tag)
        {
            ProgressInformation.winnerPlayerTag = bluePlayer.tag.ToString();
            SceneManager.LoadScene("WinningScene");
            Debug.Log("Blue won");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
