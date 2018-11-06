using UnityEngine;
using System.Collections;

public class FlashingController : HighlighterController
{
	
	public float flashingDelay = 2.5f;
	public float flashingFrequency = 0.5f;

	// 
	new void Start()
	{
		base.Start();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="flashingStartColor"></param>
	/// <param name="flashingEndColor"></param>
	public void play(Color flashingStartColor, Color flashingEndColor)
	{
        if(h!=null)
		    h.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
		//if (color == null)
		//{
		//    h.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
		//}
		//else
		//{
		//    h.FlashingOn(color, color, flashingFrequency);
		//}
       
    }

	/// <summary>
	/// 创建一种颜色
	/// </summary>
	/// <param name="color"></param>
	public void PlayOnOne(Color color)
	{
		//h.FlashingOn(color, Color.blue, flashingFrequency);
		//h.FlashingParams(color, Color.blue, 2f);
		//h.On(color);
	}
    public void stop()
    {
        if(h!=null)
            h.Off();
    }
	// 
	protected IEnumerator DelayFlashing()
    {
        //h.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
		yield return new WaitForSeconds(flashingDelay);
		
		// Start object flashing after delay
	}
}
