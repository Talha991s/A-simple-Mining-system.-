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
        toggleBox = GameObject.Find("ScanningToggle").GetComponent<Toggle>();

        toggleText = GameObject.Find("Label").GetComponent<Text>();

        scansText = GameObject.Find("ScansText").GetComponent<Text>();

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        extractionsText = GameObject.Find("ExtractionText").GetComponent<Text>();

        tipsText = GameObject.Find("TipsText").GetComponent<Text>();
       
    }

    // Update is called once per frame
    void Update()
    {
        toggleBox.isOn = GameInfo.Scanning;
        toggleText.text = toggleBox.isOn ? "Scanning" : "Extracting";
        scansText.text = "Scans: " + GameInfo.Scans.ToString();
        scoreText.text = "Score: " + GameInfo.Score.ToString();
        extractionsText.text = "Extractions: " + GameInfo.Extractions.ToString();
        tipsText.text = GameInfo.Tip;

        if (!GameInfo.CanExtract)
        {
            GameInfo.TipIndex = 4;
            tipsText.text = GameInfo.Tip;

            GameObject Canvas = GameObject.Find("Canvas");

            gameOverPanel = Canvas.transform.Find("GameOverPanel").gameObject;
            gameOverPanel.SetActive(true);
            Destroy(this);
        }
    }
}
