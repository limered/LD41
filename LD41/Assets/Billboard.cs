using UnityEngine;

namespace Assets
{
    public class Billboard : MonoBehaviour
    {
        void Update()
        {
            var dir = Camera.main.transform.up;
            transform.up = dir;
        }
    }
}
