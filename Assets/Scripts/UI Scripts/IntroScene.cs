using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroScene : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject ConfirmUI;
    private double videoLength;
    private float startTime;
    public float padTime;
    public float confirmTimer;
    private float confirmPopTime;

    private void Start()
    {
        videoLength = this.gameObject.GetComponent<VideoPlayer>().length;
        startTime = Time.time;
    }

    void Update()
    {
        if (ConfirmUI.gameObject.activeSelf && (confirmTimer < Time.time - confirmPopTime) && !(Input.GetKeyDown(KeyCode.Escape)))
        {
            ConfirmUI.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ConfirmUI.gameObject.activeSelf && (confirmTimer >= Time.time - confirmPopTime))
            {
                print("Video skipped");
                ConfirmUI.gameObject.SetActive(false);
                EndVideo();
            }
            else
            {
                ConfirmUI.gameObject.SetActive(true);
                confirmPopTime = Time.time;
            }
        }

        if (videoLength < Time.time - startTime - padTime)
        {
            EndVideo();
        }

    }

    void EndVideo()
    {
        this.gameObject.SetActive(false);
        LoadingCanvas.SetActive(true);
    }
}
