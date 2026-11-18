using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntranceTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enterPanel;
    public GameObject interactionKey;
    private bool isPlayerNearby;
    public string interactionMessage;
    public Text enterText;
    public Transform destination;
    public Transform player;
    public InteractionManager interactionManager;
    public CameraController cameraController;
    public string buildingName; //어떤 건물인가?
    public float cameraMinX;
    public float cameraMaxX;
    public float cameraMinY;
    public float cameraMaxY;
    public float cameraSize;
    
    public void EnterBuilding()
    {
        isPlayerNearby = false;
        interactionKey.SetActive(false);
        PlayerGameData.isInsideBuilding = true;
        PlayerGameData.isInsideBuilding = true;
        PlayerGameData.currentBuilding = buildingName;

        player.position = destination.position;
        SetBuildingCamera();
    }
    public void SetBuildingCamera()
    {
        cameraController.SetCameraSize(cameraSize);
        cameraController.SetCameraBounds(cameraMinX, cameraMinY, cameraMaxX, cameraMaxY);
        cameraController.ResetCameraPosition();
    }
    public void RestoreBuilding()
    {
        if (PlayerGameData.isInsideBuilding && PlayerGameData.currentBuilding == buildingName)
        {
            SetBuildingCamera();
        }
    }
    public void ClosePanel()
    {
        enterPanel.SetActive(false);
    }
    private void Start()
    {
        enterPanel.SetActive(false);
        interactionKey.SetActive(false);
        //RestoreBuilding();
    }
    
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            enterText.text = interactionMessage;
            enterPanel.SetActive(true);
            interactionManager.currentTrigger = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionKey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionKey.SetActive(false);
            enterPanel.SetActive(false);
        }
    }
}
