using UnityEngine;

namespace Model.ShootingGame
{
    public abstract class PickUps : MonoBehaviour
    {
        protected float _baseY;

        protected void Awake()
        {
            _baseY = transform.position.y;
        }

        protected void Update()
        {      
            transform.Rotate(0, 0, 1);
            transform.position = new Vector3(transform.position.x, _baseY + Mathf.Sin(Time.deltaTime * 3f) * 0.2f, transform.position.z);
        }

        protected abstract void OnTriggerEnter(Collider other);
    }
}
