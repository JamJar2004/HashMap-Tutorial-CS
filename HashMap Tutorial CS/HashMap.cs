﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace HashMap_Tutorial_CS
{
	public class HashMap<K, V>
	{
		private Entry[] m_buckets;
		private int		m_count;
		
		private float m_loadFactor;
		private int	  m_maxCount;
		
		public HashMap(int initialCapacity = 16, float loadFactor = 0.75f)
		{
			m_buckets	 = new Entry[initialCapacity];
			m_count		 = 0;
			m_loadFactor = loadFactor;
			m_maxCount	 = (int)(loadFactor * initialCapacity);
		}
		
		private bool PlaceInBucket(int bucketIndex, K key, V value)
		{
			Entry lastEntry = null;
			Entry currEntry = m_buckets[bucketIndex];
		
			int keyHash = key == null ? 0 : key.GetHashCode();
		
			while(currEntry != null)
			{
				if(currEntry.HashCode == keyHash)
				{ 
					if(currEntry.Key.Equals(key))
					{
						currEntry.Value = value;
						return true;
					}
				}
				lastEntry = currEntry;
				currEntry = currEntry.Next;
			}
		
			Entry newEntry = new Entry(key, value);
			if(lastEntry == null)
				m_buckets[bucketIndex] = newEntry;
			else
				lastEntry.Next = newEntry;
		
			m_count++;
		
			if(m_count > m_maxCount)
				Reallocate();
		
			return false;
		}
		
		private Entry GetFromBucket(int bucketIndex, K key)
		{
			Entry currEntry = m_buckets[bucketIndex];
		
			int keyHash = key == null ? 0 : key.GetHashCode();
		
			while(currEntry != null)
			{
				if(currEntry.HashCode == keyHash)
				{
					if(currEntry.Key.Equals(key))
						return currEntry;
				}
				currEntry = currEntry.Next;
			}
		
			return null;
		}
		
		private bool RemoveFromBucket(int bucketIndex, K key)
		{
			Entry lastEntry = null;
			Entry currEntry = m_buckets[bucketIndex];
		
			int keyHash = key == null ? 0 : key.GetHashCode();
		
			while(currEntry != null)
			{
				if(currEntry.HashCode == keyHash)
				{
					if(currEntry.Key.Equals(key))
					{
						if(lastEntry == null)
							m_buckets[bucketIndex] = currEntry.Next;
						else
							lastEntry.Next = currEntry.Next;
		
						m_count--;
						return true;
					}
				}
				lastEntry = currEntry;
				currEntry = currEntry.Next;
			}
		
			return false;
		}
		
		private int GetBucketIndex(K key)
		{
			int keyHash = key == null ? 0 : key.GetHashCode();
			int index	= Math.Abs(keyHash % m_buckets.Length);
			return index;
		}
		
		private void Reallocate()
		{
			Entry[] newBuckets = new Entry[m_buckets.Length * 2];
			Entry[] oldBuckets = m_buckets;
		
			m_buckets = newBuckets;
			m_maxCount = (int)(m_loadFactor * m_buckets.Length);
			m_count = 0;
			foreach(Entry firstEntry in oldBuckets)
			{
				Entry currEntry = firstEntry;
				while(currEntry != null)
				{
					PlaceInBucket(GetBucketIndex(currEntry.Key), currEntry.Key, currEntry.Value);
					currEntry = currEntry.Next;
				}
			}
		}
		
		public int Count { get { return m_count; } }
		
		public V this[K key]
		{
			get
			{
				Entry foundEntry = GetFromBucket(GetBucketIndex(key), key);
				if(foundEntry == null)
				{
					V result = default(V);
					Place(key, result);
					return result;
				}
				return foundEntry.Value;
			}
			set
			{
				Place(key, value);
			}
		}
		
		public bool Place(K key, V value)
		{
			return PlaceInBucket(GetBucketIndex(key), key, value);
		}
		
		public bool Remove(K key)
		{
			return RemoveFromBucket(GetBucketIndex(key), key);
		}
		
		public void Clear()
		{
			for(int i = 0; i < m_buckets.Length; i++)
			{
				Entry currEntry = m_buckets[i];
				while(currEntry != null)
				{
					currEntry = currEntry.Next;
				}
				m_buckets[i] = null;
			}
			m_count = 0;
		}

        public KeyCollection   Keys   { get { return new KeyCollection(m_buckets);   } }
        public ValueCollection Values { get { return new ValueCollection(m_buckets); } }

        public EntryCollection Entries { get { return new EntryCollection(m_buckets); } }
		
		public class Entry
		{
			private int m_hashCode;
		
			private K m_key;
			private V m_value;
		
			private Entry m_next;
		
			public Entry(K key, V value)
			{
				m_hashCode = key == null ? 0 : key.GetHashCode();
		
				m_key	= key;
				m_value = value;
				m_next	= null;
			}
		
			public int HashCode { get { return m_hashCode; } }
		
			public K Key { get { return m_key; } }
		
			public V Value
			{
				get { return m_value;  }
				set { m_value = value; }
			}
		
			public Entry Next
			{
				get { return m_next;  }
				set { m_next = value; }
			}
		};

        public abstract class HashIterator<T> : IEnumerator<T>
        {
            private Entry[] m_buckets;

            private int   m_bucketIndex;
            private Entry m_currEntry;
            private Entry m_nextEntry;

            public HashIterator(Entry[] buckets)
            {
                m_buckets = buckets;
                Reset();
            }

            public Entry CurrentEntry { get { return m_currEntry; } }

            public abstract T Current { get; }

            object IEnumerator.Current { get { return Current; } }

            public void Dispose() {}

            public bool MoveNext()
            {
                if(m_nextEntry == null)
                    return false;

                m_currEntry = m_nextEntry;
                m_nextEntry = m_nextEntry.Next;
                while(m_nextEntry == null)
                {
                    m_bucketIndex++;
                    if(m_bucketIndex >= m_buckets.Length)
                        break;

                    m_nextEntry = m_buckets[m_bucketIndex];
                }
                return m_currEntry != null;
            }

            public void Reset()
            {
                m_bucketIndex = 0;
                m_nextEntry   = m_buckets[m_bucketIndex];
                while(m_nextEntry == null)
                {
                    m_bucketIndex++;
                    if(m_bucketIndex >= m_buckets.Length)
                        break;

                    m_nextEntry = m_buckets[m_bucketIndex];
                }
            }
        }

        public class KeyIterator : HashIterator<K>
        {
            public KeyIterator(Entry[] buckets) : base(buckets) {}

            public override K Current { get { return CurrentEntry.Key; } }
        }

        public class ValueIterator : HashIterator<V>
        {
            public ValueIterator(Entry[] buckets) : base(buckets) {}

            public override V Current { get { return CurrentEntry.Value; } }
        }

        public class EntryIterator : HashIterator<Entry>
        {
            public EntryIterator(Entry[] buckets) : base(buckets) {}

            public override Entry Current { get { return CurrentEntry; } }
        }

        public class KeyCollection : IEnumerable<K>
        {
            private Entry[] m_buckets;

            public KeyCollection(Entry[] buckets) { m_buckets = buckets; } 

            public IEnumerator<K> GetEnumerator() { return new KeyIterator(m_buckets); }

            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        }

        public class ValueCollection : IEnumerable<V>
        {
            private Entry[] m_buckets;

            public ValueCollection(Entry[] buckets) { m_buckets = buckets; } 

            public IEnumerator<V> GetEnumerator() { return new ValueIterator(m_buckets); }

            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        }

        public class EntryCollection : IEnumerable<Entry>
        {
            private Entry[] m_buckets;

            public EntryCollection(Entry[] buckets) { m_buckets = buckets; } 

            public IEnumerator<Entry> GetEnumerator() { return new EntryIterator(m_buckets); }

            IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        }
	}
}
