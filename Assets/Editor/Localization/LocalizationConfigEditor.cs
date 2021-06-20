using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.IO.Compression;
using Ionic.Zip;

[System.Serializable]
public class LocalizationConfig
{
    [SerializeField]
    private string m_fileNameOutput;
    [SerializeField]
    private List<string> m_cellForKey;
    [SerializeField]
    private string m_beginData;
    [SerializeField]
    private List<string> m_langageColumns;
    private string m_pathGen;
    [SerializeField]
    private string m_pathExcelFile;
    private string m_pathConfig = Directory.GetCurrentDirectory() + "\\Localization\\Debug\\config.ini";
    private string m_pathPahIni = Directory.GetCurrentDirectory() + "\\Localization\\Debug\\path.ini";

    public LocalizationConfig()
    {
    }

    public LocalizationConfig(string fileNameOutput, List<string> cellForKey, string beginData, List<string> langageColumns, string path, string pathConfig)
    {
        m_fileNameOutput = fileNameOutput;
        m_cellForKey = new List<string>(cellForKey);
        m_beginData = beginData;
        m_langageColumns = new List<string>(langageColumns);
        m_pathGen = path;
        m_pathConfig = pathConfig;
    }
    public void ReadConfig()
    {
        using (StreamReader sr = new StreamReader(m_pathConfig), sr2 = new StreamReader(m_pathPahIni))
        {
            sr.ReadLine();
            m_fileNameOutput = sr.ReadLine().Split('=')[1];
            m_cellForKey = sr.ReadLine().Split('=')[1].Split(',').ToList();
            m_beginData = sr.ReadLine().Split('=')[1];
            m_langageColumns = sr.ReadLine().Split('=')[1].Split(',').ToList();
            m_pathGen = sr.ReadLine().Split('=')[1];
            m_pathExcelFile = sr2.ReadLine();
        }
    }
    public void WriteConfig()
    {
        using (StreamWriter sw = new StreamWriter(m_pathConfig), sw2 = new StreamWriter(m_pathPahIni))
        {
            sw.WriteLine("[OPTIONS]");
            sw.WriteLine("FileNameOutput=" + m_fileNameOutput);
            sw.WriteLine("CellForKey=" + string.Join(",", m_cellForKey.ToArray()));
            sw.WriteLine("BeginData=" + m_beginData);
            sw.WriteLine("LangageColumn=" + string.Join(",", m_langageColumns.ToArray()));
            sw.WriteLine("Path=Assets\\Resources\\Traduction\\");
            sw2.Write(m_pathExcelFile);
            //m_fileNameOutput = sr.ReadLine().Split('=')[1];
            //m_cellForKey = sr.ReadLine().Split('=')[1].Split(',').ToList();
            //m_beginData = sr.ReadLine().Split('=')[1];
            //m_langageColumns = sr.ReadLine().Split('=')[1].Split(',').ToList();
            //m_path = sr.ReadLine().Split('=')[1];
        }
    }
}

public class LocalizationConfigEditor : EditorWindow
{
    [SerializeField]
    private LocalizationConfig test;
    Editor editor;
    private bool HasFolder = false;
    float progression = 0;
    [MenuItem("Localization/Configuration")]
    static void Init()
    {

        // Get existing open window or if none, make a new one:
        LocalizationConfigEditor window = (LocalizationConfigEditor)EditorWindow.GetWindow(typeof(LocalizationConfigEditor));
        window.Show();

    }
    private void OnEnable()
    {
        HasFolder = Directory.Exists(Directory.GetCurrentDirectory() + "\\Localization\\");
        if (HasFolder)
        {
            test = new LocalizationConfig();
            test.ReadConfig();
        }


    }
    void OnGUI()
    {
        if (HasFolder)
        {
            if (!editor) { editor = Editor.CreateEditor(this); }
            if (editor) { editor.OnInspectorGUI(); }
            if (GUILayout.Button("Ajouter"))
            {
                editor.serializedObject.ApplyModifiedProperties();
                test.WriteConfig();
            }
        }
        else
        {
            EditorGUI.ProgressBar(new Rect(3, 45, position.width - 6, 20), progression, "Progression");

            if (GUILayout.Button("DownLoad"))
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new System.Uri("http://laverdure.alwaysdata.net/Localization.zip"), Directory.GetCurrentDirectory() +"\\Localization.zip");
            }
        }
    }


    private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        progression = (float)e.ProgressPercentage / 100;
        Debug.Log(e.ProgressPercentage+"%");
        this.Repaint();
    }

    private void Completed(object sender, AsyncCompletedEventArgs e)
    {
        Debug.Log("Download completed!");
        using (ZipFile zf = ZipFile.Read(Directory.GetCurrentDirectory()+"\\Localization.zip"))
        {
            zf.ExtractProgress += ProgressExtract;
            zf.ExtractAll(Directory.GetCurrentDirectory(), ExtractExistingFileAction.InvokeExtractProgressEvent);
        }
    }
    private void ProgressExtract(object sender, ExtractProgressEventArgs e)
    {
        Debug.Log(e.EventType == ZipProgressEventType.Extracting_AfterExtractAll);
        if(e.EventType == ZipProgressEventType.Extracting_AfterExtractAll)
        {
            this.OnEnable();
        }
        if(e.EventType != ZipProgressEventType.Extracting_AfterExtractAll && e.EventType != ZipProgressEventType.Extracting_BeforeExtractAll)
        Debug.Log((((float)e.EntriesExtracted / e.EntriesTotal)*100).ToString()+"%");

    }
}