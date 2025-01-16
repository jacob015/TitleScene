using UnityEngine;

namespace Anibel
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "Animal/Data", order = 0)]
    public class AnimalData : ScriptableObject
    {
        [SerializeField] private Sprite _dropSprite;

        public Sprite DropSprite
        {
            get => _dropSprite;
            set => _dropSprite = value;
        }

        [SerializeField] private Sprite _nervousSprite;

        public Sprite NervousSprite
        {
            get => _nervousSprite;
            set => _nervousSprite = value;
        }

        [SerializeField] private Sprite _normalSprite;

        public Sprite NormalSprite
        {
            get => _normalSprite;
            set => _normalSprite = value;
        }

        [SerializeField] private Sprite _surprisedSprite;

        public Sprite SurprisedSprite
        {
            get => _surprisedSprite;
            set => _surprisedSprite = value;
        }
    }
}