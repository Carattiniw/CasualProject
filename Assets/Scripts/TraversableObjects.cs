using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TraversableObjects : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private int currentRouteIndex;
    [SerializeField]private float tParam;
    private float speedModifier;
    static bool coroutineAllowed;

    private GameObject player;
    private BoxCollider opening;
    public string playerState;


    private void Start()
    {
        player = GameObject.Find("Player");
        opening = GetComponent<BoxCollider>();
        currentRouteIndex = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }
    void Update()
    {
        playerState = player.GetComponent<PlayerController>().currentStateString;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerState == "Liquid")
        {
            if (coroutineAllowed == true)
            {
                StartCoroutine(FollowRoute(currentRouteIndex));
            }
        }
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
            player.transform.position = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        currentRouteIndex += 1;

        if (currentRouteIndex == routes.Length)
        {
            currentRouteIndex = 0;
            coroutineAllowed = true;
            yield break;
        }
    }
}
