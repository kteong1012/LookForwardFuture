using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollVideosCellView : EnhancedScrollerCellView
{
    public Image img;
    public Text text;
        
    private ScrollVideosCellData _data;
    private int _index;
    private string[] _files;
    public void SetCellView(ScrollVideosCellData data,string[] files,int index)
    {
        _data = data;
        _index = index;
        _files = files;
        //todo 封面
        if (text)
        {
            text.text = IOUtility.GetNameWithoutExtension(data.videoPath);
        }
    }
    public void OnClick()
    {
        VideoPlayerManager.Instance.StartPlayVideo(_files, _index);
    }
}
