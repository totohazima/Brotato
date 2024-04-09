using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionManager : MonoBehaviour, UI_Upadte
{
    public static OptionManager instance;
    public GameObject mainCategory;
    public GameObject basicOption;
    public GameObject backBtn;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Text bgmPersent_Text;
    public Text sfxPersent_Text;

    [Header("�ɼ� ����")]
    public float bgmVolume;
    public float sfxVolume;
    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        UIUpdateManager.uiUpdates.Add(this);
    }
    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);
    }

    public void UI_Update()
    {
        bgmVolume = bgmSlider.value;
        sfxVolume = sfxSlider.value;

        bgmPersent_Text.text = bgmVolume.ToString("F0") + "%";
        sfxPersent_Text.text = sfxVolume.ToString("F0") + "%";
    }

    public void BasicOption_Click()
    {
        mainCategory.SetActive(false);
        backBtn.SetActive(true);
        basicOption.SetActive(true);
    }
    public void mainToClose() //�ɼ�UI ���ο��� ���� ���
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    public void BackMenu() //�ڷ�
    {
        basicOption.SetActive(false);
        backBtn.SetActive(false);
        mainCategory.SetActive(true);
    }
}
