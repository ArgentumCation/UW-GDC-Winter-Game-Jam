using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController red;
    public PlayerController blue;
    public List<AudioClip> songs;

    public float speed;
    public float scale = 5f;
    public float zoomSpeed;
    private Camera camera;
    public int song = 0;

    private AudioSource audioSource;

    // Update is called once per frame
    private void Start()
    {
        camera = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float redMaxX = red.bodies.Max(x => x.transform.position.x);
        float redMaxY = red.bodies.Max(x => x.transform.position.y);
        float blueMaxX = blue.bodies.Max(x => x.transform.position.x);
        float blueMaxY = blue.bodies.Max(x => x.transform.position.y);

        float redMinX = red.bodies.Min(x => x.transform.position.x);
        float redMinY = red.bodies.Min(x => x.transform.position.y);
        float blueMinX = blue.bodies.Min(x => x.transform.position.x);
        float blueMinY = blue.bodies.Min(x => x.transform.position.y);

        float maxX = redMaxX > blueMaxX ? redMaxX : blueMaxX;
        float minX = redMinX < blueMinX ? redMinX : blueMinX;
        float maxY = redMaxY > blueMaxY ? redMaxY : blueMaxY;
        float minY = redMinY < blueMinY ? redMinY : blueMinY;

        Vector3 target = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f,
            transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, speed);
        Vector2 minVect = new Vector2(minX, minY);
        Vector2 maxVect = new Vector2(maxX, maxX);
        float targetSize = ((maxVect - minVect).magnitude + scale) / 4.0f;
        camera.orthographicSize =
            Mathf.Lerp(camera.orthographicSize, targetSize, zoomSpeed);

        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.volume = audioSource.volume <= 0.001f ? 1f : 0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           // SceneManager.LoadScene("TitleScreen");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Quitting Game");
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            song = (song + 1) % songs.Count;
            audioSource.clip = songs[song];
            audioSource.Play();
        }
    }
}