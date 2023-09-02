/***
 *
 * @Author: Roman
 * @Created on: 02-09-23
 *
 * @Copyright (c) by BorysProductions 2023. All rights reserved.
 *
 ***/

using UnityEngine;
using BorysExperimental.Partials.Attributes;

namespace BorysExperimental.Partials
{
    public class PartialMonoBehaviour : MonoBehaviour
    {
        private PartialController _partialController;

        #region ## Constructor ##

        protected PartialMonoBehaviour()
        {
            GetPartialController().Constructor();
        }

        #endregion

        #region ## Default Partials ##

        [PartialMethodDefinition]
        protected virtual void Awake()
        {
            Debug.Log("PartialMonoBehaviour.Awake()");
            GetPartialController().Awake();
        }
        
        [PartialMethodDefinition]
        protected virtual void Start()
        {
            GetPartialController().Start();
        }
        
        [PartialMethodDefinition]
        protected virtual void OnEnable()
        {
            GetPartialController().OnEnable();
        }
        
        [PartialMethodDefinition]
        private void OnDisable()
        {
            GetPartialController().OnDisable();
        }
        
        [PartialMethodDefinition]
        protected virtual void Update()
        {
            GetPartialController().Update();
        }
        
        [PartialMethodDefinition]
        protected virtual void FixedUpdate()
        {
            GetPartialController().FixedUpdate();
        }
        
        [PartialMethodDefinition]
        protected virtual void LateUpdate()
        {
            GetPartialController().LateUpdate();
        }
        
        [PartialMethodDefinition]
        protected virtual void OnDestroy()
        {
            GetPartialController().OnDestroy();
        }

        #endregion

        #region ## Core ##

        protected PartialController GetPartialController()
        {
            return _partialController ??= new PartialController(this, false);
        }
        
        protected void CallPartialMethod(string methodName)
        {
            GetPartialController().CallPartialMethod(methodName);
        }

        #endregion
    }
}