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

using UnityEngine;
using UnityEngine.XR.ARFoundation;

/**
 * Spawns a <see cref="CarBehaviour"/> when a plane is tapped.
 */
public class CarManager : MonoBehaviour
{
    public GameObject CarPrefab;
    public ReticleBehaviour Reticle;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ErrorHandler ErrorHandler;

    public CarBehaviour Car;

    public ScoreKeeperScr scoreKeeper;
    public float minPlaneSize ;
    private int planeScanedPrecentage;
    public GameObject explosionPrefab;

    private void Update()
    {
        planeScanedPrecentage =Mathf.RoundToInt(Mathf.Clamp01(Mathf.Min(Reticle.CurrentPlane.size.x, Reticle.CurrentPlane.size.y) / minPlaneSize) * 100.0f);
        ErrorHandler.setPlaneScanedPrecentage(planeScanedPrecentage);


        if(Reticle.CurrentPlane.size.x > minPlaneSize && Reticle.CurrentPlane.size.y > minPlaneSize)
        {
            ErrorHandler.setPlaneSizeEnough(true);

   
            if (Car == null && WasTapped() && Reticle.CurrentPlane != null)
            {

                ErrorHandler.setCarAvailable(true);
                // Spawn our car at the reticle location.
                var obj = GameObject.Instantiate(CarPrefab);
                Car = obj.GetComponent<CarBehaviour>();
                Car.Reticle = Reticle;
                Car.scoreKeeper=scoreKeeper;
                Car.explosionPrefab=explosionPrefab;
                Car.transform.position = Reticle.transform.position;
                DrivingSurfaceManager.LockPlane(Reticle.CurrentPlane);
            }
        }
        else
        {
            ErrorHandler.setPlaneSizeEnough(false);
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
