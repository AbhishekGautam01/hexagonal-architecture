using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Accounting.Domain.ValueObjects
{
    public sealed class Aadhar
    {
        public string _aadhar { get; private set; }
        const string RegExForValidation = @"^[2-9]{1}[0-9]{3}\\s[0-9]{4}\\s[0-9]{4}$";
        public Aadhar(string aadhar)
        {
            if (string.IsNullOrWhiteSpace(aadhar))
                throw new AadharShouldNotBeEmptyException("The 'aadhar' field is required");
            Regex regex = new Regex(RegExForValidation);
            Match match = regex.Match(aadhar);
            if(!match.Success)
                throw new InvalidAadharException("Invalid Aadhar format. Use XXXX XXXX XXXX");
            _aadhar = aadhar;
        }

        public override string ToString()
        {
            return _aadhar.ToString();
        }

        public static implicit operator Aadhar(string aadhar)
        {
            return new Aadhar(aadhar);
        }

        public static implicit operator string(Aadhar aadhar)
        {
            return aadhar._aadhar;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is string ? (string)obj == _aadhar : ((Aadhar)obj)._aadhar == _aadhar;
        }

        public override int GetHashCode()
        {
            return _aadhar.GetHashCode();
        }
    }
}
