using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugSrc : MonoBehaviour
{
    public ReticleBehaviour Reticle;
    public TextMeshProUGUI debugText;

    private string debugString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugString = "Reticle Size : " + Reticle.CurrentPlane.trackableId.ToString() + " : X=" + Reticle.CurrentPlane.size.x.ToString() + ", y=" + Reticle.CurrentPlane.size.y.ToString();
        debugText.text = debugString;
    }
}
