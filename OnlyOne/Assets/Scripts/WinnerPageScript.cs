using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPageScript : MonoBehaviour
{
    public Text winnerTitle;
    public Image winnerImage;
    // Start is called before the first frame update
    void Start()
    {
        if (ProgressInformation.winnerPlayerTag == null)
        {
            return;
        }
        if (ProgressInformation.winnerPlayerTag.ToString() == "BluePlayer") {
            winnerTitle.text = "Blue player won!";
            winnerImage.sprite = Resources.Load<Sprite>("FaceNinja");
        } else {
            winnerTitle.text = "Red player won!";
            winnerImage.sprite = Resources.Load<Sprite>("FaceMask");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SceneManager.LoadScene("MainMenu");
            Debug.Log("Open main menu");
        }
    }
}
