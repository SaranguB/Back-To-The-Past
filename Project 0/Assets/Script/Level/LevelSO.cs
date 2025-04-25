using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    [CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/newLevel")]
    public class LevelSO : ScriptableObject
    {
        public int levelNumber;

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
        private void OnValidate()
        {
            if (sceneAsset != null)
            {
                sceneName = sceneAsset.name;
            }
        }
#endif
        public string sceneName;
        public bool unlockedAlready;

    }
}
