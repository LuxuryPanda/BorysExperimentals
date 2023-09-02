/***
 *
 * @Author: Roman
 * @Created on: 02-09-23
 *
 * @Copyright (c) by BorysProductions 2023. All rights reserved.
 *
 ***/

using System;

namespace BorysExperimental.Partials.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PartialMethodDefinitionAttribute : Attribute
    {
        public readonly string CustomMethodName;

        public PartialMethodDefinitionAttribute(string customMethodName = null)
        {
            CustomMethodName = customMethodName;
        }
    }
}