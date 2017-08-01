using System.Collections;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utils
{
    public class PressAnyKeyToProceed : MonoBehaviour {

        public string NextLevel;
        private bool _isLoading;

        void Update()
        {
            if (Input.anyKey && !_isLoading)
            {
                if (!string.IsNullOrEmpty(NextLevel))
                {
                    if (UIScreenFader.Instance == null)
                    {
                        SceneManager.LoadScene(NextLevel);
                    }
                    else
                    {
                        UIScreenFader.Instance.FadeIn();
                        StartCoroutine(LoadDelayed(NextLevel, UIScreenFader.Instance.FadeTime));
                        _isLoading = true;
                    }
                }
            }
        }

        IEnumerator LoadDelayed(string levelName, float delay = 1f)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(levelName);
        }
    }
}
