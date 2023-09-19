using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;


public class ScoreKeeperScr : MonoBehaviour
{
    public PackageSpawner packageSpawner;
    public TextMeshProUGUI packageCountText;

    private int packageCount=0;

    private int lifeCount=4;

    private string lifeBar="♥♥♥";
    public TextMeshProUGUI lifeBarTxt;
    public int packageCountWin=15;

    public Image lifeBarImg;
    public Sprite[] lifeLevels;

    // Start is called before the first frame update
    void Start()
    {
            packageCountText.enabled = false;
            lifeBarImg.sprite = lifeLevels[0];

    }

    // Update is called once per frame
    void Update()
    {

        if(packageCount!=-1){
            // Check if the TextMeshPro element is disabled.
            if (packageCountText.enabled==false)
            {
                // If it's disabled, enable it.
                packageCountText.enabled = true;
            }
        }

        
        if (packageCount >= packageCountWin)
        {
            win();
        }
    }

    public void win(){
        
        packageCountText.text = "You Win!";
        PlayerPrefs.SetString("LoadingSceneState", "LevelEnded");
        PlayerPrefs.SetString("LevelEndedState", "winState");
        string currentLevelName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("CurrentLevel", currentLevelName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenue");
        gameObject.GetComponent<ARSession>().Reset();
    }

    public void lose(){
        
        packageCountText.text = "You Lose!";
        PlayerPrefs.SetString("LoadingSceneState", "LevelEnded");
        PlayerPrefs.SetString("LevelEndedState", "lostState");
        string currentLevelName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("CurrentLevel", currentLevelName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenue");
        gameObject.GetComponent<ARSession>().Reset();
    }

    public void packageCollected(){
        packageCount++;
        packageCountText.text = "Package Count: " + packageCount.ToString() ;
    }

    public void packageDropped(){
        packageCount--;
    }

    public void resetPackageCount(){
        packageCount=0;
    }

    public void bombHit(){
        lifeCount--;
        if(lifeCount<0){
            lose();
        }
        else{
            lifeBar=lifeBar.Substring(1);
            lifeBarTxt.text = lifeBar;

            if(lifeCount==3){
                lifeBarImg.sprite = lifeLevels[0];
            }
            else if(lifeCount==2){
                lifeBarImg.sprite = lifeLevels[1];
            }
            else if(lifeCount==1){
                lifeBarImg.sprite = lifeLevels[2];
            }
        }
    }

}

