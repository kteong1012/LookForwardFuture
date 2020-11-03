using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScrollVideosCellData
{
    public FileType type;
    public string path;
    public ScrollVideosCellData(FileType type,string path)
    {
        this.type = type;
        this.path = path;
    }
}

