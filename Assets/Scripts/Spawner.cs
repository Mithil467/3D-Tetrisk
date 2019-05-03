using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static string GO = "";
    public static bool never_started = true;
    public Text score_str, text, over;
    public GameObject[] blocks;
    public void startedonce()
    {
        if (never_started)
            spawnNext();
        GO = "";
        never_started = false;
    }
    public void spawnNext()
    {

            gridMatrix.score += 10;
            int i = Random.Range(0, blocks.Length);

            Instantiate(blocks[i], transform.position, Quaternion.identity);
    }
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (gridMatrix.score > gridMatrix.hs)
        {
            gridMatrix.hs = gridMatrix.score;
            text.text = "High Score :\n" + gridMatrix.score.ToString();
        }
        text.text = "High Score :\n" + PlayerPrefs.GetInt("highscore", gridMatrix.hs);
        PlayerPrefs.SetInt("highscore", gridMatrix.hs);
        over.text = GO;
        score_str.text = "Score :\n" + gridMatrix.score.ToString();
    }

    public void endgame()
    {
        Application.Quit();
    }
}
