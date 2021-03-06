﻿#pragma checksum "D:\Users\Daniel\Source\repos\StarWars\Source\Windows 10\StarWars\StarWars\Views\CloudSetupPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8B96A3F2B59B7A6DDE426C831B17565A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarWars.Views
{
    partial class CloudSetupPage : 
        global::StarWars.Common.BindablePage, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_SplitView_IsPaneOpen(global::Windows.UI.Xaml.Controls.SplitView obj, global::System.Boolean value)
            {
                obj.IsPaneOpen = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_PasswordBox_Password(global::Windows.UI.Xaml.Controls.PasswordBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Password = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Controls_Primitives_ToggleButton_IsChecked(global::Windows.UI.Xaml.Controls.Primitives.ToggleButton obj, global::System.Nullable<global::System.Boolean> value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Nullable<global::System.Boolean>) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Nullable<global::System.Boolean>), targetNullValue);
                }
                obj.IsChecked = value;
            }
        };

        private class CloudSetupPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ICloudSetupPage_Bindings
        {
            private global::StarWars.Views.CloudSetupPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.SplitView obj2;
            private global::Windows.UI.Xaml.Controls.PasswordBox obj7;
            private global::Windows.UI.Xaml.Controls.PasswordBox obj8;
            private global::Windows.UI.Xaml.Controls.CheckBox obj9;

            private CloudSetupPage_obj1_BindingsTracking bindingsTracking;

            public CloudSetupPage_obj1_Bindings()
            {
                this.bindingsTracking = new CloudSetupPage_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2:
                        this.obj2 = (global::Windows.UI.Xaml.Controls.SplitView)target;
                        (this.obj2).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.SplitView.IsPaneOpenProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.IsPaneOpen = (this.obj2).IsPaneOpen;
                                }
                            });
                        break;
                    case 7:
                        this.obj7 = (global::Windows.UI.Xaml.Controls.PasswordBox)target;
                        (this.obj7).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.PasswordBox.PasswordProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.DeviceId = (this.obj7).Password;
                                }
                            });
                        break;
                    case 8:
                        this.obj8 = (global::Windows.UI.Xaml.Controls.PasswordBox)target;
                        (this.obj8).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.PasswordBox.PasswordProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.ApiKey = (this.obj8).Password;
                                }
                            });
                        break;
                    case 9:
                        this.obj9 = (global::Windows.UI.Xaml.Controls.CheckBox)target;
                        (this.obj9).RegisterPropertyChangedCallback(global::Windows.UI.Xaml.Controls.Primitives.ToggleButton.IsCheckedProperty,
                            (global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.ShowLifxApiKey = (this.obj9).IsChecked;
                                }
                            });
                        break;
                    default:
                        break;
                }
            }

            // ICloudSetupPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // CloudSetupPage_obj1_Bindings

            public void SetDataRoot(global::StarWars.Views.CloudSetupPage newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::StarWars.Views.CloudSetupPage obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_IsPaneOpen(obj.IsPaneOpen, phase);
                        this.Update_DeviceId(obj.DeviceId, phase);
                        this.Update_ApiKey(obj.ApiKey, phase);
                        this.Update_ShowLifxApiKey(obj.ShowLifxApiKey, phase);
                    }
                }
            }
            private void Update_IsPaneOpen(global::System.Boolean obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_SplitView_IsPaneOpen(this.obj2, obj);
                }
            }
            private void Update_DeviceId(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_PasswordBox_Password(this.obj7, obj, null);
                }
            }
            private void Update_ApiKey(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_PasswordBox_Password(this.obj8, obj, null);
                }
            }
            private void Update_ShowLifxApiKey(global::System.Nullable<global::System.Boolean> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Primitives_ToggleButton_IsChecked(this.obj9, obj, null);
                }
            }

            private class CloudSetupPage_obj1_BindingsTracking
            {
                global::System.WeakReference<CloudSetupPage_obj1_Bindings> WeakRefToBindingObj; 

                public CloudSetupPage_obj1_BindingsTracking(CloudSetupPage_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<CloudSetupPage_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void PropertyChanged_(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    CloudSetupPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        string propName = e.PropertyName;
                        global::StarWars.Views.CloudSetupPage obj = sender as global::StarWars.Views.CloudSetupPage;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                    bindings.Update_IsPaneOpen(obj.IsPaneOpen, DATA_CHANGED);
                                    bindings.Update_DeviceId(obj.DeviceId, DATA_CHANGED);
                                    bindings.Update_ApiKey(obj.ApiKey, DATA_CHANGED);
                                    bindings.Update_ShowLifxApiKey(obj.ShowLifxApiKey, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "IsPaneOpen":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_IsPaneOpen(obj.IsPaneOpen, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "DeviceId":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_DeviceId(obj.DeviceId, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ApiKey":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ApiKey(obj.ApiKey, DATA_CHANGED);
                                    }
                                    break;
                                }
                                case "ShowLifxApiKey":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_ShowLifxApiKey(obj.ShowLifxApiKey, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                public void UpdateChildListeners_(global::StarWars.Views.CloudSetupPage obj)
                {
                    CloudSetupPage_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        if (bindings.dataRoot != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)bindings.dataRoot).PropertyChanged -= PropertyChanged_;
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.SplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.HyperlinkButton element3 = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    #line 63 "..\..\..\Views\CloudSetupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)element3).Click += this.ControlPanel_Click;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.HyperlinkButton element4 = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    #line 83 "..\..\..\Views\CloudSetupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)element4).Click += this.CloudSetup_Click;
                    #line default
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.HyperlinkButton element5 = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    #line 98 "..\..\..\Views\CloudSetupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)element5).Click += this.Settings_Click;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.HyperlinkButton element6 = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                    #line 118 "..\..\..\Views\CloudSetupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.HyperlinkButton)element6).Click += this.ResetSettings_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.ShowApiKeyCheckbox = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 10:
                {
                    this.menuButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 31 "..\..\..\Views\CloudSetupPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.menuButton).Click += this.MenuButton_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::StarWars.Common.BindablePage element1 = (global::StarWars.Common.BindablePage)target;
                    CloudSetupPage_obj1_Bindings bindings = new CloudSetupPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

