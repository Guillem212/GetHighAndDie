using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackGround : MonoBehaviour
{

    public float backGroundSize;
    public float parallaxSpeed;

    private Transform CameraTransform;
    public Transform[] layers;

    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    private float lastCameraX;

    void Start()
    {
        CameraTransform = Camera.main.transform;

        lastCameraX = CameraTransform.position.x;

        layers = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++){
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    void Update(){

        float deltaX = CameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);

        lastCameraX = CameraTransform.position.x;

        if(CameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)){
            ScrollLeft();
        }

        if(CameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)){
            ScrollRight();
        }
    }

    private void ScrollLeft(){
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backGroundSize);

        leftIndex = rightIndex;
        rightIndex--;

        if(rightIndex < 0){
            rightIndex = layers.Length -1;
        }
    }

    private void ScrollRight(){
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backGroundSize);

        rightIndex = leftIndex;
        leftIndex++;

        if(leftIndex == layers.Length){
            leftIndex = 0;
        }
    }
}
