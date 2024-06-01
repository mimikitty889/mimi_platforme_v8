using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Menu : MonoBehaviour {

    [SerializeField] private Canvas Pause_Canvas;
    [SerializeField] private Canvas ATH_Canvas;
    [SerializeField] private CinemachineVirtualCamera CamJoueur;
    [SerializeField] private CinemachineVirtualCamera CamPause;
    [SerializeField] private Transform Player;
    [SerializeField] private TextMeshProUGUI Timer_display;

    public void Start_Level() {

        SceneManager.LoadScene("SampleScene");
    }

    public void Quit_Game() {

        Application.Quit();
    }

    public void UptadeTime() {

       int sec = Mathf.FloorToInt(Player.GetComponent<player_moveset>().Get_Time());

        Debug.Log(sec);

        Timer_display.text = "time " + sec;
    }

    public void Pause_Game() {

        if (Pause_Canvas != null) {

            Pause_Canvas.gameObject.SetActive(true);

            if (ATH_Canvas != null) {

                ATH_Canvas.gameObject.SetActive(false);
            }

            Player.GetComponent<player_moveset>().OnFreeze();

            CamJoueur.Priority = 0;
            CamPause.Priority = 10;
        }
    }


    public void Resume_Game() {

        if (Pause_Canvas != null) {

            Pause_Canvas.gameObject.SetActive(false);

            if (ATH_Canvas != null) {

                ATH_Canvas.gameObject.SetActive(true);
            }

            Player.GetComponent<player_moveset>().OffFreeze();

            CamJoueur.Priority = 10;
            CamPause.Priority = 0;
        }
    }

    public void Start_Menue() {


        CamJoueur.Priority = 10;
        CamPause.Priority = 0;

        SceneManager.LoadScene("MainMenu");
    }
}
