using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class MainMenu : MonoBehaviour
{

    public GameObject levelButtonPrfb;
    public GameObject levelButtonContainer;

    private Transform camaraTransform;
    private Transform camaraDesiredLookAt;

    public float camaraSpeed;

    public Transform winscreenTransform;

    public TextMeshProUGUI levelEndText;
    public TextMeshProUGUI debugText;
    private bool lookAtEndLevScreen= false;

    public static bool initDone = false;


    // ==================
    private string LoadingSceneStatetxt;
    private string lookAtEndLevScreentxt;
    private string camaraDesiredLookAttxt;

    private void Awake() {
        if (!initDone)
        {
            initDone = true;
            PlayerPrefs.SetString("LoadingSceneState", "Null");
            PlayerPrefs.SetString("LevelEndedState", "Null");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        camaraTransform = Camera.main.transform;
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelButtonPrfb) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);

            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }
        if (PlayerPrefs.HasKey("LoadingSceneState"))
        {
            // ===========
            LoadingSceneStatetxt = PlayerPrefs.GetString("LoadingSceneState","");
            string loadingSceneState = PlayerPrefs.GetString("LoadingSceneState","");
            if (loadingSceneState=="LevelEnded")
            {
                lookAtEndLevScreen = true;
                lookAtEndLevScreentxt = lookAtEndLevScreen.ToString();
                camaraTransform.LookAt(winscreenTransform.position);
                if(PlayerPrefs.HasKey("LevelEndedState")){
                    string levelEndedState = PlayerPrefs.GetString("LevelEndedState","");
                    LevelEndSscreen(levelEndedState);
                    PlayerPrefs.DeleteKey("LevelEndedState");
                }
                 PlayerPrefs.DeleteKey("LoadingSceneState");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (camaraDesiredLookAt != null)
        {

            camaraTransform.rotation = Quaternion.Slerp(camaraTransform.rotation, camaraDesiredLookAt.rotation, camaraSpeed * Time.deltaTime);
            camaraDesiredLookAttxt = camaraDesiredLookAt.ToString();
        }
        debugText.text = "LoadingSceneState: " + LoadingSceneStatetxt + "\n"+"look at end screen" +lookAtEndLevScreentxt + "\n"+ "camaraDesiredLookAt: " + camaraDesiredLookAttxt;
    }

    private void LoadLevel(string sceneName)
    {
        Debug.Log("Loading " + sceneName);
    
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(Transform menuTransform)
    {
        if (lookAtEndLevScreen)
        {
            camaraTransform.LookAt(menuTransform.position);
            lookAtEndLevScreen = false;
        }else{
            camaraDesiredLookAt = menuTransform;
        }

        // camaraDesiredLookAt = menuTransform;
    }

    public void Quit(){
        Application.Quit();
    }

    public void LevelEndSscreen(string endState){
        if (endState=="winState")
        {
            levelEndText.text = "You Win!";
        }else if(endState=="lostState"){
            levelEndText.text = "You Lose!";
        }
    }
}
