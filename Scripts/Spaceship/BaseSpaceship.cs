using UnityEngine;

namespace Spaceship
{
    public abstract class Basespaceship : MonoBehaviour
    {
        [SerializeField] protected Bullet defaultBullet;
        [SerializeField] protected Transform gunPosition;

        protected AudioSource audioSource;
        
        public int Hp { get; protected set; }
        public float Speed { get; private set; }
        public Bullet Bullet { get; private set; }

        private void Awake()
        {
            Debug.Assert(defaultBullet != null, "defaultBullet cannot be null");
            Debug.Assert(gunPosition != null, "gunPosition cannot be null");
        }        
        
        protected void Init(int hp, float speed, Bullet bullet)
        {
            Hp = hp;
            Speed = speed;
            Bullet = bullet;
        }

        public abstract void Fire();
    }
}