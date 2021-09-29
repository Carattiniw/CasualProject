using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    private int routeToFollow;
    private float tParam;
    private Vector3 playerPosition;
    private float speedModifier;
    private bool coroutineAllowed;

    private void Start()
    {
        routeToFollow = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    private void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(FollowRoute(routeToFollow));
    }
    private IEnumerator FollowRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            playerPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = playerPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToFollow += 1;

        if (routeToFollow > routes.Length - 1)
        {
            coroutineAllowed = true;
        }
    }
}
