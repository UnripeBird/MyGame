using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
public class Data
{
    public string index = string.Empty;
    public string position = string.Empty;
}

public class xmlscript : MonoBehaviour
{
    XmlDocument xml;                        // Xml      파싱 준비 
    string m_sCheckDevice = "";             // 디바이스 체크
    string m_sXmlPath = "";                 // Xml 저장 위치
    TextAsset m_textAsset = null;           // Device가 Android일 경우에 이걸로 로드한다.
    StreamReader m_sr = null;               // Device가 IOS일 경우에 이걸로 로드한다.
    XmlNodeList m_xnList;                   // 로드할 Xml Node에 접근한다.
    string m_sXmlValue = "";                // Xml 파일에서 Parsing해서 값을 저장 한다.

    //XmlDocument xml = new XmlDocument();
    //string fileName;
    int number = 0;

    int index = 1001;
    Vector3 v = new Vector3(1, 2, 3);

    void Start()
    {
        {
            xml = new XmlDocument();
#if UNITY_EDITOR
            // PC
            m_sCheckDevice = "PC";
            m_sXmlPath = Application.dataPath + "/Resources" + "/" + "TestXml01.xml";       // Asset 폴더 하위 Resources 폴더에 있어야 한다.(PC 버전과 같음)
            xml.Load(m_sXmlPath);                                                           // Xml을 읽는다.
#elif UNITY_ANDROID
//AndRoid
        m_sCheckDevice = "Android";
        m_textAsset = (TextAsset)Resources.Load("TestXml01", typeof(TextAsset));        // Asset 폴더 하위 Resources 폴더에 있어야 한다.(PC 버전과 같음)
        xml.LoadXml(m_textAsset.text);                                                  // Xml을 읽는다.
#endif

            //xmlLoad();            
        }

        //Debug.Log(m_sXmlPath);
        
        //fileName = Application.dataPath + "/Data/sample.xml";
        //xmlSave();
        //xmlLoad();
        
    }

    void xmlSave()
    {
        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        //Root Node
        XmlNode rootNode = xml.CreateNode(XmlNodeType.Element, "Data", string.Empty);
        xml.AppendChild(rootNode);

        for (int i = 0; i < 1; i++)
        {
            //child Node
            XmlNode childNode = xml.CreateNode(XmlNodeType.Element, "record", string.Empty);
            rootNode.AppendChild(childNode);

            //child element

            XmlElement name = xml.CreateElement("index");
            name.InnerText = index.ToString();
            childNode.AppendChild(name);

            XmlElement recordTime = xml.CreateElement("position");
            recordTime.InnerText = v.ToString();
            childNode.AppendChild(recordTime);
        }

        xml.Save(m_sXmlPath);
    }

    void xmlLoad()
    {

        XmlNodeList list = xml.SelectNodes("Data/record");
        Data[] recordData = new Data[list.Count];

        //Read elements
        foreach (XmlNode recordNode in list)
        {
            recordData[number] = new Data();
            recordData[number].index = recordNode["index"].InnerText;
            recordData[number].position = recordNode["position"].InnerText.ToString();
            number++;
        }

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(recordData[i].index);
            Debug.Log(recordData[i].position);
            Debug.Log("---------------------------");
        }
    }
    /*
    void OnGUI()
    {
        GUILayout.Label("Device : " + m_sCheckDevice);
        GUILayout.Label("m_sXmlValue : " + m_sXmlValue);
    }
    */
}

/*using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
public class Main : MonoBehaviour {
    XmlDocument xml;                        // Xml      파싱 준비 
    string m_sCheckDevice = "";             // 디바이스 체크
    string m_sXmlPath = "";                 // Xml 저장 위치
    TextAsset m_textAsset = null;           // Device가 Android일 경우에 이걸로 로드한다.
    StreamReader m_sr = null;               // Device가 IOS일 경우에 이걸로 로드한다.
    XmlNodeList m_xnList;                   // 로드할 Xml Node에 접근한다.
    string m_sXmlValue = "";                // Xml 파일에서 Parsing해서 값을 저장 한다.

 // Use this for initialization
 void Start () {
        xml = new XmlDocument();
#if UNITY_EDITOR
// PC
        m_sCheckDevice = "PC";
        m_sXmlPath = Application.dataPath + "/Resources" + "/" + "TestXml01.xml";       // Asset 폴더 하위 Resources 폴더에 있어야 한다.(PC 버전과 같음)
        xml.Load(m_sXmlPath);                                                           // Xml을 읽는다.
#elif UNITY_ANDROID
//AndRoid
        m_sCheckDevice = "Android";
        m_textAsset = (TextAsset)Resources.Load("TestXml01", typeof(TextAsset));        // Asset 폴더 하위 Resources 폴더에 있어야 한다.(PC 버전과 같음)
        xml.LoadXml(m_textAsset.text);                                                  // Xml을 읽는다.

#elif UNITY_IOS
//IOS
  m_sCheckDevice = "IOS";
  m_sr = new StreamReader(Application.dataPath + "/../TestXml01.xml");            // 이 폴더 경로의 경우 아래의 Xcode의 경로이다. XCode 최상위 폴더에 Xml 파일을 스샷과 같이 드랍한다.
  m_sXmlPath = m_sr.ReadToEnd();
  xml.LoadXml(m_sXmlPath);
#endif
        m_xnList = xml.SelectNodes("/Sheet1/Row");                                      // 접근할 노드
        foreach (XmlNode xn in m_xnList)
        {   
            m_sXmlValue = xn["a"].InnerText;                                            // <a>의 값을 읽어 온다. 
        }
    }
 
 // Update is called once per frame
 void Update () {
 
 }
    void OnGUI()
    {
        GUILayout.Label("Device : " + m_sCheckDevice);
        GUILayout.Label("m_sXmlValue : " + m_sXmlValue);
    }
} 
[출처] Unity에서 Android, IOS Build시 XML Parsing.|작성자 삽질과 하루를

    */