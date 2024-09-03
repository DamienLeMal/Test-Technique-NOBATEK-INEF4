
using UnityEngine;

namespace DamienLeMal.TearOffMenu {
    public class SimpleRotation : MonoBehaviour
    {
        public float speed;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0,speed * Time.deltaTime,0);
        }
    }
}