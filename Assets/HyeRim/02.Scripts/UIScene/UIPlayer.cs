using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NHR
{
    //�÷��̾� ī�޶� ������ UI
    public class UIPlayer : MonoBehaviour
    {
        //������
        public TMP_Text txtEnergy;

        //�ڷ�ƾ
        public IEnumerator runningCoroutine;
        public IEnumerator energyChargingCoroutine;

        //���߿� �÷��̾�� �ű� ��
        //max ��
        private int maxEnergy = 10;
        public int nowEnergy = 10;

        //�޸��� �����ΰ�?
        public bool isRunning = false;
        //������ �ٿ� �����ΰ�?
        public bool isEnergyDown = false;

        private void Awake()
        {
            //�ڷ�ƾ �Ҵ�
            this.runningCoroutine = this.CRunning();
            this.energyChargingCoroutine = this.CEnergyCharging();
        }
        private void LateUpdate()
        {
            //LeftShift�� ������ ���� ��� �޸���
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //������ �ٿ� ���°� �ƴϰ�, �޸��� ���°� �ƴ϶�� �޸���
                if(!this.isRunning && !this.isEnergyDown)
                {
                    this.isRunning = !this.isRunning;
                    StartCoroutine(this.runningCoroutine);
                    StopCoroutine(this.energyChargingCoroutine);
                }
                Debug.Log("GetKeyDown LeftShift");
            }
            if(Input.GetKeyUp(KeyCode.LeftShift)) 
            {
                Debug.Log("GetKeyUp LeftShift");
                //�޸��� �����̰� ������ �ٿ� ���°� �ƴϰ� �ִ� �������� �ƴ϶�� �޸��� ���� ���߰� ���� ����
                if (this.isRunning && !this.isEnergyDown && this.nowEnergy < this.maxEnergy)
                {
                    this.isRunning = !this.isRunning;
                    StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                }

            }
        }
        //�޸��� ��
        public IEnumerator CRunning()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                //������ �Ҹ�
                this.nowEnergy--;
                this.txtEnergy.text = $"�޸��� On, ������ {this.nowEnergy}";
                
                //�������� �� ����ߴٸ� �޸��� �� �ڷ�ƾ ���߱�
                if (this.nowEnergy <= 0)
                {
                    this.txtEnergy.text = $"�ٿ� ����, ������ {this.nowEnergy}";
                    this.isRunning = false;
                    this.isEnergyDown = true;
                    StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                }
            }
        }

        //������ ���� ��
        public IEnumerator CEnergyCharging()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                //������ ����
                this.nowEnergy++;

                //������ �� �����Ǿ��ٸ� ���� �ڷ�ƾ ���߱�
                if (this.nowEnergy >= maxEnergy)
                {
                    this.isEnergyDown = false;
                    Debug.Log("���� ��");
                    StopCoroutine(this.energyChargingCoroutine);
                }
                if (this.isEnergyDown)
                {
                    this.txtEnergy.text = $"�ٿ� ����, ������ {this.nowEnergy}";
                }
                else
                {
                    this.txtEnergy.text = $"�޸��� Off, ������ {this.nowEnergy}";
                }
            }
        }
    }
}
