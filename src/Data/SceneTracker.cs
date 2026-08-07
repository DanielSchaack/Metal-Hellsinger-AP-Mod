using UnityEngine;

namespace Randomizer
{
    public class SceneTracker : MonoBehaviour
    {
        private string LoadedLevel = "";

        public void Update()
        {
            Randomizer.SceneActiveTime += Time.unscaledDeltaTime;

            if(!Randomizer.IsPaused)
                Randomizer.LevelActiveTime += Time.unscaledDeltaTime;

            if (!LoadedLevel.Equals(Randomizer.CurrentLevel))
            {
                Randomizer.SceneActiveTime = 0f;
                LoadedLevel = Randomizer.CurrentLevel;
            }

            if (
                Application.runInBackground
                != !Randomizer.Configuration.hellsingerPauseGameOutOfFocused.Value
            )
                Application.runInBackground = !Randomizer
                    .Configuration
                    .hellsingerPauseGameOutOfFocused
                    .Value;
        }

        public void ResetLevelActiveTime(){
            Randomizer.SceneActiveTime = 0f;
            Randomizer.LevelActiveTime = 0f;
        }
    }
}
