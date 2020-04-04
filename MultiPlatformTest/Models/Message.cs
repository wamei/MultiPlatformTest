using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlatformTest.Models
{
    class Message : RealmObject
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Body { get; set; }
    }
}
