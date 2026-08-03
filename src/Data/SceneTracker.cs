using UnityEngine;

namespace Randomizer
{
    public class SceneTracker : MonoBehaviour
    {
        private string LoadedLevel = "";

        public void Update()
        {
            Randomizer.SceneActiveTime += Time.unscaledDeltaTime;
            Randomizer.LevelActiveTime += Time.unscaledDeltaTime;
            if (!LoadedLevel.Equals(Randomizer.CurrentLevel))
            {
                Randomizer.SceneActiveTime = 0f;
                LoadedLevel = Randomizer.CurrentLevel;
            }
        }

        public void ResetLevelActiveTime(){
            Randomizer.SceneActiveTime = 0f;
        }
    }
}
