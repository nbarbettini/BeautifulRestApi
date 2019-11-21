using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;


namespace BeautifulRestApi.Models
{
    public class User
    {
        public User()
        {
        }

        public User(Guid guid, string name, string email)
        {
            Guid = guid;
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public Guid Guid { get; set; }
        public string Email { get; set; }

        private void FillDataSet(DataSet ds)
        {
            return;
        }

        protected bool Equals(User other)
        {
            return Name == other.Name && Guid.Equals(other.Guid) && Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Guid.GetHashCode();
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                return hashCode;
            }
        }

        private sealed class NameGuidEmailEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name == y.Name && x.Guid.Equals(y.Guid) && x.Email == y.Email;
            }

            public int GetHashCode(User obj)
            {
                unchecked
                {
                    var hashCode = (obj.Name != null ? obj.Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Guid.GetHashCode();
                    hashCode = (hashCode * 397) ^ (obj.Email != null ? obj.Email.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<User> NameGuidEmailComparer { get; } = new NameGuidEmailEqualityComparer();
    }
}