using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFeedback : MonoBehaviour
{
    public Slider UsefulSlider;
    public Slider MotivatedSlider;
    public Slider SkillsSlider;
    public Slider EnjoySlider;
    public Toggle FunToggle;
    public Toggle CurioToggle;
    public Toggle CompToggle;
    public Toggle PracToggle;
    public Toggle OtherToggle;

    public void Submit(){
    LogScript.WriteNewLogEntry("Feedback","Useful",UsefulSlider.value.ToString());
    LogScript.WriteNewLogEntry("Feedback","Motivated",MotivatedSlider.value.ToString());
    LogScript.WriteNewLogEntry("Feedback","Skills",SkillsSlider.value.ToString());
    LogScript.WriteNewLogEntry("Feedback","Enjoyed",EnjoySlider.value.ToString());
    LogScript.WriteNewLogEntry("Feedback","Fun",FunToggle.isOn.ToString());
    LogScript.WriteNewLogEntry("Feedback","Curious",CurioToggle.isOn.ToString());
    LogScript.WriteNewLogEntry("Feedback","Competition",CompToggle.isOn.ToString());
    LogScript.WriteNewLogEntry("Feedback","Practice",PracToggle.isOn.ToString());
    LogScript.WriteNewLogEntry("Feedback","Other",OtherToggle.isOn.ToString());
    }
}
