using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    private GameObject gameOverPanel;

    private Toggle toggleBox;
    private Text toggleText;
    private Text scansText;
    private Text scoreText;
    private Text extractionsText;
    private Text tipsText;


    // Start is called before the first frame update
    void Start()
    {
        toggleBox = GameObject.Find("Toggle").GetComponent<Toggle>();

        toggleText = GameObject.Find("Label").GetComponent<Text>();

        scansText = GameObject.Find("Scan").GetComponent<Text>();

        scoreText = GameObject.Find("Score").GetComponent<Text>();

        extractionsText = GameObject.Find("Extract").GetComponent<Text>();

        tipsText = GameObject.Find("tips").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        toggleBox.isOn = GameInfo.Scanning;
        toggleText.text = toggleBox.isOn ? "Scanning" : "Extracting";
        scansText.text = "Scan: " + GameInfo.Scans.ToString();
        scoreText.text = "Score: " + GameInfo.Score.ToString();
        extractionsText.text = "Extract: " + GameInfo.Extractions.ToString();
        tipsText.text = GameInfo.Tip;

        if (!GameInfo.CanExtract)
        {
            GameInfo.TipIndex = 4;
            tipsText.text = GameInfo.Tip;

            GameObject Canvas = GameObject.Find("Canvas");

            gameOverPanel = Canvas.transform.Find("GameOverScreen").gameObject;
            gameOverPanel.SetActive(true);
            Destroy(this);
        }
    }
}
