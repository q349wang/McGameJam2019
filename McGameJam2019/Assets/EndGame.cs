using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static int survivors = 0;
    public static int hooker = 0;

    public GameObject endGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Lobby" && (hooker == 0 || survivors == 0))
        {
            endGamePanel.SetActive(true);
        }
    }
}
