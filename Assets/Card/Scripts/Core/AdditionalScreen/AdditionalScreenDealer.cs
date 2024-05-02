using System.Collections.Generic;
using TheaCard.Core.Enums;
using TheaCard.Infrastructure.ScreenDealer;
using UnityEngine;

namespace TheaCard.Code.ScreenDealer
{
    public sealed class AdditionalScreenDealer : IAdditionalScreenDealer
    {
        private readonly Dictionary<AdditionalScreens, IAdditionalScreen<AdditionalScreens>> _screens = 
            new Dictionary<AdditionalScreens, IAdditionalScreen<AdditionalScreens>>();

        public AdditionalScreenDealer(IEnumerable<IAdditionalScreen<AdditionalScreens>> additionalScreens)
        {
            foreach (var screen in additionalScreens)
            {
                if (!_screens.ContainsKey(screen.ScreenType))
                {
                    _screens.Add(screen.ScreenType, screen);
                }
                else
                {
                    Debug.LogWarning($"The additional Screens already contains an element of type {screen.ScreenType}!");
                }
            }
        }

        public void ShowScreen(AdditionalScreens screen)
        {
            if (_screens.ContainsKey(screen))
            {
                _screens[screen].Show();
            }
            else
            {
                Debug.LogWarning($"There is no field for type {screen}!");
            }
        }

        public void HideScreen(AdditionalScreens screen)
        {
            if (_screens.ContainsKey(screen))
            {
                _screens[screen].Hide();
            }
            else
            {
                Debug.LogWarning($"There is no field for type {screen}!");
            }
        }
    }
}