﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.AMIS.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.AMIS.Core.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã nhân viên đã tồn tại trên hệ thống! Vui lòng kiểm tra lại..
        /// </summary>
        internal static string Msg_Code_Exist {
            get {
                return ResourceManager.GetString("Msg_Code_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email đã được sử dụng! Vui lòng điền Email khác..
        /// </summary>
        internal static string Msg_Email_Exist {
            get {
                return ResourceManager.GetString("Msg_Email_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số chứng minh thư đã tồn tại trên hệ thống! Vui lòng kiểm tra lại..
        /// </summary>
        internal static string Msg_IdentifyNumber_Exist {
            get {
                return ResourceManager.GetString("Msg_IdentifyNumber_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tham số truyền vào không hợp lệ!.
        /// </summary>
        internal static string Msg_Param_Error {
            get {
                return ResourceManager.GetString("Msg_Param_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số điện thoại đã tồn tại trên hệ thống!.
        /// </summary>
        internal static string Msg_Phone_Exist {
            get {
                return ResourceManager.GetString("Msg_Phone_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(([^&lt;&gt;()\\.,;:\s@&quot;]+(\.[^&lt;&gt;()\\.,;:\s@&quot;]+)*)|(&quot;.+&quot;))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,24}))$.
        /// </summary>
        internal static string Regex_String {
            get {
                return ResourceManager.GetString("Regex_String", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /^(([^&lt;&gt;()[\]\\.,;:\s@&quot;]+(\.[^&lt;&gt;()[\]\\.,;:\s@&quot;]+)*)|(&quot;.+&quot;))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;.
        /// </summary>
        internal static string Regex_StringV2 {
            get {
                return ResourceManager.GetString("Regex_StringV2", resourceCulture);
            }
        }
    }
}
