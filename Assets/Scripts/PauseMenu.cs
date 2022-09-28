using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private float pauseTimeScale;
    float originalTimeScale;
    public GameObject pauseScreen;
    public bool IsPaused;
    void Start()
    {
        originalTimeScale = Time.timeScale;
        pauseTimeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) )
        {
            Time.timeScale = IsPaused ? originalTimeScale : pauseTimeScale;
            IsPaused = !IsPaused;
            pauseScreen.SetActive(IsPaused);
        }
        
    }
}
