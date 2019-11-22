using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BunkerScript : MonoBehaviour {
    public RectTransform menuCanvas;
    public PlayerController pc;

    [HideInInspector] public GameObject m_storedSink;
    private int m_index;
    [SerializeField] private Transform m_sinkStorage;
    private void Start()
    {

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
