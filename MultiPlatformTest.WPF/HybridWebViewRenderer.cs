using MultiPlatformTest;
using MultiPlatformTest.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace MultiPlatformTest.WPF
{
    public class HybridWebViewRenderer : WebViewRenderer
    {
        const string JavaScriptFunction = "function invokeAction(key,data){window.external.Notify(key,data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.Navigated -= OnWebViewNavigationCompleted;
                Control.ObjectForScripting = null;
            }
            if (e.NewElement != null)
            {
                Control.Navigated += OnWebViewNavigationCompleted;
                Control.ObjectForScripting = new ScriptInterface(this);
            }
        }

        private void OnWebViewNavigationCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Control.InvokeScript("eval", new[] { JavaScriptFunction });
        }
    }

    [ComVisible(true)]
    public class ScriptInterface
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public ScriptInterface(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        public void Notify(string key, string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((HybridWebView)hybridRenderer.Element).InvokeAction(key, data);
            }
        }
    }
}
