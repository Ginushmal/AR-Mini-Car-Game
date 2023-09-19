using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorHandler : MonoBehaviour
{


    private bool carAvailable;
    private bool planeSizeEnough;

    private string errorString;

    public TextMeshProUGUI errorText;

    private int planeScanedPrecentage;

    // public List<string> errorList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        carAvailable=false;
        planeSizeEnough=false;
        errorText.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!planeSizeEnough){
            errorString="Scan the ground with the camara \n"+planeScanedPrecentage.ToString()+"%";
            errorText.enabled=true;
            errorText.text=errorString;
        }
        else if(!carAvailable){
            errorString="Tap to Spawn the Car";
            errorText.enabled=true;
            errorText.text=errorString;
        }
        else{
            errorString="";
            errorText.enabled=false;
            errorText.text=errorString;
        }
    }

    public void setCarAvailable(bool carAvailable){
        this.carAvailable=carAvailable;
    }
    public void setPlaneSizeEnough(bool planeSizeEnough){
        this.planeSizeEnough=planeSizeEnough;
    }

    public void setPlaneScanedPrecentage(int planeScanedPrecentage){
        this.planeScanedPrecentage=planeScanedPrecentage;
    }
}
