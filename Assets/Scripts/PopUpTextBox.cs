using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextBox : MonoBehaviour
{
    private BoxCollider trigger;
    [SerializeField]private TextMeshProUGUI popupTMP;
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        popupTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
       if(other.gameObject.CompareTag("Player"))
        {
            popupTMP.text = message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            popupTMP.text = "";
        }
    }
}
