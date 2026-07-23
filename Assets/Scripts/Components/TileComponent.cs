using Janito.EditorExtras;
using TMPro;
using UnityEngine;

namespace GMTK
{
    public class TileComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _tileText;

        private void Awake()
        {
            if (_tileText == null)
            {
                _tileText = GetComponentInChildren<TMP_Text>();
            }
        }

        private void Start()
        {
            if (_tileText == null)
            {
                this.LogErrorInDevelopment("TileComponent: TMP_Text component is not assigned or found in children.");
            }   
        }

        public void SetContent(string content)
        {
            _tileText.text = content;
        }
    }
}
