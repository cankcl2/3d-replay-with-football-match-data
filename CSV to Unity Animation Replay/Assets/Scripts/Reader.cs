using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Reader : MonoBehaviour
{
    public TextAsset textAssetData;
    public static List<PlayerInfo> playerInfos;
    public static List<int> ballxPos, ballyPos, ballzPos;
    public static List<string> frameList;


    void Awake() {ReadCSV();}

    [Serializable]
    public class PlayerInfo
    {
        public int teamId, trackingId, playerNum;
        public List<int> xPos, yPos;
    }
    [Serializable]
    public class BallInfo
    {
        public List<int> xPos, yPos, zPos;
    }


    void ReadCSV()
    {
        //Splits all the data values from the testData by covering it as TextAsset.
        string[] datas = textAssetData.text.Split(new string[] {":"}, StringSplitOptions.RemoveEmptyEntries);
        List<string> playerDatasInLines = new List<string>();
        frameList = new List<string>();
        ballxPos = new List<int>();
        ballyPos = new List<int>();
        ballzPos = new List<int>();
        playerInfos = new List<PlayerInfo>();

        bool isTheFirstTime = true;


        foreach (var data in datas)
        {
            if (data.Length > 50)
            {
                //The data is held by player. We see all the data values here is related to players.
                var playerTransformValuesbyFrame = data.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < playerTransformValuesbyFrame.Length; i++)
                {
                    var eachPlayerTransforms = playerTransformValuesbyFrame[i];
                    PlayerInfo playerInfo;
                    var valuesOfEachPlayers = eachPlayerTransforms.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (isTheFirstTime)
                    {
                        //Initializing players by their class.
                        playerInfo = new PlayerInfo()
                        {
                            teamId = int.Parse(valuesOfEachPlayers[0]),
                            trackingId = int.Parse(valuesOfEachPlayers[1]),
                            playerNum = int.Parse(valuesOfEachPlayers[2]),
                            xPos = new List<int>(),
                            yPos = new List<int>()
                        };
                        //Seperated position values into a list
                        playerInfo.xPos.Add(int.Parse(valuesOfEachPlayers[3]));
                        playerInfo.yPos.Add(int.Parse(valuesOfEachPlayers[4]));
                        playerInfos.Add(playerInfo);
                    }
                    else {
                        //Seperated position values into a list
                        playerInfos[i].xPos.Add(int.Parse(valuesOfEachPlayers[3]));
                        playerInfos[i].yPos.Add(int.Parse(valuesOfEachPlayers[4]));
                    }
                }
                
                isTheFirstTime = false;
            }
            if (data.Length < 10)
            {
                //Adding frames to a list to be used in future.
                frameList.Add(data);
            }
            if (data.Length >10 && data.Length < 50)
            {
                //Seperated ball datas into a list thanks to their x3 position lists.
                var ballDataWithoutSpecialChars = data.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ballDataWithoutSpecialChars.Length; i++)
                {
                    var ballDataPerFrameList = ballDataWithoutSpecialChars[i];
                    var ballDatasSeperated = ballDataPerFrameList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    ballxPos.Add(int.Parse(ballDatasSeperated[0]));
                    ballyPos.Add(int.Parse(ballDatasSeperated[1]));
                    ballzPos.Add(int.Parse(ballDatasSeperated[2]));
                }
                

            }
            
        }
    }
}
