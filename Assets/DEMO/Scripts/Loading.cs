using System.Collections;
using AudioManager;
using UnityEngine;

namespace DEMO.Scripts
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        
        IEnumerator Start()
        {
            _menu.gameObject.SetActive(false);
            AudioManagerFmod.Instance.LoadVolume();
            yield return new WaitUntil(() => AudioManagerFmod.Instance.IsAllBanksLoaded());
            _menu.Initialize();
            _menu.gameObject.SetActive(true);
        }
    }
}