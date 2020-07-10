using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiPlatformTest
{
    public class HybridWebView : WebView
    {
        Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            propertyName: "Uri",
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public void RegisterAction(string key, Action<string> callback)
        {
            actions.Add(key, callback);
        }

        public void Cleanup()
        {
            actions.Clear();
        }

        public void InvokeAction(string key, string data)
        {
            if (!actions.ContainsKey(key))
            {
                return;
            }
            if (data == null)
            {
                data = "";
            }
            actions[key].Invoke(data);
        }
    }
}
