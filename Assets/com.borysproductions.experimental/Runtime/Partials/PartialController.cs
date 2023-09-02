/***
 *
 * @Author: Roman
 * @Created on: 02-09-23
 *
 * @Copyright (c) by BorysProductions 2023. All rights reserved.
 *
 ***/

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BorysExperimental.Partials.Attributes;
using UnityEngine;

namespace BorysExperimental.Partials
{
    public class PartialController
    {
        #region ## Fields ##

        private const string ConstructorMethodName = "Constructor";
        private const string AwakeMethodName = "Awake";
        private const string StartMethodName = "Start";
        private const string OnEnableMethodName = "OnEnable";
        private const string OnDisableMethodName = "OnDisable";
        private const string UpdateMethodName = "Update";
        private const string FixedUpdateMethodName = "FixedUpdate";
        private const string LateUpdateMethodName = "LateUpdate";
        private const string OnDestroyMethodName = "OnDestroy";

        private readonly MonoBehaviour _host;
        private readonly Dictionary<string, List<MethodInfo>> _methods = new();
        private readonly BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private List<string> _methodNameProviders = new();
        private bool _initialized;
        private bool _showLogs;

        #endregion

        #region ## Constructor ##

        public PartialController(MonoBehaviour host, bool showLogs = false)
        {
            _host = host;
            _showLogs = showLogs;
            Initialize();
        }

        #endregion

        #region ## Initialization ##

        private void Initialize()
        {
            if (_initialized) return;

            Log($"Initializing...");
            CollectMethods();
            CollectMethodsAttributeBased();
            _initialized = true;
        }
        
        #endregion
        
        #region ## Collecting Methods ##

        private void CollectMethodsAttributeBased()
        {
            Log($"Collecting methods by attribute...");
            var hostType = _host.GetType();
            
            // Collect partial definitions first
            var partialDefinitions = hostType.GetMethods(_bindingFlags)
                .Where(x => x.GetCustomAttribute<PartialMethodDefinitionAttribute>() != null);

            foreach (var method in partialDefinitions)
            {
                var attribute = method.GetCustomAttribute<PartialMethodDefinitionAttribute>();
                var targetMethod = attribute.CustomMethodName ?? method.Name;
                if (!_methods.ContainsKey(targetMethod))
                {
                    _methods.Add(targetMethod, new List<MethodInfo>());
                    _methodNameProviders.Add(targetMethod);
                }
                
                Log($"EntityController-{hostType.Name} - Successfully registered method {method.Name}");
            }
            
            // Collect partial invocations
            var methods = hostType.GetMethods(_bindingFlags)
                .Where(x => x.GetCustomAttribute<PartialMethodAttribute>() != null);

            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<PartialMethodAttribute>() == null)
                {
                    continue;
                }

                if (!_methods.ContainsKey(method.Name))
                {
                    _methods.Add(method.Name, new List<MethodInfo>());
                }

                _methods[method.Name].Add(method);
                Log($"EntityController-{hostType.Name} - Successfully registered method {method.Name}");
            }
        }

        protected virtual void CollectMethods()
        {
            Log($"Collecting methods...");
            
            // Collect default partial definitions in this class first
            var fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var fieldInfo in fields)
            {
                var field = fieldInfo.GetValue(this) as string;
                if (field != null && _methods.ContainsKey(field) == false)
                {
                    Log($"Collecting field {field}");
                    _methodNameProviders.Add(field);
                    _methods.Add(field, new List<MethodInfo>());
                }
                else
                {
                    if (field != null) _methods[field] = new List<MethodInfo>();
                }
            }
            
            // Collect partial invocations in host class
            var type = _host.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.GetCustomAttribute<PartialMethodAttribute>() != null);

            foreach (var method in methods)
            {
                var targetMethod = "";
                foreach (var field in _methodNameProviders)
                {
                    if (method.Name.StartsWith(field))
                    {
                        targetMethod = field;
                    }
                }

                if (!_methods.ContainsKey(targetMethod))
                {
                    _methods.Add(targetMethod, new List<MethodInfo>());
                }

                _methods[targetMethod].Add(method);
                Log($"Successfully registered method '{method.Name}'");
            }
        }

        #endregion

        #region ## Core ##

        public void CallPartialMethod(string methodName)
        {
            if (!_methods.TryGetValue(methodName, out var methods) || methods.Count <= 0)
            {
                return;
            }
            
            Log($"Calling method {methodName}");
            foreach (var method in methods)
            {
                method.Invoke(_host, null);
            }
        }

        #endregion

        #region ## Partial Invocations ##
        
        

        public virtual void Constructor()
        {
            CallPartialMethod(ConstructorMethodName);
        }

        public virtual void Awake()
        {
            CallPartialMethod(AwakeMethodName);
        }

        public virtual void Start()
        {
            CallPartialMethod(StartMethodName);
        }

        public virtual void OnEnable()
        {
            CallPartialMethod(OnEnableMethodName);
        }

        public virtual void OnDisable()
        {
            CallPartialMethod(OnDisableMethodName);
        }

        public virtual void Update()
        {
            CallPartialMethod(UpdateMethodName);
        }

        public virtual void FixedUpdate()
        {
            CallPartialMethod(FixedUpdateMethodName);
        }

        public virtual void LateUpdate()
        {
            CallPartialMethod(LateUpdateMethodName);
        }

        public virtual void OnDestroy()
        {
            CallPartialMethod(OnDestroyMethodName);
        }

        #endregion

        #region ## Utils ##

        private void Log(string message)
        {
            if (!_showLogs) return;
            Debug.Log($"[PartialController]::[{_host.GetType().Name}]::{message}");
        }

        #endregion
    }
}