using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScrollVideosCellView : EnhancedScrollerCellView
{
    public MediaPlayer player;
    public GameObject uguiDisplayGob;
    public RawImage img;
    public Text text;
        
    private ScrollVideosCellData _data;
    private int _index;
    private string[] _files;

    public void SetCellView(ScrollVideosCellData data,int index)
    {
        if (data == null)
        {
            Debug.LogError("Error unknown,,,");
        }
        _data = data;
        _index = index;
        uguiDisplayGob.SetActive(data.type == FileType.Video);
        img.gameObject.SetActive(data.type == FileType.Picture);
        if (data.type == FileType.Video)
        { 
            player.m_VideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
            player.m_VideoPath = data.path;
        }
        if (text)
        {
            text.text = IOUtility.GetNameWithoutExtension(data.path);
        }
    }
    private void OnEnable()
    {
        if(_data == null)
        {
            return;
        }
        StopAllCoroutines();
        if (_data.type == FileType.Picture)
        {
            StartCoroutine(TryLoadImage());
        }
    }

    private IEnumerator TryLoadImage()
    {
        if (string.IsNullOrEmpty(_data.path)||_data.type!= FileType.Picture)
        {
            yield break;
        }
        UnityWebRequest request = new UnityWebRequest(_data.path);
        DownloadHandlerTexture handler = new DownloadHandlerTexture();
        request.downloadHandler = handler;
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError(request.error);
        }
        else
        {
            if (handler.texture)
            {
                img.texture = handler.texture;
            }
            else
            {
                Debug.LogError("unknown error");
            }
        }
    }

    public override void RefreshCellView(params object[] objs)
    {
        if (objs != null && objs.Length > 0)
        {
            int index = (int)objs[0];
            if (index == _index)
            {
                transform.DOKill();
                transform.DOScale(2f, 0.5f);
                if (_data.type == FileType.Video)
                {
                    player.Play();
                }
            }
            else
            {
                transform.DOKill();
                transform.DOScale(0.8f, 0.5f);
                if(_data.type== FileType.Video)
                {
                    player.Pause();
                }
            }
        }
    }
}
