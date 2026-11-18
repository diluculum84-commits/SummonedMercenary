using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public EntranceTrigger currentTrigger;
    public void ConfirmInteraction()
    {
        currentTrigger.EnterBuilding();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
