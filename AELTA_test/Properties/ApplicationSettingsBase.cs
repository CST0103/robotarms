#region 組件 System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.dll
#endregion

using System.ComponentModel;
using System.Reflection;

namespace System.Configuration
{
    [DefaultMember("Item")]
    public abstract class ApplicationSettingsBase : SettingsBase, INotifyPropertyChanged
    {
        protected ApplicationSettingsBase();
        protected ApplicationSettingsBase(IComponent owner);
        protected ApplicationSettingsBase(string settingsKey);
        protected ApplicationSettingsBase(IComponent owner, string settingsKey);

        public override object this[string propertyName] { get; set; }

        [Browsable(false)]
        public string SettingsKey { get; set; }
        [Browsable(false)]
        public override SettingsProviderCollection Providers { get; }
        [Browsable(false)]
        public override SettingsPropertyValueCollection PropertyValues { get; }
        [Browsable(false)]
        public override SettingsPropertyCollection Properties { get; }
        [Browsable(false)]
        public override SettingsContext Context { get; }

        public event SettingChangingEventHandler SettingChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event SettingsSavingEventHandler SettingsSaving;
        public event SettingsLoadedEventHandler SettingsLoaded;

        public object GetPreviousVersion(string propertyName);
        public void Reload();
        public void Reset();
        public override void Save();
        public virtual void Upgrade();
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
        protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e);
        protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e);
        protected virtual void OnSettingsSaving(object sender, CancelEventArgs e);
    }
}