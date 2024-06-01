using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Menu : MonoBehaviour {

    [SerializeField] private Canvas Pause_Canvas;
    [SerializeField] private Canvas ATH_Canvas;
    [SerializeField] private CinemachineVirtualCamera CamJoueur;
    [SerializeField] private CinemachineVirtualCamera CamPause;

    public void Start_Level() {

        SceneManager.LoadScene("SampleScene");
    }

    public void Quit_Game() {

        Time.timeScale = 1f;

        Application.Quit();
    }

    public void Pause_Game() {

        if (Pause_Canvas != null) {

            Pause_Canvas.gameObject.SetActive(true);

            if (ATH_Canvas != null) {

                ATH_Canvas.gameObject.SetActive(false);
            }

            CamJoueur.Priority = 0;
            CamPause.Priority = 10;

            Time.timeScale = 0f;
        }
    }


    public void Resume_Game() {

        if (Pause_Canvas != null) {

            Pause_Canvas.gameObject.SetActive(false);

            if (ATH_Canvas != null) {

                ATH_Canvas.gameObject.SetActive(true);
            }

            CamJoueur.Priority = 10;
            CamPause.Priority = 0;

            Time.timeScale = 1f;
        }
    }

    public void Start_Menue() {

        Time.timeScale = 1f;

        CamJoueur.Priority = 10;
        CamPause.Priority = 0;

        SceneManager.LoadScene("MainMenu");
    }
}
