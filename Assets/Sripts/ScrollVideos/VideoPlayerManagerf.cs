using RenderHeads.Media.AVProVideo.Demos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayerManager : Singleton<VideoPlayerManager>
{
    private const int PLAYER_SCENE_INDEX = 1;

    private int _lastSceneIndex = 0;

    private string[] _prePlayVideos;
    private string _playingVideo;
    private int _index;

    public VideoPlayerManager()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    public string StartPlayVideo(string[] videoFiles,int index)
    {
        _prePlayVideos = videoFiles;
        _index = index;
        _lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(PLAYER_SCENE_INDEX);
        return "success";
    }
    public void Back()
    {
        SceneManager.LoadScene(_lastSceneIndex);
    }
    private void Play()
    {
        VCR vcr = GameObject.FindObjectOfType<VCR>();
        if (vcr)
        {
            vcr._videoFiles = _prePlayVideos;
            vcr._location = RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL;
            vcr.Play(_index);
        }
    }

    private void OnActiveSceneChanged(Scene preScene, Scene currentScene)
    {
        if(currentScene.buildIndex == PLAYER_SCENE_INDEX)
        {
            Play();
        }
    }
}
