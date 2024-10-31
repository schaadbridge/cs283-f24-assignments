using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollide : MonoBehaviour
{
    private PlayerMotionController _script; 
    // Start is called before the first frame update
    void Start()
    {
        _script = GameObject.Find("Gecko_A07").GetComponent<PlayerMotionController>();
        if (_script == null) {
            Debug.Log("script is null!");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
        _script.addPoint();
    }
}
