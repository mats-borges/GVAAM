using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    private string languageName;
    private List<string> langList = new List<string>();

    private List<AudioClip> musicList = new List<AudioClip>();
    private List<string> musicNameList = new List<string>();

    [SerializeField] private GameObject BGMusic;
    [SerializeField] private GameObject MusicNameTextObject;
    private TextMeshPro MusicNameText;
    public int CurMusicNum = 0;

    private AudioClip Track;
    public int CurTrackNum = 0;

    [SerializeField] private GameObject langNameTextObject;
    [SerializeField] private GameObject pagesideTextManager;
    private TextMeshPro langNameText;
    public int CurLangNum = 0;

    [SerializeField] private UnityEvent onMusicPress;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Material pause;
    [SerializeField] private Material play;

    private bool musicHasBeenPaused;

    [SerializeField] private UnityEvent onIntroWallPress;
    [SerializeField] public GameObject introWallObject;
    [SerializeField] private GameObject introWallOnOffText;
    private bool introWallHasBeenTurnedOff;

    [SerializeField] private GameObject AnnotationOnOffText;
    [SerializeField] private GameObject highlightFolder;
    private bool AnnotationHasBeenTurnedOff;

    [SerializeField] private GameObject InspectorOnOffText;
    private bool inspectorIsOn = true;
    [SerializeField] private GameObject inspectorPeripheries;


    // Start is called before the first frame update
    void Start()
    {
        //fill a list with the names of all language files from the pageside text manager
        List<TextAsset> langAssetList = pagesideTextManager.GetComponent<PagesideTextManager>().LanguageFileList;

        for (int i = 0; i < langAssetList.Count; i++)
        {
            langList.Add(langAssetList[i].name);
        }

        langNameText = langNameTextObject.GetComponent<TextMeshPro>();
        langNameText.text = langList[CurLangNum];

        List<AudioClip> musicLists = BGMusic.GetComponent<MusicManager>().MusicList;
        List<string> musicNameLists = BGMusic.GetComponent<MusicManager>().MusicNameList;

        for (int i = 0; i < musicLists.Count; i++)
        {
            musicList.Add(musicLists[i]);
        }

        for (int i = 0; i < musicNameLists.Count; i++)
        {
            musicNameList.Add(musicNameLists[i]);
        }

        MusicNameText = MusicNameTextObject.GetComponent<TextMeshPro>();
        MusicNameText.text = musicNameList[CurMusicNum];

        Track = musicList[CurTrackNum];

        musicHasBeenPaused = false;
        AnnotationHasBeenTurnedOff = false;
        introWallHasBeenTurnedOff = false;
    }

    //Cycle music name
    public void CycleMusicName()
    {
        CurMusicNum++;
        if (CurMusicNum >= musicNameList.Count)
        {
            CurMusicNum--;
        }
        MusicNameText.text = musicNameList[CurMusicNum];
    }

    public void CycleMusicNameBackwards()
    {
        CurMusicNum--;
        if (CurMusicNum <= -1)
        {
            CurMusicNum++;
        }
        MusicNameText.text = musicNameList[CurMusicNum];
    }

    public void CycleTrack()
    {
        CurTrackNum++;
        if (CurTrackNum >= musicList.Count)
        {
            CurTrackNum--;
        }
        Track = musicList[CurTrackNum];
        BGMusic.GetComponent<MusicManager>().UpdateTrack(CurTrackNum);
    }

    public void CycleTrackBackwards()
    {
        CurTrackNum--;

        if (CurTrackNum <= -1)
        {
            CurTrackNum++;
        }
        Track = musicList[CurTrackNum];
        BGMusic.GetComponent<MusicManager>().UpdateTrack(CurTrackNum);
    }

    public void CycleLanguageName()
    {
        CurLangNum++;
        if (CurLangNum >= langList.Count)
        {
            CurLangNum = 0;
        }
        langNameText.text = langList[CurLangNum];
    }

    public void CycleLanguageNameBackwards()
    {
        CurLangNum--;
        if (CurLangNum <= langList.Count)
        {
            CurLangNum = langList.Count;
        }
        langNameText.text = langList[CurLangNum];
    }


    public void UpdateMusicOnOff()
    {
        if (musicHasBeenPaused == false)
        {
            pauseButton.GetComponent<Renderer>().sharedMaterial = pause;
            BGMusic.GetComponent<AudioSource>().Pause();
            musicHasBeenPaused = true;
        }
        else
        {
            pauseButton.GetComponent<Renderer>().sharedMaterial = play;
            BGMusic.GetComponent<AudioSource>().Play();
            musicHasBeenPaused = false;
            Debug.Log("play");
        }
    }

    public void UpdateIntroWallOnOff()
    {
        if (introWallHasBeenTurnedOff == false)
        {
            introWallOnOffText.GetComponent<TextMeshPro>().text = "OFF";
            introWallObject.SetActive(false);
            introWallHasBeenTurnedOff = true;
        }
        else
        {
            introWallOnOffText.GetComponent<TextMeshPro>().text = "ON";
            introWallObject.SetActive(true);
            introWallHasBeenTurnedOff = false;
        }
    }

    public void UpdateAnnotationsOnOff()
    {
        if (AnnotationHasBeenTurnedOff == false)
        {
            AnnotationOnOffText.GetComponent<TextMeshPro>().text = "OFF";
            highlightFolder.SetActive(false);
            AnnotationHasBeenTurnedOff = true;
        }
        else
        {
            AnnotationOnOffText.GetComponent<TextMeshPro>().text = "ON";
            highlightFolder.SetActive(true);
            AnnotationHasBeenTurnedOff = false;
        }
    }

    public void UpdateInspectorOnOff()
    {
        inspectorIsOn = !inspectorIsOn;
        if (inspectorIsOn == false)
        {
            InspectorOnOffText.GetComponent<TextMeshPro>().text = "OFF";
            inspectorPeripheries.SetActive(false);
        }
        else
        {
            InspectorOnOffText.GetComponent<TextMeshPro>().text = "ON";
            inspectorPeripheries.SetActive(true);

        }

    }

    public void ResetExperienceCP()
    {
        //translation
        CurLangNum = 0;
        langNameText.text = langList[CurLangNum];

        //music
        //musicOnOffText.GetComponent<TextMeshPro>().text = "ON";
        //musicObject.GetComponent<AudioSource>().Play();
        //musicHasBeenTurnedOff = false;

        //intro wall
        introWallOnOffText.GetComponent<TextMeshPro>().text = "ON";
        introWallObject.SetActive(true);
        introWallHasBeenTurnedOff = false;

        //page
        //done in book manager

        //annotation
        AnnotationOnOffText.GetComponent<TextMeshPro>().text = "ON";
        highlightFolder.SetActive(true);
        AnnotationHasBeenTurnedOff = false;
    }
}