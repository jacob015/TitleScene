using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceFarm
{
    public class Define
    {
        public enum EventType // ASTEROID : ���༺, COMET : ����, SUPERNOVA : �ʽż�
        {
            ASTEROID, COMET, SUPERNOVA, NONE
        }
        public enum GameState
        {
            START, PLAY, END, PAUSE, NONE
        }
        public enum UIType
        {
            ALL, BUTTON, IMAGE, CANVASGROUP, TMPRO, TOGGLE,SLIDER
        }

        public enum SunLightState
        {
            ON, OFF, NONE
        }

        public enum GrowthState
        {
            GROWTH, FINISH
        }
        
        public enum SunLightPoint
        {
            LEFT, CENTER, RIGHT
        }
    }
}
