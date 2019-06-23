using UnityEngine;

namespace src.Interfaces
{
    public interface IUpgrade
    {
        void PerformUpgrade();
        void OnTriggerEnter2D(Collider2D other);
    }
}