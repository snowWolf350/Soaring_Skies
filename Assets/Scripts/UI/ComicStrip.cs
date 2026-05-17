using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ComicStrip : MonoBehaviour
{
    [Serializable]
    struct panel
    {
        public Vector2 _startPos;
        public Vector2 _endPos;
        public Image _image;
    }

    [SerializeField] List<panel> _panelList;
}
