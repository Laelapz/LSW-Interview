using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsManager : MonoBehaviour
{
    [SerializeField] private SO_CharacterBody characterBody;

    [SerializeField] private string[] bodyPartTypes;
    [SerializeField] private string[] characterDirections;

    [SerializeField] private Animator animator;
    private AnimationClip animationClip;
    private AnimatorOverrideController animatorOverrideController;
    private AnimationClipOverrides defaultAnimationClips;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        defaultAnimationClips = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(defaultAnimationClips);

        UpdateBodyParts();
    }

    // Update is called once per frame
    public void UpdateBodyParts()
    {
        for (int partIndex = 0; partIndex < bodyPartTypes.Length; partIndex++)
        {
            // Get current body part
            string partType = bodyPartTypes[partIndex];
            // Get current body part ID
            string partID = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartId.ToString();

           
            for (int directionIndex = 0; directionIndex < characterDirections.Length; directionIndex++)
            {
                string direction = characterDirections[directionIndex];

                // Get players animation from player body
                // ***NOTE: Unless Changed Here, Animation Naming Must Be: "[Type]_[Index]_[state]_[direction]" (Ex. Body_0_idle_down)
                animationClip = Resources.Load<AnimationClip>("Player Animations/" + partType + "/" + partType + "_" + partID + "_" + direction);

                // Override default animation
                defaultAnimationClips[partType + "_" + 0 + "_" + direction] = animationClip;
            }
        }

        // Apply updated animations
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
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
