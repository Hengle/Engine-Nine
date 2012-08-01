﻿namespace Nine.Content.Pipeline.Xaml
{
    using System;

    public struct PropertyInstance : IEquatable<PropertyInstance>
    {
        public object Target;
        public string TargetProperty;

        public PropertyInstance(object target, string targetProperty)
        {
            Target = target;
            TargetProperty = targetProperty;
        }

        public bool Equals(PropertyInstance other)
        {
            return Target == other.Target && TargetProperty == other.TargetProperty;
        }

        public override bool Equals(object obj)
        {
            if (obj is PropertyInstance)
                return Equals((PropertyInstance)obj);
            return false;
        }

        public static bool operator ==(PropertyInstance value1, PropertyInstance value2)
        {
            return ((value1.Target == value2.Target) && (value1.TargetProperty == value2.TargetProperty));
        }

        public static bool operator !=(PropertyInstance value1, PropertyInstance value2)
        {
            return !(value1.Target == value2.Target && value1.TargetProperty == value2.TargetProperty);
        }

        public override int GetHashCode()
        {
            return (Target != null ? Target.GetHashCode() : 0) ^ (TargetProperty != null ? TargetProperty.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return (Target != null ? Target.GetType().Name : "") + "." + (TargetProperty != null ? TargetProperty : "");
        }
    }
}