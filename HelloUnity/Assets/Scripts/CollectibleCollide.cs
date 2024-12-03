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
            Debug.Log("gecko script is null!");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        // confirm collision by player character 
        if (System.String.Equals(other.gameObject.name, "Gecko_A07"))
        {
            _script.addPoint(1);
            StartCoroutine(AnimateCollision());
        }
    }

    IEnumerator AnimateCollision() {
        Debug.Log("Reached animation.");
        Vector3 destination = transform.position + new Vector3(0, 0.5f, 0);
        Vector3 start = transform.position;
        for (float timer = 0; timer < duration; timer += Time.deltaTime) {
            float u = timer / duration;
            transform.position = Vector3.Lerp(start, destination, u);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, u);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
