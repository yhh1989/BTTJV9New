using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Sw.Hospital.HealthExaminationSystem.Common.Bases
{
    [ProvideProperty("Enabled", typeof(Component))]
    [ProvideProperty("PermissionId", typeof(Component))]
    [ProvideProperty("IsVisable", typeof(Component))]
    public partial class PermissionManager : Component, IExtenderProvider
    {
        public PermissionManager()
        {
            HashtableProperties = new Hashtable();
            InitializeComponent();
        }

        public PermissionManager(IContainer container)
        {
            HashtableProperties = new Hashtable();
            container.Add(this);

            InitializeComponent();
        }

        public bool Enabled { get; set; }

        public Hashtable HashtableProperties { get; }


        public bool CanExtend(object extendee)
        {
            return Enabled && extendee is Component;
        }

        public bool GetEnabled(Component ctrl)
        {
            if (HashtableProperties.Contains(ctrl))
                return ((ProvidedProperties)HashtableProperties[ctrl]).Enabled;
            return false;
        }

        public void SetEnabled(Component ctrl, bool value)
        {
            GetAddControl(ctrl).Enabled = value;
        }

        public string GetPermissionId(Component ctrl)
        {
            if (HashtableProperties.Contains(ctrl))
                return ((ProvidedProperties)HashtableProperties[ctrl]).Id;
            return string.Empty;
        }

        public void SetPermissionId(Component ctrl, string value)
        {
            GetAddControl(ctrl).Id = value;
        }

        public bool GetIsVisable(Component ctrl)
        {
            if (HashtableProperties.Contains(ctrl))
                return ((ProvidedProperties)HashtableProperties[ctrl]).IsVisable;
            return false;
        }

        public void SetIsVisable(Component ctrl, bool value)
        {
            GetAddControl(ctrl).IsVisable = value;
        }

        private ProvidedProperties GetAddControl(Component ctrl)
        {
            if (HashtableProperties.Contains(ctrl))
            {
                return (ProvidedProperties)HashtableProperties[ctrl];
            }

            var providedProperties = new ProvidedProperties { Enabled = false, IsVisable = true };
            HashtableProperties.Add(ctrl, providedProperties);
            return providedProperties;
        }
    }
}