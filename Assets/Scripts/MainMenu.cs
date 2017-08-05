using System.Collections;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {

        public bool LoadNextByAnyKey = false;
        public string NextLevel;
        private bool _isLoading;

        void Update()
        {
            if (LoadNextByAnyKey && Input.anyKey && !_isLoading)
            {
                if (!string.IsNullOrEmpty(NextLevel))
                    LoadScene(NextLevel);
            }
        }

        IEnumerator LoadDelayed(string levelName, float delay = 1f)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(levelName);
        }

        public void LoadScene(string levelName)
        {
            if (UIScreenFader.Instance == null)
            {
                SceneManager.LoadScene(levelName);
                return;
            }
            else
            {
                UIScreenFader.Instance.FadeIn();
                StartCoroutine(LoadDelayed(levelName, UIScreenFader.Instance.FadeTime));
                _isLoading = true;
            }
        }
    }
}
