using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BunkerScript : MonoBehaviour {
    public RectTransform menuCanvas;
    public Transform playerPosition;
    public Transform playerTransform;
    public Transform menuPosition;
    private int m_index;
    [SerializeField] private BunkerScript[] m_bunkers;
    private void Start()
    {
       // m_bunkers[0].ChangeBunker();
    }

    public void ChangeBunker()
    {
        menuCanvas.transform.position = menuPosition.position;
        menuCanvas.rotation = Quaternion.Euler(menuPosition.eulerAngles);
        
        playerTransform.position = playerPosition.position;
        playerTransform.rotation = Quaternion.LookRotation(playerPosition.forward, Vector3.up);
    }
}
