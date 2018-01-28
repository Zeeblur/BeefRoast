        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.SceneManagement;

    public class IntroCont : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("level_01_Laser");
            }
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("level_01_Laser");
            }
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("level_01_Laser");
            }

            if (Input.GetKey(KeyCode.W))
            {
                SceneManager.LoadScene("level_01_Laser");
            }

            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("level_01_Laser");
        }

            if (Input.GetKey(KeyCode.Q))
            {
                SceneManager.LoadScene("level_01_Laser");
        }
    }
        }