using System;
using System.Collections.Generic;
using System.Linq;
using Realms;

namespace MultiPlatformTest.Core.Models
{
    public class Version : RealmObject
    {
        static private List<string> _list = new List<string>()
        {
            "old",
            "new"
        };

        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }

        public Version()
        {
            var value = _list.First();
            Key = value;
            Value = value;
        }

        static public Version Default = new Version();

        static private string _currentKey = "current";
        static public Version Current
        {
            get
            {
                var realm = Realm.GetInstance();
                var version = realm.All<Version>().Where(v => v.Key == _currentKey).FirstOrDefault();
                if (version == null)
                {
                    version = new Version { Key = _currentKey };
                    realm.Write(() =>
                    {
                        realm.Add(version);
                    });
                }
                return version;
            }
            set
            {
                var realm = Realm.GetInstance();
                var version = realm.All<Version>().Where(v => v.Key == _currentKey).FirstOrDefault();
                realm.Write(() =>
                {
                    version.Value = value.Value;
                });
            }
        }
        static public List<Version> List
        {
            get
            {
                var list = new List<Version>();
                _list.ForEach(value =>
                {
                    list.Add(new Version { Key = value, Value = value });
                });
                return list;
            }
        }
    }
}
