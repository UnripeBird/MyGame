using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
[System.Serializable]
public class totalData
{
    public GameObject gm;
    public GameObject target;
    public GameObject Spawnpull;
}
public class saveload : MonoBehaviour {

    public static saveload sl;
    public totalData t;
    // Use this for initialization

    void Awake()
    {
        sl = this;
    }

	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void save()
    {
        //SaveMap();
        Debug.Log("save");
    }
    public void load()
    {

    }

    public string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }

        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }
    /*
    void LoadMap(string str)
    {
        bool _fhile_check = File.Exists(pathForDocumentsFile(Application.persistentDataPath + "/" + str + ".dat"));

        BinaryFormatter _binary_formatter = new BinaryFormatter();

        FileStream filestream = File.Open((Application.persistentDataPath + "/" + str + ".dat"), FileMode.Open);

        md = (mapdata)_binary_formatter.Deserialize(filestream);

        filestream.Close();
    }
    */

    void SaveMap()
    {
        BinaryFormatter _binary_formatter = new BinaryFormatter();
        FileStream filestream = File.Create(Application.persistentDataPath + "/TestSave.dat");
        Debug.Log(Application.persistentDataPath + "/TestSave.dat");
        _binary_formatter.Serialize(filestream, t);
        filestream.Close();
    }
    /*
    void CreateMap()
    {
        //SaveMap();
        md = new mapdata();
        LoadMap("TestSave");
        basicmap();
        blockmap();
    }
    */
}
