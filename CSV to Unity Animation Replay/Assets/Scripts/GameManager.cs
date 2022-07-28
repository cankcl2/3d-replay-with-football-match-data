using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Reader.PlayerInfo readerScript;
    public GameObject player;
    public GameObject playerPrefab;
    public List<GameObject> players = new List<GameObject>();
    public GameObject ball;
    public GameObject frameViewer;
    public GameObject timeViewer;
    float timeSinceStartup;
    bool replayPlays = true;
    public GameObject replayButton;

    private void Start()
    {
        player.transform.position = new Vector3(0, 0, 0);
        ball.transform.position = new Vector3(0, 0, 0);
        timeSinceStartup = 0;
        replayButton.SetActive(false);
        SpawnPlayers();
        VisualizationCharacter();
        StartCoroutine(PlayerMovement());
        
    }

    private void Update()
    {
        if (replayPlays)
        {
            //Time viewer setup
            timeSinceStartup += 1 * Time.deltaTime;
            timeViewer.GetComponent<TextMeshProUGUI>().text = "Time: " + (int)timeSinceStartup;
            if ( timeSinceStartup >= (560/25f))
            {
                replayPlays = false;
                replayButton.SetActive(true);
            }
        }
    }

    IEnumerator PlayerMovement()
    {
        
        for (int i = 0; i < Reader.frameList.Count; i++)
        {

            for (int j = 0; j < Reader.playerInfos.Count; j++)
            {
                //Player position updating
                float instantxPos = Reader.playerInfos[j].xPos[i];
                float instantyPos = Reader.playerInfos[j].yPos[i];
                players[j].transform.position = new Vector3(instantxPos, 0, instantyPos);
            }
            //Ball position updating
            float ballxPosInstant = Reader.ballxPos[i];
            float ballyPosInstant = Reader.ballyPos[i];
            float ballzPosInstant = Reader.ballzPos[i];
            ball.transform.position = new Vector3(ballxPosInstant,ballzPosInstant,ballyPosInstant);

            //Frame updating
            frameViewer.GetComponent<TextMeshProUGUI>().text = "Frame: " + Reader.frameList[i].ToString();

            //around 560 frames -> 25fps | 1 frame 0.04 seconds -> Animation takes 23 seconds.!
            yield return new WaitForSeconds(0.04f);
        }
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < 22; i++)
        {
            //Instantiating Players and Adding them to PlayerList
            GameObject preObject = Instantiate(playerPrefab) as GameObject;
            preObject.transform.position = new Vector3();
            players.Add(preObject);
        }
        
    }

    void VisualizationCharacter()
    {

        for (int i = 0; i < players.Count; i++)
        {
            //Naming player

            var name = Reader.playerInfos[i].teamId.ToString();
            var playerNum = Reader.playerInfos[i].playerNum.ToString();
            players[i].name = name + " - " +playerNum;

            //Coloring player
            
            if (Reader.playerInfos[i].teamId == 1)
            {
                var playerRenderer = players[i].GetComponent<Renderer>();
                playerRenderer.material.SetColor("_Color",Color.blue);
            }else if (Reader.playerInfos[i].teamId == 0)
            {
                var playerRenderer = players[i].GetComponent<Renderer>();
                playerRenderer.material.SetColor("_Color", Color.red);
            }

        }
        

    }

    public void LoadLevels(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
