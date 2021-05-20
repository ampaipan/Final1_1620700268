using System;
using Manager;
using UnityEngine;

namespace Spaceship
{
    public class Planet : Basespaceship, IDamagable
    {
        public event Action OnExploded;

        public int Hp { get; protected set; }

        public void TakeHit(int damage)
        {
            Hp -= damage;

            if (Hp > 0)
            {
                return;
            }

            Explode();
        }

        public void Explode()
        {
            SoundManager.Instance.Play(SoundManager.Sound.Planet);
            Debug.Assert(Hp <= 0, "HP is more than zero");
            gameObject.SetActive(false);
            Destroy(gameObject);
            ScoreManager.Instance.UpdateScore(1);
            OnExploded?.Invoke();
        }
        public override void Fire()
        { 
        }

    }
}