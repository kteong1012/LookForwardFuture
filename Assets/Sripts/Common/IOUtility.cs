using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public static class IOUtility
{
    /// <summary>
    /// mp4
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static FileInfo[] GetAllVideoFiles(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            return null;
        }
        DirectoryInfo dir = new DirectoryInfo(dirPath);
        FileInfo[] files = dir.GetFiles().Where(file =>
        {
            for (int i = 0; i < (int)VideoFileSufix.Count; i++)
            {
                if (file.Name.EndsWith($".{(VideoFileSufix)i}"))
                {
                    return true;
                }
            }
            return false;
        }).ToArray();
        return files;
    }
    public static bool Exists(string name)
    {
        return File.Exists(name);
    }
    public static string GetNameWithoutExtension(FileInfo file)
    {
        return file.Name.Replace(file.Extension, string.Empty);
    }
    public static string GetNameWithoutExtension(string fullName)
    {
        FileInfo file = new FileInfo(fullName);
        return file.Name.Replace(file.Extension, string.Empty);
    }
    public static bool IsAnyPictureFileWithName(string dir,string name,out string fullName)
    {
        string path = Path.Combine(dir, name);
        fullName = "";
        for (int i = 0; i < (int)PictureFileSufix.Count; i++)
        {
            fullName = $".{(PictureFileSufix)i}";
            if (File.Exists(fullName))
            {
                return true;
            }
        }
        fullName = "";
        return false;
    }
}
