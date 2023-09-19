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

/**
 * Our car will track a reticle and collide with a <see cref="PackageBehaviour"/>.
 */
public class CarBehaviour : MonoBehaviour
{
    public ReticleBehaviour Reticle;
    public float Speed = 1.2f;

    public ScoreKeeperScr scoreKeeper;
    public GameObject explosionPrefab;

    private void Update()
    {
        var trackingPosition = Reticle.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position =
            Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        var Bomb = other.GetComponent<BombBehaviour>();
        if (Package != null)
        {
            Destroy(other.gameObject);
            scoreKeeper.packageCollected();

            // Get all GameObjects in the scene with the specific component.
            BombBehaviour[] BombList = FindObjectsOfType<BombBehaviour>();

            // Loop through each GameObject that has the PackageBehaviour component.
            foreach (BombBehaviour Bombtodel in BombList)
            {
                Destroy(Bombtodel.gameObject);
            }

        }
        else if (Bomb != null)
        {
            Vector3 bombPosition = other.transform.position;
            Destroy(other.gameObject);
            scoreKeeper.bombHit();
            var explotionClone = GameObject.Instantiate(explosionPrefab);
            explotionClone.transform.position = bombPosition;
            
            scoreKeeper.bombHit();
        }
    }
}
