using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScrollVideosController : MonoBehaviour, IEnhancedScrollerDelegate
{
    #region Static Fields
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
    private void OnEnable()
    {
        _scroller.scrollerScrollingChanged += OnScrollingChanged;
    }
    private void OnDisable()
    {
        _scroller.scrollerScrollingChanged -= OnScrollingChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentIndex =  (GetNumberOfCells(_scroller) + _currentIndex - 1)%GetNumberOfCells(_scroller);
            JumpTo(_currentIndex);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _currentIndex = (GetNumberOfCells(_scroller) + _currentIndex + 1) % GetNumberOfCells(_scroller);
            JumpTo(_currentIndex);
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
        string dirPath = Path.Combine(Application.streamingAssetsPath);
        FileInfo[] files = IOUtility.GetAllFiles(dirPath);
        if (files == null || files.Length == 0)
        {
            return;
        }
        _datas.Clear();
        foreach (var file in files)
        {
            if (IOUtility.IsVideoFile(file))
            {
                _datas.Add(new ScrollVideosCellData(FileType.Video, file.FullName));
            }
            else if (IOUtility.IsPictureFile(file))
            {
                _datas.Add(new ScrollVideosCellData(FileType.Picture, file.FullName));
            }
        }
        if (_scroller.Delegate == null) 
        {
            _scroller.Delegate = this;

        }
        _scroller.ReloadData();
        _currentIndex = 0;
        _scroller.RefreshActiveCellViews(_currentIndex);
    }
    private void OnScrollingChanged(EnhancedScroller scroller, bool scrolling)
    {
        _scroller.RefreshActiveCellViews(_currentIndex);
    }
    private void JumpTo(int index)
    {
        _scroller.JumpToDataIndex(index, 0.25f, 0, true, EnhancedScroller.TweenType.linear, 0.3f, () =>
        {
            OnScrollingChanged(_scroller, true);
        });
    }
    #endregion

    #region Implementations
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        ScrollVideosCellView cell = scroller.GetCellView(_cellViewPrefab) as ScrollVideosCellView;
        ScrollVideosCellData data = _datas[dataIndex];
        cell.SetCellView(data, dataIndex);
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
