using UnityEditor;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/newLevel")]
    public class LevelSO : ScriptableObject
    {
        public int levelNumber;
        public string sceneName;
        public bool unlockedAlready;

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
    }
}
