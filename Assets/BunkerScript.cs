﻿using System.Collections;
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
            pc.currentBunker = this;
    }

    public void ChangeBunker()
    {
        menuCanvas.transform.position = menuPosition.position;
        menuCanvas.rotation = Quaternion.Euler(menuPosition.eulerAngles);
        
        playerTransform.position = playerPosition.position;
        playerTransform.rotation = Quaternion.LookRotation(playerPosition.forward, Vector3.up);
        pc.currentBunker = this;
    }

    public void StoreSink(GameObject sink)
    {
        m_storedSink = sink;
        m_storedSink.transform.parent = null;
        m_storedSink.transform.position = m_sinkStorage.position;
        m_storedSink.transform.rotation = m_sinkStorage.rotation;
    }

    public GameObject SwapSink(GameObject sink)
    {
        GameObject temp = m_storedSink;
        m_storedSink = sink;
        m_storedSink.transform.parent = null;
        temp.transform.position = pc.sinkSpawn.position;
        m_storedSink.transform.position = m_sinkStorage.position;
        m_storedSink.transform.rotation = m_sinkStorage.rotation;
        return temp;
    }
}