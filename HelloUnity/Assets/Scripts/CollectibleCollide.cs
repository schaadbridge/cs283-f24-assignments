using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollectibleCollide : MonoBehaviour
{
    public float duration = 20.0f;
    private PlayerMotionController _script; 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        _script = GameObject.Find("Gecko_A07").GetComponent<PlayerMotionController>();
        if (_script == null) {
            Debug.Log("gecko cript is null!");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        _script.addPoint();
        StartCoroutine(AnimateCollision());
    }

    IEnumerator AnimateCollision() {
        Vector3 destination = transform.position + new Vector3(0, 0.5f, 0);
        Vector3 start = transform.position;
        for (float timer = 0; timer < duration; timer += Time.deltaTime) {
            float u = timer / duration;
            transform.position = Vector3.Lerp(start, destination, u);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, u);
            Debug.Log(transform.localScale + " u: " + u + " timer: " + timer);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}