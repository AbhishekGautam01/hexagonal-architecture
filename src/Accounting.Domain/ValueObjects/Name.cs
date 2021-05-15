using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.ValueObjects
{
    public sealed class Name
    {
        private string _name;
        public Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameShouldNotBeEmptyException($"The name {name} is not a valid name");
            _name = name;
        }

        public override string ToString()
        {
            return _name.ToString();
        }

        public static implicit operator Name(string name)
        {
            return new Name(name);
        }

        public static implicit operator string(Name name)
        {
            return name._name;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is string ? (string)obj == _name : ((Name)obj)._name == _name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}
