using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePanel: MonoBehaviour
{
    public GameObject enterPanel;
    void Start()
    {
        enterPanel.SetActive(false);
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            enterPanel.SetActive(true);
        }    
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            enterPanel.SetActive(false);
        }
    }
}
