﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] content;
    [SerializeField]
    private Transform cameraPos;
    private Vector3 SpawnPosition;

    [Header("Interface settings")]
    [SerializeField]
    private float distanceFromCam = 5;
    private bool isActive;
    [SerializeField]
    private float DistContent = 3;
    [SerializeField]
    private float contentSpeed = 4;
    private float waitTime = 1;

    private AudioSource audioSrc;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isActive) {
            OpenInterface();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            CloseInterface();
        }
    }

    public void OpenInterface() {
        audioSrc.PlayOneShot(audioSrc.clip);
        SpawnPosition = cameraPos.forward * distanceFromCam + cameraPos.position;
        transform.position = new Vector3(SpawnPosition.x, cameraPos.position.y, SpawnPosition.z);
        //transform.LookAt(cameraPos);
        transform.LookAt(2 * transform.position - cameraPos.position);

        for (int i = 0; i < content.Length; i++) {
            StartCoroutine("SpawnContent", i);
            isActive = true;
        }
    }

    public void CloseInterface() {
        Debug.Log("close");
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);
        isActive = false;
    }


    IEnumerator SpawnContent(int i) {
        GameObject prefab = Instantiate(content[i], new Vector3(0, (((content.Length - 1) * DistContent / 2) + 10), 0), Quaternion.identity);
        prefab.transform.SetParent(transform, false);

        float elapsedTime = 0;
        while (elapsedTime < contentSpeed) {
            prefab.transform.position = Vector3.Lerp(prefab.transform.position, transform.position - new Vector3(0, (i* DistContent - ((content.Length - 1) * DistContent / 2)), 0), (elapsedTime / contentSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;


        //Rotate opject 180 gaden,
        //Laat 
    }
}