using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TweeningCurve
{
    public CurveType curveType;
    public Func<float, float> tweenFunc;

    public TweeningCurve(CurveType curveType)
    {
        this.curveType = curveType;
    }

    public TweeningCurve(Func<float, float> tweenFunc)
    {
        this.tweenFunc = tweenFunc;
    }

    public static TweeningCurve Linear { get { return new TweeningCurve(CurveType.Linear); } }
    public static TweeningCurve Quadratic { get { return new TweeningCurve(CurveType.Quadratic); } }
    public static TweeningCurve Cubic { get { return new TweeningCurve(CurveType.Cubic); } }
    public static TweeningCurve Quartic { get { return new TweeningCurve(CurveType.Quartic); } }
    public static TweeningCurve Quintic { get { return new TweeningCurve(CurveType.Quintic); } }
    public static TweeningCurve Bounce { get { return new TweeningCurve(CurveType.Bounce); } }
}

