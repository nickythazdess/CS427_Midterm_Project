using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    private int selectedCharacterIndex;

    [Header("List of characters")]
    [SerializeField] private List<CharacterSeclectObject> list = new List<CharacterSeclectObject>();
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDescription;
    [SerializeField] private Image background;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button startButton;

    [Header("Sounds")]
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private AudioClip characterSelectMusic;


    private void Start() {
        UpdateCharacterSelectionUI();
    }

    public void LeftArrow() {
        buttonClick.Play();
        selectedCharacterIndex--;
        if (selectedCharacterIndex<0) {
            selectedCharacterIndex = list.Count - 1;
        }
        UpdateCharacterSelectionUI();
    }

    public void RightArrow() {
        buttonClick.Play();
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= list.Count) {
            selectedCharacterIndex = 0;
        }
        UpdateCharacterSelectionUI();
    }

    public void StartGame() {
        PlayerPrefs.SetInt("Character", selectedCharacterIndex);
        Loader.Load(Loader.Scene.Scene);
    }

    private void UpdateCharacterSelectionUI() {
        int left = selectedCharacterIndex - 1;
        int right = selectedCharacterIndex + 1;
        if (left < 0) left = list.Count-1;
        else if (left >= list.Count) left = 0;
        if (right < 0) right = list.Count-1;
        else if (right >= list.Count) right = 0;

        list[selectedCharacterIndex].splash.GetComponent<Renderer>().enabled = true;
        list[left].splash.GetComponent<Renderer>().enabled = false;
        list[right].splash.GetComponent<Renderer>().enabled = false;

        leftButton.GetComponent<Image>().sprite = list[left].button;
        rightButton.GetComponent<Image>().sprite = list[right].button;
        startButton.GetComponent<Image>().sprite = list[selectedCharacterIndex].start;
        characterName.text = list[selectedCharacterIndex].name;
        characterDescription.text = list[selectedCharacterIndex].description;
        background.color = list[selectedCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class CharacterSeclectObject {
        public Transform splash;
        public string name;
        public string description;
        public Color characterColor;
        public Sprite button;
        public Sprite start;
    }

}
