/***
 *
 * @Author: Roman
 * @Created on: 02-09-23
 *
 * @Copyright (c) by BorysProductions 2023. All rights reserved.
 *
 ***/

using BorysExperimental.Partials;
using BorysExperimental.Partials.Attributes;
using UnityEngine;

namespace BorysExperimental.Tests.Partials
{
    public class MyPartialBehaviour : PartialMonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            Debug.Log("MyPartialBehaviour.Awake()");
            MyCustomMethod();
        }
        
        [PartialMethodDefinition]
        private void MyCustomMethod()
        { 
            Debug.Log("MyPartialBehaviour.MyCustomMethod()");
            CallPartialMethod("MyCustomMethod");
        }

        [PartialMethod]
        private void StartTest()
        {
            Debug.Log("MyPartialBehaviour.StartTest()");
        }
        
        [PartialMethod]
        private void MyCustomMethodTest()
        {
            Debug.Log("MyPartialBehaviour.MyCustomMethodTest()");
        }
    }
}