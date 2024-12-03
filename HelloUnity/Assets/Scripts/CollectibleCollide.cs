using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollectibleCollide : MonoBehaviour
{
    public float duration = 20.0f;
    private PlayerMotionController _controlScript;
    private Spawner _spawnScript;
    private Boolean _triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        _controlScript = GameObject.Find("Gecko_A07").GetComponent<PlayerMotionController>();
        if (_controlScript == null) {
            Debug.Log("gecko script is null!");
        }
        _spawnScript = GameObject.Find("MushroomSpawner").GetComponent<Spawner>();
        if (_spawnScript == null)
        {
            Debug.Log("spawner script is null!");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        // confirm collision by player character 
        if (Equals(other.gameObject.name, "Gecko_A07"))
        {
            StartCoroutine(AnimateCollision());
            // Messy solution to avoid doubly counting
            // TODO: Refactor collectible coroutine
            if (!_triggered)
            {
                _triggered = true;
                _controlScript.addPoint(1);
                if (_controlScript._exploreMode)
                {
                    _spawnScript.PlaceObjectRandom();
                }
            }
        }
    }

    IEnumerator AnimateCollision() {
        Debug.Log("Reached animation.");
        Vector3 destination = transform.position + new Vector3(0, 0.5f, 0);
        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        for (float timer = 0; timer < duration; timer += Time.deltaTime) {
            float u = timer / duration;
            transform.position = Vector3.Lerp(startPos, destination, u);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, u);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
