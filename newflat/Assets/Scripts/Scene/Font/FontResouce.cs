﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontResouce :MonoSingleton<FontResouce> {

    private Font _font;
    public Font font
    {
        get
        {
            return _font;
        }
    }

    public void Init()
    {
        _font = Resources.Load<Font>("Font/MicrosoftYaHeiGB");
    }
}