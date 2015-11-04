using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Voxel
{
    public struct BlockData
    {
        public int BlockType;
        public int ByteData;
        public Dictionary<string, object> NamedData;

        public Block Block
        {
            get
            {
                Block ret;
                if (Blocks.All.TryGetValue(BlockType, out ret))
                {
                    return ret;
                }
                return Blocks.Air;
            }
            set
            {
                BlockType = value.Name.GetHashCode();
            }
        }

        public bool HasData { get { return NamedData != null; } }
        public T GetNamedData<T>(string key) where T : System.IConvertible
        {
            if(!HasData) return default(T);
            object ret;
            if (NamedData.TryGetValue(key, out ret))
            {
                return (T)ret;
            }
            return default(T);
        }
        public List<T> GetNamedDataList<T>(string key) where T : System.IConvertible
        {
            if (!HasData) return null;
            object ret;
            if (NamedData.TryGetValue(key, out ret))
            {
                return (List<T>)ret;
            }
            return null;
        }
    }
}

