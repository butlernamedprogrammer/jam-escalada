using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int nextLevelIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag.Contains("Player"))
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
