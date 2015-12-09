using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    sealed class AliasAttribute : Attribute
    {
        public string Name { get; private set; }


        public AliasAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Alias must be at least one symbol.");
            }

            this.Name = name;
        }
    }
}