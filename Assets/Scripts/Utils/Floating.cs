using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Floating : MonoBehaviour
    {

        private float _offset;

        void Start ()
        {
            _offset = Random.value;
        }
	
        // Update is called once per frame
        void Update ()
        {
            var s = Mathf.Sin(Time.time + _offset);
            var c = Mathf.Cos(Time.time * 0.9f + _offset);

            transform.position += new Vector3(s, c, 0) * 0.005f;
        }
    }
}
