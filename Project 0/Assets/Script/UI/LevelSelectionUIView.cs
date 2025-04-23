using Level;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilis;

namespace UI
{
    public class LevelSelectionUIView : MonoBehaviour
    {
        private LevelSelectionUIController levelSelectionUIController;

        public List<LevelSO> levelData;
        public GameObject levelParentObject;
        public GameObject levelButtonPrefab;
        public Button backButton;
        public CanvasGroup levelSelectionCanvas;

        public void SetController(LevelSelectionUIController levelSelectionUIController)
        {
            this.levelSelectionUIController = levelSelectionUIController;
            DisplayLevelButtons();
            backButton.onClick.AddListener(DisableLevelSelection);
        }

        private void DisableLevelSelection()
        {
            CanvasGroupExtension.Hide(levelSelectionCanvas);
        }

        private void DisplayLevelButtons()
        {
            foreach (var level in levelData)
            {
                GameObject levelButtonObject = Instantiate(levelButtonPrefab, levelParentObject.transform);

                SetLevelButton(level, levelButtonObject);

                Image lockedImage = levelButtonObject.transform.Find("LockedImage").GetComponent<Image>();
                TextMeshProUGUI levelText = levelButtonObject.GetComponentInChildren<TextMeshProUGUI>();

                SetLevelText(levelButtonObject, level, lockedImage, levelText);

                if (level.unlockedAlready || levelSelectionUIController.IsLevelUnlocked(level.sceneName))
                    levelSelectionUIController.UnlockLevel(level.sceneName);
                else
                    ShowLockedImage(levelButtonObject, level, lockedImage, levelText);
            }
        }

        private void ShowLockedImage(GameObject levelButtonObject, LevelSO capturedLevelData, Image lockedImage, TextMeshProUGUI levelText)
        {
            lockedImage.enabled = true;
            SetLevelButtonInteractable(levelButtonObject, false);
        }



        private void SetLevelText(GameObject levelButtonObject, LevelSO capturedLevelData, Image lockedImage, TextMeshProUGUI levelText)
        {
            lockedImage.enabled = false;

            levelText.text = capturedLevelData.levelNumber.ToString();
            SetLevelButtonInteractable(levelButtonObject, true);
        }

        private void SetLevelButtonInteractable(GameObject levelButtonObject, bool isInteractable)
        {
            Button levelButton = levelButtonObject.GetComponent<Button>();
            levelButton.interactable = isInteractable;
            Canvas.ForceUpdateCanvases();
        }

        private void SetLevelButton(LevelSO capturedLevel, GameObject levelObject)
        {
            Button levelButton = levelObject.GetComponent<Button>();

            levelButton.onClick.AddListener(() => LoadScene(capturedLevel.sceneName));
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
