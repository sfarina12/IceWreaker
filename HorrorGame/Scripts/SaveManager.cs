using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    SaveData dataToSave = null;

    [Tooltip("Choose this option olny if you are makeing a new game, it will erease all save data and create new one")]
    public bool isNewGame=false;

    
    [Space,Space,Header("Stuff to save")]
    [Header("Player info")]
    public GameObject player;
    public Interact[] sceneDoors;

    
    [Space,Header("d-Pad info")]
    public open_dPad d_Pad;
    public open_dPad d_PadLight;

    [Header("d-Pad updates")]
    public quest_pickUpUpdate lightUpdate;

    
    [Space,Header("Quests")]
    public quest_dPad dpadQuest;


    [Space,Space,Header("Dev tools")]
    public bool keybinded_loadSave=false;
    public bool ereaseSaveData = false;
    

    private void Awake()
    {
        if (!isNewGame)
        {
        k1:
            if (!ereaseSaveData)
            {
                dataToSave = new SaveData();

                bool isLoaded = Load();

                if (player != null)
                    player.SetActive(false);

                if (isLoaded) unpackData();

                if (player != null)
                    player.SetActive(true);
            }
            else
            {
                Delete();
                ereaseSaveData = false;
                goto k1;
            }
        }
    }

    private void Update()
    {
        if (keybinded_loadSave)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Load();
                unpackData();
            }

            if (Input.GetKeyDown(KeyCode.D)) { Delete(); }
        }
    }

    private void packData()
    {   
        dataToSave.playerPosition = player.transform.position;

        int i = 0;
        dataToSave.doorsStoredValues.Clear();

        foreach (Interact doorInteract in sceneDoors)
        {
            dataToSave.doorsStoredValues.Add(doorInteract.isOn);
            i++;
        }

        dataToSave.isDPadEnabled = d_Pad.enabled;

        dataToSave.isLightUpdate = !lightUpdate.enabled ? true : false;

        dataToSave.endQuest_dpad = dpadQuest.questEnded;
    }

    private void unpackData()
    {
        player.transform.position = dataToSave.playerPosition;

        int i = 0;
        foreach (bool value in dataToSave.doorsStoredValues) 
        {
            sceneDoors[i].isOn = value;
            sceneDoors[i].forceAnimationStateUpdate = true;
            i++;
        }

        d_Pad.enabled = dataToSave.isDPadEnabled;
        d_PadLight.enabled = dataToSave.isDPadEnabled;

        if (dataToSave.isLightUpdate) lightUpdate.performeUpdate(false);

        dpadQuest.questEnded = dataToSave.endQuest_dpad;
        if(dataToSave.endQuest_dpad) dpadQuest.destroyDPadProp();
    }

    public void Save() {
        packData();

        string path = Application.persistentDataPath;

        XmlSerializer serializer =new XmlSerializer(typeof(SaveData));
        FileStream stream = new FileStream(path+"/"+dataToSave.saveName+".ice",FileMode.Create);
        
        serializer.Serialize(stream, dataToSave);
        stream.Close();
    }
    public bool Load() {
        string path = Application.persistentDataPath;

        if (System.IO.File.Exists(path + "/" + dataToSave.saveName + ".ice"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            FileStream stream = new FileStream(path + "/" + dataToSave.saveName + ".ice", FileMode.Open);

            dataToSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            return true;
        }
        return false;
    }
    public void Delete() {
        string path = Application.persistentDataPath;

        if (System.IO.File.Exists(path + "/wrecker.ice"))
            File.Delete(path + "/wrecker.ice");
    }
}

public class SaveData
{
    public string saveName = "wrecker";
    
    public Vector3 playerPosition;
    public List<bool> doorsStoredValues;
    public bool isDPadEnabled;
    public bool isLightUpdate;
    public bool endQuest_dpad;

    public SaveData() { doorsStoredValues = new List<bool>(); }
}
