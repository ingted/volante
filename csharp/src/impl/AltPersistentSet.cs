namespace Volante.Impl        
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using Volante;
    
    class AltPersistentSet<T> : AltBtree<T,T>, Volante.ISet<T> where T:class,IPersistent
    {
        public AltPersistentSet() 
        { 
            type = ClassDescriptor.FieldType.tpObject;
            unique = true;
        }

        public override bool Contains(T o) 
        {
            Key key = new Key(o);
            IEnumerator<T> e = GetEnumerator(key, key, IterationOrder.AscentOrder);
            return e.MoveNext();
        }
    
        public override void Add(T o) 
        { 
            base.Put(new Key(o), o);
        }

        public bool AddAll(ICollection<T> c) 
        {
            bool modified = false;
            foreach (T o in c)
            {
                modified |= base.Put(new Key(o), o);
            }
            return modified;
        }

        public override bool Remove(T o) 
        { 
            try 
            { 
                Remove(new Key(o), o);
            } 
            catch (StorageError x) 
            { 
                if (x.Code == StorageError.ErrorCode.KEY_NOT_FOUND) 
                { 
                    return false;
                }
                throw;
            }
            return true;
        }
    
        public bool ContainsAll(ICollection<T> c) 
        { 
            foreach (T o in c)
            { 
                if (!Contains(o)) 
                {
                    return false;
                }
            }
            return true;
        }

        public bool RemoveAll(ICollection<T> c) 
        {
            bool modified = false;
            foreach (T o in c)
            {
                modified |= Remove(o);
            }
            return modified;
        }

        public override bool Equals(object o) 
        {
            if (o == this) 
            {
                return true;
            }
            Volante.ISet<T> s = o as Volante.ISet<T>;
            if (s == null) 
            {
                return false;
            }
            if (Count != s.Count) 
            {
                return false;
            }
            return ContainsAll(s);
        }

        public override int GetHashCode() 
        {
            int h = 0;
            foreach (IPersistent o in this) 
            { 
                h += o.Oid;
            }
            return h;
        }
    }
}
