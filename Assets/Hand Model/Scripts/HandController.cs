using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public enum HandType { HEADSET, LEFT_CONTROLLER, RIGHT_CONTROLLER }
    public HandType handType = HandType.LEFT_CONTROLLER;

    private InputDevice targetDevice;
    private Animator anim;
    private bool isSetting = false;
    private int container;

    IEnumerator Start()
    {
        anim = this.GetComponent<Animator>();
        List<InputDevice> devices = new List<InputDevice>(); //디바이스들을 담을 변수 생성

        while (true)
        {
            yield return new WaitForSeconds(1f);
            InputDevices.GetDevices(devices); //연결된 디바이스 목록 불러와서 devices 변수에 담기

            if (devices.Count > 1)
            {
                for (int i = 0; i < devices.Count; i++)
                {
                    if (handType == HandType.LEFT_CONTROLLER)
                    {
                        if (devices[i].name.Contains("Left"))
                        {
                            Debug.Log(devices[i].name);
                            targetDevice = devices[i];
                        }
                    }
                    else if (handType == HandType.RIGHT_CONTROLLER)
                    {
                        if (devices[i].name.Contains("Right"))
                        {
                            Debug.Log(devices[i].name);
                            targetDevice = devices[i];
                        }
                    }
                }
                
                break;
            }
        }

        isSetting = true;
    }

    void Update()
    {
        if (!isSetting) return;

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            anim.SetFloat("Trigger", triggerValue);
        else
            anim.SetFloat("Trigger", 0);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            anim.SetFloat("Grip", gripValue);
        else
            anim.SetFloat("Grip", 0);
        
    }
}
