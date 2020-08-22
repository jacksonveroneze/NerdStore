using System;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
            => Id = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            Entity compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
            => !(a == b);

        public override int GetHashCode()
            => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString()
            => $"{GetType().Name} [Id={Id}]";
    }
}
