using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftyAnimation : MonoBehaviour
{
    public ShiftyAnimation Instance { get; private set; }
    GameObject shifty;
    SkinnedMeshRenderer mesh;
    public int currentIndex =10;
    public int lastIndex;
    public float currentWeight = 0f;
    public bool weightBool = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mesh = GetComponent<SkinnedMeshRenderer>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*private void Update()
    {
        shifty = GameObject.Find("Player");
        mesh = shifty.GetComponent<SkinnedMeshRenderer>();
        switch(weightBool)
        { 
            case true:
                currentWeight += Time.deltaTime;
                break;
            case false:
                currentWeight -= Time.deltaTime;
                break;
        }
        Mathf.Clamp(currentWeight, 0f, 100f);     
        if(currentWeight == 0f)
        {
            weightBool = !weightBool;
        }
        mesh.SetBlendShapeWeight(currentIndex, currentWeight);

        currentWeight = Mathf.PingPong(0, 100);
    }*/

    private void Update()
    {
        mesh.SetBlendShapeWeight(lastIndex, 0f);
        mesh.SetBlendShapeWeight(currentIndex, 100f);
        if (currentIndex > 6)
        {
            lastIndex = currentIndex;
            currentIndex--;
        }
        else if (currentIndex == 6)
        {
            lastIndex = 6;
            currentIndex = 10;
        }
    }
    public void SetAnimation(int targetIndex)
    {
        weightBool = !weightBool;
       
        mesh.SetBlendShapeWeight(targetIndex, currentWeight);
    }
}

