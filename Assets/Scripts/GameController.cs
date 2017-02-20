using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UniRx;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    public static GameController inst;

    public void Awake()
    {
        playerData = null;
        if (inst == null) inst = this;
        LoadSavedPlayer();
    }

    private float d = 0;
    void Update () {

	    if (Input.GetKeyDown(KeyCode.Mouse0))
	    {
	        playerData.gold.Apply(1);
	        playerData.passiveGold.Apply(1);
	    }
        d += Time.deltaTime;
        if (d >= 1)
        {
            d = 0;
            playerData.gold.Apply(playerData.passiveGold.count.Value);
        }
    }

    public static string savePath { get { return Application.persistentDataPath + "/player.dat"; } }

    void SavePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream f = File.Open(savePath, FileMode.Create);
        bf.Serialize(f, playerData);
        f.Close();
    }

    void LoadSavedPlayer()
    {
        if (playerData == null) playerData = ReadPlayerFromDisk();
        if (playerData == null)
        {
            playerData = new PlayerData();
            playerData.Inicialization();
        }
    }

    public static PlayerData ReadPlayerFromDisk()
    {
        PlayerData player = null;
        if (File.Exists(savePath))
        {
            FileStream f = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                f = File.Open(savePath, FileMode.Open);
                player = bf.Deserialize(f) as PlayerData;
                f.Close();
            }
            catch (System.Exception e)
            {
                if (f != null && f.CanRead)
                {
                    f.Close();
                }
                Debug.LogError("player deserialization error: " + e.Message);
            }
        }
        return player;
    }

    void OnApplicationPause(bool pause)
    {
        Debug.Log("pause: " + pause.ToString());
        if (playerData == null) return;
        if (pause)
        {
            SavePlayerData();
        }
    }
}
