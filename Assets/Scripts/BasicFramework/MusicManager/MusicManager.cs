using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : BaseManager<MusicManager>
{
    #region 背景音乐

    private AudioSource BGMusic = null;
    private float bgValue = 1;
    private bool mute = false;
    public float BGValue => bgValue;

    /// <summary>
    /// 切换音量
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBGMVolume(float value)
    {
        bgValue = value;
        BGMusic.volume = bgValue;
    }

    /// <summary>
    /// 切换静音
    /// </summary>
    /// <param name="isMute"></param>
    public void ChangeBGMMute(bool isMute)
    {
        mute = isMute;
        BGMusic.mute = mute;
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name">音乐名</param>
    public void PlayBGM(string name)
    {
        if(BGMusic == null)
        {
            GameObject obj = new GameObject("BGMusic");
            BGMusic = obj.AddComponent<AudioSource>();
        }
        ResManager.Instance.LoadAsyn<AudioClip>("Music/BGM/" + name, (clip) =>
        {
            BGMusic.clip = clip;
            BGMusic.volume = BGValue;
            BGMusic.mute = mute;
            BGMusic.Play();
        });
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGM()
    {
        if (BGMusic == null)
            return;
        BGMusic.Stop();
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBGM()
    {
        if (BGMusic == null)
            return;
        BGMusic.Pause();
    }

    #endregion



    public MusicManager()
    {
        MonoManger.Instance.AddListener(Update);
    }



    #region 音效
    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    private float soundValue = 1;
    private bool soundMute = false;

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">音乐名</param>
    /// <param name="isLoop">是否循环</param>
    /// <param name="callBack">回调</param>
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callBack = null)
    {
        if (soundObj == null)
        {
            soundObj = new GameObject("Sound");
        }
        
        ResManager.Instance.LoadAsyn<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = soundValue;
            source.mute = soundMute;
            source.loop = isLoop;
            source.Play();
            soundList.Add(source);

            callBack?.Invoke(source);
        });
    }

    /// <summary>
    /// 暂停音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }

    /// <summary>
    /// 音效音量
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = soundValue;
        }
    }

    /// <summary>
    /// 音效静音
    /// </summary>
    /// <param name="isMute"></param>
    public void ChangeSoundMute(bool isMute)
    {
        soundMute = isMute;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].mute = soundMute;
        }
    }
    #endregion

    private void Update()
    {
        for (int i = soundList.Count - 1; i >= 0 ; i--)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
}
