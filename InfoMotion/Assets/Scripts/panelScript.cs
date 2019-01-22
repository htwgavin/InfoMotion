using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class panelScript : MonoBehaviour
{

    public GameObject ScrollPanel;

    public GameObject MainMenu;

    public GameObject ButtonDown;
    public GameObject GiveControlPanel;
    public GameObject TakeControlPanel;
    public GameObject Drive;
    public Transform ButtonMovies;
    public Transform ButtonGames;
    public Transform ButtonDownThePanel;

    public GameObject MusicPanel;
    public GameObject MainEverything;

    // public float sss;

    void Update()
    {
        //mit knopfdruck übergabe und annahme vom fahrzeug 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateAttentionPanel();

            Debug.Log("Space key was pressed.");
        }
    }

    public void showhideNavigationPanel()
    {
        ScrollPanel.SetActive(!ScrollPanel.activeSelf);
        ButtonDown.SetActive(!ButtonDown.activeSelf);
        if (!ScrollPanel.activeSelf)
        {

           // MainMenu.transform.localScale = new Vector2(1.09864f, 1.46f);
            MainMenu.transform.position = new Vector2(MainMenu.transform.position.x, MainMenu.transform.position.y+55f);

        }
        else
        {

            //MainMenu.transform.localScale = new Vector2(1.09864f, 0.9887757f);
            MainMenu.transform.position = new Vector2(MainMenu.transform.position.x, MainMenu.transform.position.y -55f);
        }

    }



    public void deactivateButtons()
    {
        if (ButtonGames.GetComponent<Button>().IsInteractable() == true)
        {
            ButtonGames.GetComponent<Button>().interactable = false;
            ButtonMovies.GetComponent<Button>().interactable = false;
            ButtonDownThePanel.GetComponent<Button>().interactable = false;
            TakeControlPanel.SetActive(false);

            checkIfScrollPanelAktive();

        }
        else //Else make it interactable
        {
            ButtonGames.GetComponent<Button>().interactable = true;
            ButtonMovies.GetComponent<Button>().interactable = true;
            ButtonDownThePanel.GetComponent<Button>().interactable = true;
            GiveControlPanel.SetActive(false);
        }
    }

    public void activateButtons()
    {
        if (ButtonGames.GetComponent<Button>().IsInteractable() == true)
        {
            ButtonGames.GetComponent<Button>().interactable = false;
            ButtonMovies.GetComponent<Button>().interactable = false;
            ButtonDownThePanel.GetComponent<Button>().interactable = false;
            GiveControlPanel.SetActive(false);

            checkIfScrollPanelAktive();

        }
        else //Else make it interactable
        {
            ButtonGames.GetComponent<Button>().interactable = true;
            ButtonMovies.GetComponent<Button>().interactable = true;
            ButtonDownThePanel.GetComponent<Button>().interactable = true;
            TakeControlPanel.SetActive(false);
        }
    }


    public void checkIfScrollPanelAktive()
    {
        if (ScrollPanel.activeSelf )
        {
            ScrollPanel.SetActive(false);
            ButtonDown.SetActive(true);


        }
       
    }

    public void activateAttentionPanel()
    {
        if(ButtonGames.GetComponent<Button>().IsInteractable() == false)
        {
            GiveControlPanel.SetActive(true);
        }
        else{

            TakeControlPanel.SetActive(true);
        }
      

    }


    public void showhideMusicPanel()
    {
        MainEverything.SetActive(!MainEverything.activeSelf);

        MusicPanel.SetActive(!MusicPanel.activeSelf);

    }

	public void loadGameScene() {
		SceneManager.LoadScene (2, LoadSceneMode.Single);
	}

}
