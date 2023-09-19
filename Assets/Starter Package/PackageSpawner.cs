/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PackageSpawner : MonoBehaviour
{
    public DrivingSurfaceManager DrivingSurfaceManager;
    public PackageBehaviour Package;
    public GameObject PackagePrefab;

    public GameObject BombPrefab;

    public GameObject reticle;
    public float SpawnRadius = 2.0f; // Adjust the value as needed.

    public int packageCount;
    public PackageBehaviour awvaiPackag=null;
    private List<GameObject> carObjlist = new List<GameObject>();

    private bool functionCalled = false;
    private float timeSinceLastCall = 0f;

    private void Start() {
        packageCount = -1;
        // List of object to consider when spawning the package , it will not spawn the package on top of these objects 
        carObjlist.Add(reticle);
    }

    public static Vector3 RandomInTriangle(Vector3 v1, Vector3 v2)
    {
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);
        if (v + u > 1)
        {
            v = 1 - v;
            u = 1 - u;
        }

        return (v1 * u) + (v2 * v);
    }



    public Vector3 FindRandomLocation(ARPlane plane,List<GameObject> checkObjects)
{
    // Get the mesh and triangles from the ARPlaneMeshVisualizer component.
    var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
    var triangles = mesh.triangles;
    Vector3 randomInTriangle;
    Vector3 randomPoint;
    bool safe = true;
    int errorCount = 0;

     do
    {
        // Select a random triangle from the mesh.
        var triangle = triangles[(int)Random.Range(0, triangles.Length - 1)] / 3 * 3;

        // Get the vertices from the mesh.
        var vertices = mesh.vertices;

        // Keep generating random points until we find one outside the spawn radius.

   
        randomInTriangle = RandomInTriangle(vertices[triangle], vertices[triangle + 1]);
        randomPoint = plane.transform.TransformPoint(randomInTriangle);

        foreach (GameObject obj in checkObjects)
        {
            if (Vector3.Distance(randomPoint, obj.transform.position) < SpawnRadius)
            {
                safe = false;
                break;
            }
        }

        errorCount++;
        if (errorCount > 25)
        {
            Debug.LogError("Error finding a safe spawn point on plane.");
            break;
        }

    }
    while (!safe);

    return randomPoint;
}




    public void SpawnPackage(ARPlane plane, GameObject objectPrefab)
    {
        var packageClone = GameObject.Instantiate(objectPrefab);
        packageClone.transform.position = FindRandomLocation(plane,carObjlist);
        // carObjlist.Add(packageClone);

        Package = packageClone.GetComponent<PackageBehaviour>();
    }

    private void Update()
    {
        awvaiPackag = FindObjectOfType<PackageBehaviour>();
        var lockedPlane = DrivingSurfaceManager.LockedPlane;
        if (lockedPlane != null)
        {
            if (awvaiPackag== null)
            {
                functionCalled = true;
                SpawnPackagesftBombs(lockedPlane);
            }

            var packagePosition = Package.gameObject.transform.position;
            packagePosition.Set(packagePosition.x, lockedPlane.center.y, packagePosition.z);
        }

        if (functionCalled)
        {
            // Reset the timer and set functionCalled to false.
            timeSinceLastCall = 0f;
            functionCalled = false;
        }else
        {
            // Increment the timer.
            timeSinceLastCall += Time.deltaTime;
            // Check if 30 seconds have passed without the function being called.
            if (timeSinceLastCall >= 20f)
            {
                // Call the other function here.
                SpawnPackagesftBombs(lockedPlane);
                // Reset the timer and set functionCalled to false.
                timeSinceLastCall = 0f;
                functionCalled = false;
            }
        }
    }
    public void YourFunction()
    {
        // Your function's logic here.
        functionCalled = true;
    }

    private void SpawnPackagesftBombs(ARPlane lockedPlane){
        SpawnPackage(lockedPlane, PackagePrefab);
                packageCount++;
                if (BombPrefab != null){
                    int bombCount = Random.Range(0, 4);
                    for (int i = 0; i < bombCount; i++)
                    {
                        SpawnPackage(lockedPlane, BombPrefab);
                    }
                
                }
    }
}
