using CodeBase.UI.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Entities.HUD_Folder
{
    public class HUD : MonoBehaviour, IUIEntity
    {
        [SerializeField] private Image Crosshair;

        public void TurnCrosshair(bool on) => 
            Crosshair.gameObject.SetActive(on);
    }
}