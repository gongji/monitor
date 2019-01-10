using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SoundUtilty : MonoBehaviour {


    #region play Sound
    public static void PlayServerSound(string soundPath,bool isLoop =false)
    {
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        if(!audioSource)
        {
            audioSource = Camera.main.transform.gameObject.AddComponent<AudioSource>();
        }
        ResourceUtility.Instance.GetHttpAudio (soundPath, (result) =>
        {
            audioSource.clip = result;
            audioSource.loop = isLoop;
            audioSource.Play();
        }
      
       );
    }

    public static void PlayResouceSound(string soundPath, bool isLoop =false)
    {
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = Camera.main.transform.gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip =Resources.Load<AudioClip>(soundPath); 
        audioSource.Play();
        audioSource.loop = isLoop;
    }

    public static void StopSound()
    {
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
            Resources.UnloadUnusedAssets();
        }
    }


    #endregion

    #region poston Sound

    public static void PlayCameraPostionSound(AudioClip ac)
    {
        AudioSource.PlayClipAtPoint(ac, Camera.main.transform.position);
    }


 

    public static void StopCameraPostionSound()
    {

        //AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();
        //foreach(AudioSource audioSource in audioSources)
        //{
        //    Debug.Log(audioSource.clip);
        //    if (audioSource.clip!=null && audioSource.clip.name == soundName)
        //    {
        //        audioSource.enabled = false;
        //        GameObject.Destroy(audioSource);
        //        break;
        //    }
        //}

        if(Camera.main.transform.GetComponent<AudioSource>()!=null)
        {
            Camera.main.transform.GetComponent<AudioSource>().Stop();
        }
        
  
    }

    #endregion

}
