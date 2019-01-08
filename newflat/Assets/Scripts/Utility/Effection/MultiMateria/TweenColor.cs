

using UnityEngine;

/// <summary>
/// Tween the object's color.
/// </summary>

public class TweenColor : UITweener
{
	public Color from = Color.white;
	public Color to = Color.white;

	Transform mTrans;
	Material mMat;
	Light mLight;
	
//	public bool isAlarmState =false;

	/// <summary>
	/// Current color.
	/// </summary>

	public Color color
	{
		get
		{
			
			if (mLight != null) return mLight.color;
			if (mMat != null) return mMat.color;
			return Color.black;
		}
		set
		{
			
			if (mMat != null) mMat.color = value;

			if (mLight != null)
			{
				mLight.color = value;
				mLight.enabled = (value.r + value.g + value.b) > 0.01f;
			}
		}
	}

	/// <summary>
	/// Find all needed components.
	/// </summary>

	void Awake ()
	{
		
		Renderer ren = GetComponent<Renderer>();
		if (ren != null) mMat = ren.material;
        mLight = GetComponent<Light>();
	}

	/// <summary>
	/// Interpolate and update the color.
	/// </summary>

	override protected void OnUpdate(float factor, bool isFinished) { color = Color.Lerp(from, to, factor);}

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenColor Begin (GameObject go, float duration, Color color)
	{
		TweenColor comp = UITweener.Begin<TweenColor>(go, duration);
		comp.from = comp.color;
		comp.to = color;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
}