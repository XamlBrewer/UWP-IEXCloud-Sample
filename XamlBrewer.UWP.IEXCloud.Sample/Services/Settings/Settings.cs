using Windows.Storage;
using XamlBrewer.Mvvm;

namespace XamlBrewer.UWP.IEXCloud.Sample.Services
{
    public class Settings : ObservableSettings
    {
        public static Settings Default { get; } = new Settings();

        public Settings()
            : base(ApplicationData.Current.LocalSettings)
        { }

        public string PublishableKey
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string SecretKey
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string PublishableSandBoxKey
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string SecretSandBoxKey
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        [DefaultSettingValue(Value = true)]
        public bool UseSandBox
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }
    }
}
