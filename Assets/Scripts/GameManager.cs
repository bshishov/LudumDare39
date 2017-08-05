using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsActive { get; private set; }
        public Dictionary<ResourceData, float> Resources = new Dictionary<ResourceData, float>();
        public GameObject Sun;

        [HideInInspector]
        public List<Machine> BuiltMachines = new List<Machine>();
        
        [Header("Level")]
        public float TimeForSunToGoOut;
        public List<ResourceAmount> BaseResources;
        public List<ResourceAmount> ConsumePerSecond = new List<ResourceAmount>();

        [Header("Winning")]
        public ResourceAmount WinningCondition;
        public float WinTimer = 480;

        [Header("Game Over")]
        public ResourceAmount[] LosingCondition;
        
        private Sun _sunComponent;
        private float _timer = 0f;
        private bool _checkForWin = false;

        void Start()
        {
            var resources = UnityEngine.Resources.LoadAll<ResourceData>("Res"); 
            Debug.Log(string.Format("Loaded {0} resources", resources.Length));

            // Load all resources
            foreach(var resource in resources)
                Resources.Add(resource, 0);

            // Set base values
            foreach (var resource in BaseResources)
                Resources[resource.Resource] = resource.Amount;

            _sunComponent = Sun.GetComponent<Sun>();

            var existingMachines = FindObjectsOfType<Machine>();
            BuiltMachines.AddRange(existingMachines);
            IsActive = true;
            StartCoroutine(CheckWin());
        }

        IEnumerator CheckWin()
        {
            yield return new WaitForSeconds(WinTimer);
            _checkForWin = true;
        }

        void Update ()
        {
            if (_sunComponent.Temperature > 0f)
            {
                var oldTemperature = _sunComponent.Temperature;
                var newTemperature = oldTemperature - Time.deltaTime / TimeForSunToGoOut;
                _sunComponent.Temperature = Mathf.Max(newTemperature, 0f);
            }

            _timer += Time.deltaTime;
            if (_timer > 1f)
            {
                Consume();
                _timer = 0;
            }

            if (IsActive)
            {
                foreach (var amount in LosingCondition)
                {
                    if (Resources[amount.Resource] < amount.Amount)
                    {
                        UIManager.Instance.ShowLoseScreen();
                        IsActive = false;
                        break;
                    }
                }

                if (_checkForWin && HasResourceAmount(WinningCondition))
                {
                    UIManager.Instance.ShowWinScreen();
                    _checkForWin = false;
                }
            }
        }

        private void Consume()
        {
            foreach (var res in ConsumePerSecond)
            {
                DecreaseResource(res);
            }
        }

        public bool HasResourceAmount(ResourceAmount resource)
        {
            return Resources[resource.Resource] >= resource.Amount;
        }

        public void DecreaseResource(ResourceAmount resource, float multiplier = 1)
        {
            if(IsActive)
                Resources[resource.Resource] = Mathf.Max(Resources[resource.Resource] - resource.Amount * multiplier, 0);
        }

        public void IncreaseResource(ResourceAmount resource, float multiplier = 1)
        {
            if(IsActive)
                Resources[resource.Resource] = resource.Amount * multiplier + Resources[resource.Resource];
        }

        public void Restart()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        public void LoadLevel(string sceneName)
        {
            var fader = UIScreenFader.Instance;
            if (fader != null)
            {
                fader.FadeIn();
                StartCoroutine(LoadDelayed(fader.FadeTime, sceneName));
            }
            else
            {
                StartCoroutine(LoadDelayed(0.1f, sceneName));
            }
        }

        private IEnumerator LoadDelayed(float delay, string sceneName)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }

        public void ToggleAudio()
        {
            AudioListener.volume = 1 - AudioListener.volume;
        }

        public void ExitToMenu()
        {
            //LoadLevel(SceneManager.GetSceneByBuildIndex(0).name);
            LoadLevel("title");
        }
    }
}
