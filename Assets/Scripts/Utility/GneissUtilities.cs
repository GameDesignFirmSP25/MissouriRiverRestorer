using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GneissUtilities
{
    public class BoolEvent : UnityEvent<bool> { }
    public class IntEvent : UnityEvent<int> { }
    public class FloatEvent : UnityEvent<float> { }
    public class Vector3Event : UnityEvent<Vector3> { }



    public static class RaycastUtilities
    {
        public static RaycastHit[] SortHitsByDistance(this RaycastHit[] hits)
        {
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            System.Array.Sort(distances, hits);
            return hits;
        }
    }

    public static class ABC
    {
        private static string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static char GetLetter(int i)
        {
            if (i < 0 || i > letters.Length) { return ' '; }
            //todo: return AA for int i = 26, AB for 27, AC 28, etc.
            return letters[i];
        }
    }

    public static class CollectionExtensions
    {

        public static void RandomizeContents<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            int n = 0;
            int o = list.Count;
            while (n < o)
            {
                newList.Add(list.RemoveFrom());
                n = newList.Count;
                o = list.Count;
            }
        }

        public static bool ContainsReversePair<T>(this List<System.Tuple<T, T>> collection, System.Tuple<T, T> toCheck)
        {
            foreach (System.Tuple<T, T> pairing in collection)
            {
                if (ReferenceEquals(pairing.Item1, toCheck.Item2) && ReferenceEquals(pairing.Item2, toCheck.Item1))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Removes a random element from a collection and returns the removed element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T RemoveFrom<T>(this T[] collection) //todo - test using array
        {
            int index = Random.Range(0, collection.Length);
            T toRemove = collection[index];

            T[] tempCollection = new T[collection.Length - 1];
            for (int i = 0; i < collection.Length; i++)
            {
                if (!collection[i].Equals(toRemove))
                {
                    tempCollection[i] = collection[i];
                }
            }
            tempCollection = collection.ConvertToList().RemoveNullReferences().ToArray();
            collection = tempCollection;
            return toRemove;
        }

        public static List<T> ConvertToList<T>(this T[] array)
        {
            List<T> list = new List<T>();
            foreach (T element in array)
            {
                list.Add(element);
            }
            return list;
        }

        public static List<T> RemoveNullReferences<T>(this List<T> list)
        {
            int c = 0;
            int count = list.Count;
            while (c < count)
            {
                if (list[c] == null)
                {
                    list.RemoveAt(c);
                    count--;
                }
                else
                {
                    c++;
                }
            }
            return list;
        }


        /// <summary>
        /// Removes a random element from a collection and returns the removed element, updating the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T RemoveFrom<T>(this List<T> collection)
        {
            int index = Random.Range(0, collection.Count);
            T toRemove = collection[index];
            collection.Remove(toRemove);
            return toRemove;
        }

        /// <summary>
        /// Remove a specific element from the collection and return it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T RemoveFrom<T>(this List<T> collection, T element)
        {
            collection.Remove(element);
            return element;
        }


        public static string PrintCollection<T>(this T[] collection)
        {
            string s = "";
            foreach (T element in collection)
            {
                if (element == null)
                {
                    s += "null, ";
                }
                else
                {
                    s += element.ToString() + ", ";
                }
            }
            return s;
        }

        public static string PrintCollection<T>(this List<T> collection, string deliniation = "")
        {
            string s = "";
            foreach (T element in collection)
            {
                if (element == null)
                {
                    s += "null" + deliniation;
                }
                else
                {
                    s += element.ToString() + deliniation;
                }
            }
            return s;
        }
    }




    public static class MathExtensions
    {

        public static int PlusOrMinus(this int i)
        {
            return i * Random.Range(0, 2) * 2 - 1;
        }

        public static float PlusOrMinus(this float i)
        {
            return i * (Random.Range(0, 2) * 2 - 1);
        }

        /*
         * DIVISIBILTY OF INTS
         */
        public static int[] GetDivisibility(this int i)
        {
            List<int> _divisibles = i.GetDivisibilityList();

            //Convert to array
            return _divisibles.ToArray();
        }


        public static List<int> GetDivisibilityList(this int i)
        {
            List<int> _divisibles = new List<int>();

            for (int x = 1; x < Mathf.Infinity; x++)
            {
                if (x > i) { break; }
                if (CheckDivisibility(i, x))
                {
                    _divisibles.Add(x);
                }
            }

            return _divisibles;
        }

        public static bool CheckDivisibility(this int i, int divisor)
        {
            return i % divisor == 0;
        }

        public static Vector3Int ToVector3(this int i)
        {
            return new Vector3Int(i, i, i);
        }

        public static Vector3 ToVector3(this float f)
        {
            return new Vector3(f, f, f);
        }

        public static Vector2Int ToVector2(this int i)
        {
            return new Vector2Int(i, i);
        }

        public static Vector2 ToVector2(this float f)
        {
            return new Vector2(f, f);
        }
    }

    public static class Vector3Extensions
    {
        public static Vector2 To2D(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }
    }


    public static class TerrainExtensions
    {
        public static TerrainData GetCurrentTerrainData(this Terrain terrain)
        {
            if (terrain != null)
            {
                return terrain.terrainData;
            }
            return default(TerrainData);
        }

        public static Vector3 GetTerrainSize(this Terrain terrain)
        {
            if (terrain != null)
            {
                return terrain.terrainData.size;
            }
            return Vector3.zero;
        }
    }


    public static class RendererExtensions
    {
        public static void SetColor(this Renderer renderer, Color color)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);
            block.SetColor("_Color", color);
            renderer.SetPropertyBlock(block);
        }

        public static bool IsOnScreen(this GameObject g, Camera camera)
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(g.transform.position);
            return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
    }



    public static class StringExtensions
    {
        public static string RemoveRandomIndexC(this string s)
        {
            return s.Remove(Random.Range(0, s.Length));
        }

        public static string RandomizeStringC(this string s)
        {
            string S = "";
            //add letters to list
            List<char> letters = new List<char>();
            for (int i = 0; i < s.Length; i++)
            {
                letters.Add(s[i]);
            }

            int L = letters.Count;
            while (L > 0)
            {
                char randomLetter = letters[Random.Range(0, letters.Count)];

                S += randomLetter;
                letters.Remove(randomLetter);
                L--;
            }
            return S;
        }
    }


    public static class TransformExtensions
    {
        public static Transform[] GetImmediateChildArray(this Transform t)
        {
            Transform[] array = new Transform[t.childCount];
            for (int i = 0; i < t.childCount; i++)
            {
                array[i] = t.GetChild(i);
            }
            return array;
        }
    }


    public static class ColorExtensions
    {
        public static Color GetRandomColor()
        {
            return new Color(UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1));
        }

        public static Color GetRainbowColor(int index = -1)
        {
            Color[] colors = {new Color(1f, 0f, 0f)
                        , new Color(1f, .5f, 0f)
                        , new Color(1f, 1f, 0f)
                        , new Color(.5f, 1f, 0f)
                        , new Color(0f, 1f, 0f)
                        , new Color(0f, 1f, .5f)
                        , new Color(0f, 1f, 1f)
                        , new Color(0f, .5f, 1f)
                        , new Color(0f, 0f, 1f)
                        , new Color(.5f, 0f, 1f)
                        , new Color(1f, 0f, 1f)
                        , new Color(1f, 0f, .5f)};

            if (index < 0)
            {
                return colors[Random.Range(0, colors.Length)];
            }
            return colors[index % colors.Length];
        }

        public static Color SetRainbowColor(this Color color, int index = -1)
        {
            Color[] colors = {new Color(1f, 0f, 0f)
                        , new Color(1f, .5f, 0f)
                        , new Color(1f, 1f, 0f)
                        , new Color(.5f, 1f, 0f)
                        , new Color(0f, 1f, 0f)
                        , new Color(0f, 1f, .5f)
                        , new Color(0f, 1f, 1f)
                        , new Color(0f, .5f, 1f)
                        , new Color(0f, 0f, 1f)
                        , new Color(.5f, 0f, 1f)
                        , new Color(1f, 0f, 1f)
                        , new Color(1f, 0f, .5f)};

            if (index < 0)
            {
                return colors[Random.Range(0, colors.Length)];
            }
            return colors[index % colors.Length];
        }
    }

}
