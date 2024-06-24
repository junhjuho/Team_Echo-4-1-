using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SeongMin;
using Jaewook;

namespace Michsky.UI.Dark
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class SwitchManager : MonoBehaviour
    {
        // Events
        public UnityEvent onEvents;
        public UnityEvent offEvents;

        // Saving
        public bool saveValue = true;
        public string switchTag = "Switch";

        // Settings
        public bool isOn = true;
        public bool invokeAtStart = true; 

        // Resources
        public Animator switchAnimator;
        public Button switchButton;

        // SoundController
        public Jaewook.SoundController soundController;
        public bool isSoundOn = true;

        void Awake()
        {
            Jaewook.SoundController soundController = GetComponent<Jaewook.SoundController>();

            if (switchAnimator == null) { switchAnimator = gameObject.GetComponent<Animator>(); }
            if (switchButton == null)
            {
                switchButton = gameObject.GetComponent<Button>();
                switchButton.onClick.AddListener(AnimateSwitch);
            }
        }

        void Start()
        {
            if (saveValue == true)
            {
                if (PlayerPrefs.GetString(switchTag + "DarkUISwitch") == "")
                {
                    if (isOn == true)
                    {
                        switchAnimator.Play("Switch On");
                        isOn = true;
                        PlayerPrefs.SetString(switchTag + "DarkUISwitch", "true");
                    }

                    else
                    {
                        switchAnimator.Play("Switch Off");
                        isOn = false;
                        PlayerPrefs.SetString(switchTag + "DarkUISwitch", "false");
                    }
                }

                else if (PlayerPrefs.GetString(switchTag + "DarkUISwitch") == "true")
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else if (PlayerPrefs.GetString(switchTag + "DarkUISwitch") == "false")
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            else
            {
                if (isOn == true)
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            if (invokeAtStart == true && isOn == true)
                onEvents.Invoke();
            else if (invokeAtStart == true && isOn == false)
                offEvents.Invoke();
        }

        void OnEnable()
        {
            

            if (switchAnimator == null)
                return;

            if (saveValue == true)
            {
                if (PlayerPrefs.GetString(switchTag + "DarkUISwitch") == "")
                {
                    if (isOn == true)
                    {
                        switchAnimator.Play("Switch On");
                        isOn = true;
                        PlayerPrefs.SetString(switchTag + "DarkUISwitch", "true");
                    }

                    else
                    {
                        switchAnimator.Play("Switch Off");
                        isOn = false;
                        PlayerPrefs.SetString(switchTag + "DarkUISwitch", "false");
                    }
                }

                else if (PlayerPrefs.GetString(switchTag + "DarkUISwitch") == "true")
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else if (PlayerPrefs.GetString(switchTag + "DarkUISwitch") == "false")
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            else
            {
                if (isOn == true)
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;

                    soundController?.loopSource.Play();
                }

                else
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;

                    soundController?.loopSource.Pause();
                }
            }

            
        }

        public void AnimateSwitch()
        {

            if (isOn == true)
            {

                soundController?.StartLoopClip();
                


                switchAnimator.Play("Switch Off");
                isOn = false;
                offEvents.Invoke();

                if (saveValue == true)
                    PlayerPrefs.SetString(switchTag + "DarkUISwitch", "false");
            }

            else
            {
                soundController?.StopLoopClip();
                switchAnimator.Play("Switch On");
                isOn = true;
                onEvents.Invoke();

                if (saveValue == true)
                    PlayerPrefs.SetString(switchTag + "DarkUISwitch", "true");
            }
        }
    }
}