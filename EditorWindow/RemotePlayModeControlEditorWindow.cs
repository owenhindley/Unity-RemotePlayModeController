using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;


public class RemotePlayModeControlEditorWindow : EditorWindow
{
    public int port = 88;
    public string IP = "10.121.100.100";
    public float checkInterval = 1.0f;

    private float lastCheck = -1f;

    private bool remoteStatus = false;
    
    private UnityWebRequest rq;

    [MenuItem("Window/RemotePlayModeControl")]
    public static void Init()
    {
        RemotePlayModeControlEditorWindow wind =
            (RemotePlayModeControlEditorWindow) EditorWindow.GetWindow(typeof(RemotePlayModeControlEditorWindow));
        wind.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Remote status : " + (remoteStatus ? "play" : "stop"));
        
        CheckStatus();

        port = EditorGUI.IntField(new Rect(3, 20, position.width - 6, 20), "Port", port);
        IP = EditorGUI.TextField(new Rect(3, 50, position.width - 6, 20), "IP", IP);

        if (GUI.Button(new Rect(3, 80, position.width - 6, 20), "Toggle PlayMode"))
        {
            if (EditorApplication.isPlaying)
            {
                StopPlayMode();
            }
            else
            {
                SetPlayMode();
            }
        }
        
        

    }


   
    public void SetPlayMode()
    {
        var playModeRQ = UnityWebRequest.Get("http://"+ IP + ":" + port + "/start");
        playModeRQ.SendWebRequest();
    }

    public void StopPlayMode()
    {
        var stopModeRQ = UnityWebRequest.Get("http://"+ IP + ":" + port + "/stop");
        stopModeRQ.SendWebRequest();
    }
    
    

    void CheckStatus()
    {

        if (rq == null)
        {
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            if (now - lastCheck > checkInterval)
            {
                rq = UnityWebRequest.Get("http://"+ IP + ":" + port + "/state");
                rq.SendWebRequest();
            }
        } else if (rq.isDone)
        {
            if (rq.result == UnityWebRequest.Result.Success)
            {
                    
                var t = rq.downloadHandler.text;
                // Debug.Log("PlayMode Control status : " + t);
                remoteStatus = t == "1";
                if (t == "1" && !EditorApplication.isPlaying)
                {
                    // Debug.Log("Remotely playing editor");
                    AssetDatabase.Refresh();
                    EditorApplication.isPlaying = true;
                }
                else if ( t == "0" && EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                }
            }
            else
            {
                Debug.LogWarning("Remote play control request error : " + rq.result.ToString() + " to " + rq.url);
            }

            rq = null;

        }

    }


    void Update()
    {
        // Debug.Log("Update called!");
        
        CheckStatus();
    }
}
