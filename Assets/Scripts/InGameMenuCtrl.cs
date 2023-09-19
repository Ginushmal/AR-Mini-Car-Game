using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameMenuCtrl : MonoBehaviour
{
    public ScoreKeeperScr scoreKeeper;
    public TextMeshProUGUI debugtxt;
    // Reference to the ARPlaneManager
    private ARPlaneManager arPlaneManager;
    [SerializeField]
    ARSession arSession;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goHome(){
        scoreKeeper.lose();
    }

    public void restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        // Save the current active scene name
        string currentLevelName = SceneManager.GetActiveScene().name;

        // Reset the ARSession
        // ARSession arSession = gameObject.GetComponent<ARSession>();
        if (arSession != null)
        {
            arSession.Reset();
        }
        else
        {
            Debug.LogWarning("ARSession component not found on the GameManager.");
            debugtxt.text="ARSession component not found on the GameManager.";
        }

        

        // Wait for a short time to ensure the AR session is properly reset
        yield return new WaitForSeconds(1.0f);

        // Reload the current scene
        SceneManager.LoadScene(currentLevelName);
    }
}
