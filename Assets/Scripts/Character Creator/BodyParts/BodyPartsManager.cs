using System.Collections.Generic;
using UnityEngine;

public class BodyPartsManager : MonoBehaviour
{
    public SO_CharacterBody _characterBody;
    [SerializeField] private Animator _animator;
    [SerializeField] private string[] _bodyPartTypes;
    [SerializeField] private string[] _characterDirections;

    private AnimationClip _animationClip;
    private AnimatorOverrideController _animatorOverrideController;
    private AnimationClipOverrides _defaultAnimationClips;

    public void Start()
    {
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;

        _defaultAnimationClips = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
        _animatorOverrideController.GetOverrides(_defaultAnimationClips);

        UpdateBodyParts();
    }

    public void UpdateBodyParts()
    {
        for (int partIndex = 0; partIndex < _bodyPartTypes.Length; partIndex++)
        {

            string partType = _bodyPartTypes[partIndex];
            string partID = _characterBody.characterBodyParts[partIndex].bodyPart.bodyPartId.ToString();

           
            for (int directionIndex = 0; directionIndex < _characterDirections.Length; directionIndex++)
            {
                string direction = _characterDirections[directionIndex];

                _animationClip = Resources.Load<AnimationClip>("Animations/" + partType + "/" + partType + "_" + partID + "_" + direction);

                _defaultAnimationClips[partType + "_" + 0 + "_" + direction] = _animationClip;
            }
        }

        _animatorOverrideController.ApplyOverrides(_defaultAnimationClips);
    }

    class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }


        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}
