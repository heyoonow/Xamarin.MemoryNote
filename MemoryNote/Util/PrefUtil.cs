using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MemoryNote.Util
{
    public class PrefUtil
    {
        public PrefUtil()
        {

        }


        public void Set(string key, string value)
        {
            Preferences.Set(key, value);
        }
        public void Set(string key, int value)
        {
            Preferences.Set(key, value);
        }
        public void Set(string key, bool value)
        {
            Preferences.Set(key, value);
        }
        public void Set(string key, DateTime value)
        {
            Preferences.Set(key, value);
        }
        public string Get(string key)
        {
            return Preferences.Get(key, "");
        }
        public int Get(string key, int defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }
        public bool Remove(string key)
        {
            if (Preferences.ContainsKey(key) == false)
                return false;

            Preferences.Remove(key);
            return true;
        }
        public bool ContainsKey(string key)
        {
            return Preferences.ContainsKey(key);
        }
        public void Clear()
        {
            Preferences.Clear();
        }
    }
}
