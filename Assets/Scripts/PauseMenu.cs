using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private float pauseTimeScale;
    float originalTimeScale;
    GameObject pauseScreen;
    bool IsPaused;
    void Start()
    {
        originalTimeScale = Time.timeScale;
        pauseTimeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !IsPaused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = pauseTimeScale;
        }
        if (Input.GetKeyDown(KeyCode.P) && IsPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = originalTimeScale;
        }
    }
}
