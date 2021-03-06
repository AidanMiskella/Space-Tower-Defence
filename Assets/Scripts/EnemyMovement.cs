﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float dwellTime = 1f;
    [SerializeField] ParticleSystem goalParticle;

    // Start is called before the first frame update
    void Start() {

        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<Waypoint> path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path) {

        foreach (Waypoint waypoint in path) {

            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
        SelfDestruct();
    }

    private void SelfDestruct() {

        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();

        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }
}
