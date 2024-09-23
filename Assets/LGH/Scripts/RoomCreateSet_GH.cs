using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreateSet_GH : MonoBehaviour
{
    //Dropdown 제어를 위한 TMP_Dropdown타입 변수와 
    //Dropdown의 옵션 데이터로 활용되는 문자열 배열 변수 arrayClass를 선언합니다.

    [SerializeField]
    private TMP_Dropdown dropdown;
    private string[] gameArray = new string[] { "롤", "오버워치", "FIFA", "Game & Watch" };
    private string[] financeArray = new string[] { "주식", "부동산", "재테크" };
    private string[] musicArray = new string[] { "K-POP", "발라드", "밴드", "락" };
    private string[] lifeArray = new string[] { "인테리어", "자동차", "요리", "거지방ㅋ" };

    public TMP_Dropdown cateDD;

    int valueSave = 0;

    public Slider cntSlider;

    public TMP_Text cntText;
    private void Update()
    {
        if (valueSave != cateDD.value)
        {
            DropBoxValueChange(cateDD.value);
            valueSave = cateDD.value;
        }

        cntText.text = cntSlider.value + "/" + cntSlider.maxValue;
    }

    public void DropBoxValueChange(int index)
    {
        switch (index)
        {
            case 0:
                {

                    // 현재 dropdown에 있는 모든 옵션을 제거
                    dropdown.ClearOptions();

                    // 새로운 옵션 설정을 위한 OptionData 생성
                    List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

                    // arrayClass 배열에 있는 모든 문자열 데이터를 불러와서 optionList에 저장
                    foreach (string str in gameArray)
                    {
                        optionList.Add(new TMP_Dropdown.OptionData(str));
                    }

                    // 위에서 생성한 optionList를 dropdown의 옵션 값에 추가
                    dropdown.AddOptions(optionList);

                    // 현재 dropdown에 선택된 옵션을 0번으로 설정
                    dropdown.value = 0;
                }
                break;
            case 1:
                {

                    // 현재 dropdown에 있는 모든 옵션을 제거
                    dropdown.ClearOptions();

                    // 새로운 옵션 설정을 위한 OptionData 생성
                    List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

                    // arrayClass 배열에 있는 모든 문자열 데이터를 불러와서 optionList에 저장
                    foreach (string str in financeArray)
                    {
                        optionList.Add(new TMP_Dropdown.OptionData(str));
                    }

                    // 위에서 생성한 optionList를 dropdown의 옵션 값에 추가
                    dropdown.AddOptions(optionList);

                    // 현재 dropdown에 선택된 옵션을 0번으로 설정
                    dropdown.value = 0;
                }
                break;
            case 2:
                {

                    // 현재 dropdown에 있는 모든 옵션을 제거
                    dropdown.ClearOptions();

                    // 새로운 옵션 설정을 위한 OptionData 생성
                    List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

                    // arrayClass 배열에 있는 모든 문자열 데이터를 불러와서 optionList에 저장
                    foreach (string str in musicArray)
                    {
                        optionList.Add(new TMP_Dropdown.OptionData(str));
                    }

                    // 위에서 생성한 optionList를 dropdown의 옵션 값에 추가
                    dropdown.AddOptions(optionList);

                    // 현재 dropdown에 선택된 옵션을 0번으로 설정
                    dropdown.value = 0;
                }
                break;
            case 3:
                {

                    // 현재 dropdown에 있는 모든 옵션을 제거
                    dropdown.ClearOptions();

                    // 새로운 옵션 설정을 위한 OptionData 생성
                    List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

                    // arrayClass 배열에 있는 모든 문자열 데이터를 불러와서 optionList에 저장
                    foreach (string str in lifeArray)
                    {
                        optionList.Add(new TMP_Dropdown.OptionData(str));
                    }

                    // 위에서 생성한 optionList를 dropdown의 옵션 값에 추가
                    dropdown.AddOptions(optionList);

                    // 현재 dropdown에 선택된 옵션을 0번으로 설정
                    dropdown.value = 0;
                }
                break;
        }

    }
}
