using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomColor : MonoBehaviour
{
    public static Color  carColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomiseColor(){
        carColor = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
}
