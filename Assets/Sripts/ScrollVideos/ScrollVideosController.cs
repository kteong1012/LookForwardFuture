using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScrollVideosController : MonoBehaviour, IEnhancedScrollerDelegate
{
    #region Static Fields
    private const string DIR_NAME = "AVProVideoSamples";
    #endregion

    #region Serialize Fields
    [SerializeField] private EnhancedScroller _scroller;
    [SerializeField] private EnhancedScrollerCellView _cellViewPrefab;
    #endregion

    #region Member Variables
    private List<ScrollVideosCellData> _datas = new List<ScrollVideosCellData>();
    private string[] _files;
    private int _currentIndex;
    private RectTransform _scalingRect;
    #endregion

    #region LifeCycle
    private void Update()
    {
        if (_scroller)
        {
            float min = float.MaxValue;
            RectTransform rect = null;
            foreach (RectTransform tran in _scroller.Container)
            {
                float distance = Mathf.Abs(tran.position.x - Screen.width / 2);
                if (distance < min)
                {
                    min = distance;
                    rect = tran;
                }
            }
            if (_scalingRect != rect)
            {
                if (_scalingRect)
                {
                    _scalingRect.DOKill();
                    _scalingRect.DOScale(Vector3.one, 0.5f);
                }
                if (rect)
                {
                    rect.DOKill();
                    rect.DOScale(Vector3.one * 2, 0.5f);
                }
                _scalingRect = rect;
            }
        }
    }
    #endregion

    #region Public Methods
    private void Start()
    {
        LoadAllDatas();
    }
    public void LoadAllDatas()
    {
        string dirPath = Path.Combine(Application.streamingAssetsPath, DIR_NAME);
        FileInfo[] files = IOUtility.GetAllVideoFiles(dirPath);
        if (files == null || files.Length == 0)
        {
            return;
        }
        _datas.Clear();
        foreach (var file in files)
        {
            ScrollVideosCellData data = new ScrollVideosCellData();
            data.videoPath = file.FullName;
            IOUtility.IsAnyPictureFileWithName(Application.streamingAssetsPath,IOUtility.GetNameWithoutExtension(file),out data.imagePath);
            _datas.Add(data);
        }
        _files = _datas.Select(d => d.videoPath).ToArray();
        if (_scroller.Delegate == null) 
        {
            _scroller.Delegate = this;
        }
        _scroller.ReloadData();
    }

    public void OnScrollValueChange()
    {
    }
    #endregion

    #region Implementations
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        ScrollVideosCellView cell = scroller.GetCellView(_cellViewPrefab) as ScrollVideosCellView;
        ScrollVideosCellData data = _datas[dataIndex];
        cell.SetCellView(data, _files, dataIndex);
        return cell;
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        RectTransform rect = _cellViewPrefab.transform as RectTransform;
        if (_scroller.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Horizontal)
        {
            return rect.rect.width;
        }
        else
        {
            return rect.rect.height;
        }
    }
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _datas.Count;
    }
    #endregion
}
