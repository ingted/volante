namespace NachoDB
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary> Class representing relation between owner and members
    /// </summary>
    public abstract class Relation<M,O> : PersistentCollection<M>, Link<M> where M:class,IPersistent where O:class,IPersistent
    {
        public abstract int Size();

        public abstract int Length 
        {
            get;
            set;
        }

        public abstract M this[int i] 
        {
            get;
            set;
        }
		
        public abstract M Get(int i);
		
        public abstract IPersistent GetRaw(int i);
		
        public abstract void  Set(int i, M obj);

        public abstract void  RemoveAt(int i);
        public abstract void  Remove(int i);

        public abstract void  Insert(int i, M obj);
				
        public abstract void  AddAll(M[] arr);
		
        public abstract void  AddAll(M[] arr, int from, int length);
		
        public abstract void  AddAll(Link<M> anotherLink);
      
        public abstract M[] ToArray();

        public abstract Array ToRawArray();

        public abstract Array ToArray(Type elemType);

        public abstract bool  ContainsElement(int i, M obj);

        public abstract int   IndexOf(M obj);
		
        public abstract void  Pin();

        public abstract void  Unpin();
 
        public virtual O Owner
        {
            /// <summary> Get relation owner
            /// </summary>
            /// <returns>owner of the relation
            /// 
            /// </returns>
            get
            {
                return owner;
            }
			
            /// <summary> Set relation owner
            /// </summary>
            /// <param name="owner">new owner of the relation
            /// 
            /// </param>
            set
            {
                this.owner = value;
                Modify();
            }			
        }

        /// <summary> Relation constructor. Creates empty relation with specified owner and no members. 
        /// Members can be added to the relation later.
        /// </summary>
        /// <param name="owner">owner of the relation
        /// 
        /// </param>		
        public Relation(O owner)
        {
            this.owner = owner;
        }
		
        internal Relation() {}

        public void SetOwner(IPersistent obj)
        { 
             owner = (O)obj;
        }

        private O owner;
    }
}