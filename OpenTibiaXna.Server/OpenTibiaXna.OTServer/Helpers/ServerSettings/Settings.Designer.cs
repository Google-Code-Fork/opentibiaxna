﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenTibiaXna.OTServer.Helpers.ServerSettings {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Settings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Settings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OpenTibiaXna.OTServer.Helpers.ServerSettings.Settings", typeof(Settings).Assembly);
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
        ///   Looks up a localized string similar to GameServerPort.
        /// </summary>
        internal static string GameServerPort {
            get {
                return ResourceManager.GetString("GameServerPort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ItemDataFile.
        /// </summary>
        internal static string ItemDataFile {
            get {
                return ResourceManager.GetString("ItemDataFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ItemsXmlFile.
        /// </summary>
        internal static string ItemsXmlFile {
            get {
                return ResourceManager.GetString("ItemsXmlFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to LoginServerPort.
        /// </summary>
        internal static string LoginServerPort {
            get {
                return ResourceManager.GetString("LoginServerPort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MessageOfTheDay.
        /// </summary>
        internal static string MessageOfTheDay {
            get {
                return ResourceManager.GetString("MessageOfTheDay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ScriptsDirectory.
        /// </summary>
        internal static string ScriptsDirectory {
            get {
                return ResourceManager.GetString("ScriptsDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WorldName.
        /// </summary>
        internal static string WorldName {
            get {
                return ResourceManager.GetString("WorldName", resourceCulture);
            }
        }
    }
}
