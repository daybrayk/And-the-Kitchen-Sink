using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BunkerScript : MonoBehaviour {
    public RectTransform menuCanvas;
    public Transform playerPosition;
    public Transform playerTransform;
    public Transform menuPosition;
    public PlayerController pc;

    [HideInInspector] public GameObject m_storedSink;
    private int m_index;
    [SerializeField] private Transform m_sinkStorage;
    [SerializeField] private BunkerScript[] m_bunkers;
    private void Start()
    {
        // m_bunkers[0].ChangeBunker();
        if (gameObject.name == "HouseBunker")
            pc.CurrentBunker = this;
    }

    public void ChangeBunker()
    {
        menuCanvas.transform.position = menuPosition.position;
        menuCanvas.rotation = Quaternion.Euler(menuPosition.eulerAngles);
        
        playerTransform.position = playerPosition.position;
        playerTransform.rotation = Quaternion.LookRotation(playerPosition.forward, Vector3.up);
        pc.CurrentBunker = this;
    }

    public void StoreSink(GameObject sink)
    {
        if (!sink)
            return;
        m_storedSink = sink;
        m_storedSink.transform.parent = null;
        m_storedSink.transform.position = m_sinkStorage.position;
        m_storedSink.transform.rotation = m_sinkStorage.rotation;
        m_storedSink.GetComponent<LineRenderer>().enabled = false;
    }

    public GameObject SwapSink(GameObject sink)
    {
        GameObject temp = m_storedSink;
        StoreSink(sink);

        return temp;
    }
}
