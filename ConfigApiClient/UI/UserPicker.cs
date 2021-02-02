using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ConfigAPIClient.UI
{
    /// <summary>
    /// This class and its associated classes are not required to work  with the Configuration API.
    /// But included to be able to add AD-users and groups.
    /// 
    /// Not all normal options are made available!
    /// </summary>

    #region Native method and structures

    internal static class NativeMethods
    {
        /// <summary>
        /// The object picker dialog box.
        /// </summary>
        [ComImport, Guid("17D6CCD8-3B7B-11D2-B9E0-00C04FD8DBF7")]
        public class DSObjectPicker
        {
        }

        /// <summary>
        /// The IDsObjectPicker interface is used by an application to initialize and display an object picker dialog box. 
        /// </summary>
        [ComImport, Guid("0C87E64E-3B7A-11D2-B9E0-00C04FD8DBF7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDsObjectPicker
        {
            int Initialize(ref DSOP_INIT_INFO pInitInfo);
            int InvokeDialog(IntPtr HWND, ref IntPtr lpDataObject);
        }

        /// <summary>
        /// The DSOP_INIT_INFO structure contains data required to initialize an object picker dialog box. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DSOP_INIT_INFO
        {
            public uint cbSize;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwzTargetComputer;

            public uint cDsScopeInfos;
            public IntPtr aDsScopeInfos;
            public uint flOptions;
            public uint cAttributesToFetch;
            public IntPtr apwzAttributeNames;
        }

        /// <summary>
        /// The DSOP_UPLEVEL_FILTER_FLAGS structure contains flags that indicate the filters to use for an up-level scope. An up-level scope is a scope that supports the ADSI LDAP provider.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DSOP_UPLEVEL_FILTER_FLAGS
        {
            public uint flBothModes;
            public uint flMixedModeOnly;
            public uint flNativeModeOnly;
        }

        /// <summary>
        /// The DSOP_FILTER_FLAGS structure contains flags that indicate the types of objects presented to the user for a specified scope or scopes.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DSOP_FILTER_FLAGS
        {
            public DSOP_UPLEVEL_FILTER_FLAGS Uplevel;
            public uint flDownlevel;
        }

        /// <summary>
        /// The DSOP_SCOPE_INIT_INFO structure describes one or more scope types that have the same attributes. A scope type is a type of location, for example a domain, computer, or Global Catalog, from which the user can select objects.
        /// A scope type is a type of location, for example a domain, computer, or Global Catalog, from which the user can select objects. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto), Serializable]
        public struct DSOP_SCOPE_INIT_INFO
        {
            public uint cbSize;
            public uint flType;
            public uint flScope;
            [MarshalAs(UnmanagedType.Struct)]
            public DSOP_FILTER_FLAGS FilterFlags;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwzDcName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwzADsPath;
            public uint hr;
        }

        /// <summary>
        /// The IDsObjectPicker.InvokeDialog result
        /// </summary>
        public static class HRESULT
        {
            public const int S_OK = 0; // The method succeeded. 
            public const int S_FALSE = 1; // The user cancelled the dialog box. ppdoSelections receives NULL. 
            public const int E_NOTIMPL = unchecked((int)0x80004001); 
        }

        public static class DSOP_DOWNLEVEL_FLAGS
        {
            public const uint DSOP_DOWNLEVEL_FILTER_USERS = 0x80000001;
            public const uint DSOP_DOWNLEVEL_FILTER_LOCAL_GROUPS = 0x80000002;
            public const uint DSOP_DOWNLEVEL_FILTER_GLOBAL_GROUPS = 0x80000004;
            public const uint DSOP_DOWNLEVEL_FILTER_COMPUTERS = 0x80000008;
            public const uint DSOP_DOWNLEVEL_FILTER_WORLD = 0x80000010;
            public const uint DSOP_DOWNLEVEL_FILTER_AUTHENTICATED_USER = 0x80000020;
            public const uint DSOP_DOWNLEVEL_FILTER_ANONYMOUS = 0x80000040;
            public const uint DSOP_DOWNLEVEL_FILTER_BATCH = 0x80000080;
            public const uint DSOP_DOWNLEVEL_FILTER_CREATOR_OWNER = 0x80000100;
            public const uint DSOP_DOWNLEVEL_FILTER_CREATOR_GROUP = 0x80000200;
            public const uint DSOP_DOWNLEVEL_FILTER_DIALUP = 0x80000400;
            public const uint DSOP_DOWNLEVEL_FILTER_INTERACTIVE = 0x80000800;
            public const uint DSOP_DOWNLEVEL_FILTER_NETWORK = 0x80001000;
            public const uint DSOP_DOWNLEVEL_FILTER_SERVICE = 0x80002000;
            public const uint DSOP_DOWNLEVEL_FILTER_SYSTEM = 0x80004000;
            public const uint DSOP_DOWNLEVEL_FILTER_EXCLUDE_BUILTIN_GROUPS = 0x80008000;
            public const uint DSOP_DOWNLEVEL_FILTER_TERMINAL_SERVER = 0x80010000;
            public const uint DSOP_DOWNLEVEL_FILTER_ALL_WELLKNOWN_SIDS = 0x80020000;
            public const uint DSOP_DOWNLEVEL_FILTER_LOCAL_SERVICE = 0x80040000;
            public const uint DSOP_DOWNLEVEL_FILTER_NETWORK_SERVICE = 0x80080000;
            public const uint DSOP_DOWNLEVEL_FILTER_REMOTE_LOGON = 0x80100000;
        }

        public static class DSOP_FILTER_FLAGS_FLAGS
        {
            public const uint DSOP_FILTER_INCLUDE_ADVANCED_VIEW = 0x00000001;
            public const uint DSOP_FILTER_USERS = 0x00000002;
            public const uint DSOP_FILTER_BUILTIN_GROUPS = 0x00000004;
            public const uint DSOP_FILTER_WELL_KNOWN_PRINCIPALS = 0x00000008;
            public const uint DSOP_FILTER_UNIVERSAL_GROUPS_DL = 0x00000010;
            public const uint DSOP_FILTER_UNIVERSAL_GROUPS_SE = 0x00000020;
            public const uint DSOP_FILTER_GLOBAL_GROUPS_DL = 0x00000040;
            public const uint DSOP_FILTER_GLOBAL_GROUPS_SE = 0x00000080;
            public const uint DSOP_FILTER_DOMAIN_LOCAL_GROUPS_DL = 0x00000100;
            public const uint DSOP_FILTER_DOMAIN_LOCAL_GROUPS_SE = 0x00000200;
            public const uint DSOP_FILTER_CONTACTS = 0x00000400;
            public const uint DSOP_FILTER_COMPUTERS = 0x00000800;
        }

        			/// <summary>
			/// Flags that indicate the scope types described by this structure. You can combine multiple scope types if all specified scopes use the same settings. 
			/// </summary>
			public static class DSOP_SCOPE_TYPE_FLAGS
			{
				public const uint DSOP_SCOPE_TYPE_TARGET_COMPUTER = 0x00000001;
				public const uint DSOP_SCOPE_TYPE_UPLEVEL_JOINED_DOMAIN = 0x00000002;
				public const uint DSOP_SCOPE_TYPE_DOWNLEVEL_JOINED_DOMAIN = 0x00000004;
				public const uint DSOP_SCOPE_TYPE_ENTERPRISE_DOMAIN = 0x00000008;
				public const uint DSOP_SCOPE_TYPE_GLOBAL_CATALOG = 0x00000010;
				public const uint DSOP_SCOPE_TYPE_EXTERNAL_UPLEVEL_DOMAIN = 0x00000020;
				public const uint DSOP_SCOPE_TYPE_EXTERNAL_DOWNLEVEL_DOMAIN = 0x00000040;
				public const uint DSOP_SCOPE_TYPE_WORKGROUP = 0x00000080;
				public const uint DSOP_SCOPE_TYPE_USER_ENTERED_UPLEVEL_SCOPE = 0x00000100;
				public const uint DSOP_SCOPE_TYPE_USER_ENTERED_DOWNLEVEL_SCOPE = 0x00000200;
			}

            /// <summary>
            /// The DS_SELECTION structure contains data about an object the user selected from an object picker dialog box. 
            /// The DS_SELECTION_LIST structure contains an array of DS_SELECTION structures.
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct DS_SELECTION
            {
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzName;

                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzADsPath;

                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzClass;

                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzUPN;

                public IntPtr pvarFetchedAttributes;
                public uint flScopeType;
            }

            /// <summary>
            /// The DS_SELECTION_LIST structure contains data about the objects the user selected from an object picker dialog box.
            /// This structure is supplied by the IDataObject interface supplied by the IDsObjectPicker::InvokeDialog method in the CFSTR_DSOP_DS_SELECTION_LIST data format.
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct DS_SELECTION_LIST
            {
                public uint cItems;
                public uint cFetchedAttributes;
            }

            public class DS_SELECTION_ATTRIBUTES
            {
                public object[] attributeValues;

                public DS_SELECTION_ATTRIBUTES()
                {
                }
            }
            /// <summary>
            /// The STGMEDIUM structure is a generalized global memory handle used for data transfer operations by the IDataObject
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct STGMEDIUM
            {
                public uint tymed;
                public IntPtr hGlobal;
                public IntPtr pUnkForRelease;
            }
            /// <summary>
            /// The TYMED enumeration values indicate the type of storage medium being used in a data transfer. 
            /// </summary>
            public enum TYMED
            {
                TYMED_HGLOBAL = 1,
                TYMED_FILE = 2,
                TYMED_ISTREAM = 4,
                TYMED_ISTORAGE = 8,
                TYMED_GDI = 16,
                TYMED_MFPICT = 32,
                TYMED_ENHMF = 64,
                TYMED_NULL = 0
            }
            /// <summary>
            /// The GlobalLock function locks a global memory object and returns a pointer to the first byte of the object's memory block.
            /// GlobalLock function increments the lock count by one.
            /// Needed for the clipboard functions when getting the data from IDataObject
            /// </summary>
            /// <param name="hMem"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll")]
            public static extern IntPtr GlobalLock(IntPtr hMem);

            /// <summary>
            /// The GlobalUnlock function decrements the lock count associated with a memory object.
            /// </summary>
            /// <param name="hMem"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll")]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            public static extern bool GlobalUnlock(IntPtr hMem);

            /// <summary>
            /// This structure is used as a parameter in OLE functions and methods that require data format information.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct FORMATETC
            {
                public uint cfFormat;
                public IntPtr ptd;
                public uint dwAspect;
                public int lindex;
                public uint tymed;
            }

            /// <summary>
            /// The CFSTR_DSOP_DS_SELECTION_LIST clipboard format is provided by the IDataObject obtained by calling IDsObjectPicker.InvokeDialog
            /// </summary>
            public static class CLIPBOARD_FORMAT
            {
                public const string CFSTR_DSOP_DS_SELECTION_LIST = "CFSTR_DSOP_DS_SELECTION_LIST";
            }
            /// <summary>
            /// Interface to enable data transfers
            /// </summary>
            [ComImport, Guid("0000010e-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IDataObject
            {
                [PreserveSig()]
                int GetData(ref FORMATETC pFormatEtc, ref STGMEDIUM b);

                [PreserveSig()]
                void GetDataHere(ref FORMATETC pFormatEtc, ref STGMEDIUM b);

                [PreserveSig()]
                int QueryGetData(IntPtr a);

                [PreserveSig()]
                int GetCanonicalFormatEtc(IntPtr a, IntPtr b);

                [PreserveSig()]
                int SetData(IntPtr a, IntPtr b, int c);

                [PreserveSig()]
                int EnumFormatEtc(uint a, IntPtr b);

                [PreserveSig()]
                int DAdvise(IntPtr a, uint b, IntPtr c, ref uint d);

                [PreserveSig()]
                int DUnadvise(uint a);

                [PreserveSig()]
                int EnumDAdvise(IntPtr a);
            }
            public static class DSOP_SCOPE_INIT_INFO_FLAGS
            {
                public const uint DSOP_SCOPE_FLAG_STARTING_SCOPE = 0x00000001;
                public const uint DSOP_SCOPE_FLAG_WANT_PROVIDER_WINNT = 0x00000002;
                public const uint DSOP_SCOPE_FLAG_WANT_PROVIDER_LDAP = 0x00000004;
                public const uint DSOP_SCOPE_FLAG_WANT_PROVIDER_GC = 0x00000008;
                public const uint DSOP_SCOPE_FLAG_WANT_SID_PATH = 0x00000010;
                public const uint DSOP_SCOPE_FLAG_WANT_DOWNLEVEL_BUILTIN_PATH = 0x00000020;
                public const uint DSOP_SCOPE_FLAG_DEFAULT_FILTER_USERS = 0x00000040;
                public const uint DSOP_SCOPE_FLAG_DEFAULT_FILTER_GROUPS = 0x00000080;
                public const uint DSOP_SCOPE_FLAG_DEFAULT_FILTER_COMPUTERS = 0x00000100;
                public const uint DSOP_SCOPE_FLAG_DEFAULT_FILTER_CONTACTS = 0x00000200;
            }

    }

    #endregion


    public partial class UserPicker : CommonDialog
	{        
		private DirectoryObject[] _selectedObjects;
        public DirectoryObject[] SelectedObjects
        {
            get
            {
                if (_selectedObjects == null)
                {
                    return new DirectoryObject[0];
                }

                return (DirectoryObject[])_selectedObjects.Clone();
            }
        }

		private string[] attributeNames;
		private uint length = 0;

        public UserPicker()
		{
			Reset();

			attributeNames = new string[] { "objectSid" };
			length = (uint)attributeNames.Length;
		}
        
		public override void Reset()
		{
		}
        
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			IntPtr dataObj = IntPtr.Zero;

			NativeMethods.IDsObjectPicker ipicker = Initialize();
			if (ipicker == null)
			{
				_selectedObjects = null;
				return false;
			}

			int hResult = ipicker.InvokeDialog(hwndOwner, ref dataObj);
			if (dataObj != IntPtr.Zero)
			{
				_selectedObjects = ProcessSelections(dataObj);
			}

			return (hResult == NativeMethods.HRESULT.S_OK);
		}

		private NativeMethods.IDsObjectPicker Initialize()
		{
			NativeMethods.DSObjectPicker picker = new NativeMethods.DSObjectPicker();

			NativeMethods.IDsObjectPicker ipicker = (NativeMethods.IDsObjectPicker)picker;

			NativeMethods.DSOP_SCOPE_INIT_INFO[] scopeInitInfo = new NativeMethods.DSOP_SCOPE_INIT_INFO[2];

			// The default and filters are used by all scopes
			uint defaultFilter = GetDefaultFilter();
			uint upLevelFilter = GetUpLevelFilter();
			uint downLevelFilter = GetDownLevelFilter();

			// Use one scope for the default (starting) locations.
			uint startingScope = GetStartingScope();

            
			if (startingScope > 0)
			{
				scopeInitInfo[0].cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.DSOP_SCOPE_INIT_INFO));
				scopeInitInfo[0].flType = startingScope;
				scopeInitInfo[0].flScope = NativeMethods.DSOP_SCOPE_INIT_INFO_FLAGS.DSOP_SCOPE_FLAG_STARTING_SCOPE | defaultFilter;

				scopeInitInfo[0].FilterFlags.Uplevel.flBothModes = upLevelFilter;
				scopeInitInfo[0].FilterFlags.flDownlevel = downLevelFilter;

				scopeInitInfo[0].pwzADsPath = null;
				scopeInitInfo[0].pwzDcName = null;

				scopeInitInfo[0].hr = 0;
			}
            
			// Allocate memory from the unmananged mem of the process, this should be freed later!!!
			IntPtr refScopeInitInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.DSOP_SCOPE_INIT_INFO)) * scopeInitInfo.Length);

			// Marshal structs to pointers
			for (int index = 0; index < scopeInitInfo.Length; index++)
			{
				Marshal.StructureToPtr(scopeInitInfo[index], (IntPtr)((long)refScopeInitInfo + index * Marshal.SizeOf(typeof(NativeMethods.DSOP_SCOPE_INIT_INFO))), false);
			}

			// Initialize structure with data to initialize an object picker dialog box. 
			NativeMethods.DSOP_INIT_INFO initInfo = new NativeMethods.DSOP_INIT_INFO();
			initInfo.cbSize = (uint)Marshal.SizeOf(initInfo);
            initInfo.pwzTargetComputer = null; 
			initInfo.cDsScopeInfos = (uint)scopeInitInfo.Length;
			initInfo.aDsScopeInfos = refScopeInitInfo;

			initInfo.flOptions = 0;
			initInfo.cAttributesToFetch = length;
			if (length > 0)
			{
				MarshalStrings strings = new MarshalStrings(attributeNames);
				initInfo.apwzAttributeNames = strings.ArrayPtr;
			}
			else
			{
				initInfo.cAttributesToFetch = 0;
				initInfo.apwzAttributeNames = IntPtr.Zero;
			}

			int hresult = ipicker.Initialize(ref initInfo);

			if (hresult != NativeMethods.HRESULT.S_OK)
			{
				return null;
			}

			return ipicker;
		}

        
		private DirectoryObject[] ProcessSelections(IntPtr dataObj)
		{
			if (dataObj == IntPtr.Zero)
			{
				throw new ArgumentNullException("dataObj");
			}

			DirectoryObject[] selections = null;

			// The STGMEDIUM structure is a generalized global memory handle used for data transfer operations
			NativeMethods.STGMEDIUM stg = new NativeMethods.STGMEDIUM();
			stg.tymed = (uint)NativeMethods.TYMED.TYMED_HGLOBAL;
			stg.hGlobal = IntPtr.Zero;
			stg.pUnkForRelease = IntPtr.Zero;

			// The FORMATETC structure is a generalized Clipboard format.
			NativeMethods.FORMATETC fe = new NativeMethods.FORMATETC();

			// The CFSTR_DSOP_DS_SELECTION_LIST clipboard format is provided by the IDataObject obtained by calling IDsObjectPicker::InvokeDialog
			fe.cfFormat = (uint)DataFormats.GetFormat(NativeMethods.CLIPBOARD_FORMAT.CFSTR_DSOP_DS_SELECTION_LIST).Id;

			fe.ptd = IntPtr.Zero;

			// DVASPECT_CONTENT = 1
			fe.dwAspect = 1;

			// all of the data
			fe.lindex = -1;

			// The storage medium is a global memory handle (HGLOBAL)
			fe.tymed = (uint)NativeMethods.TYMED.TYMED_HGLOBAL;

			NativeMethods.DS_SELECTION[] dataArray = null;

			int dsAttributesCount = 0;
			NativeMethods.DS_SELECTION_ATTRIBUTES[] dsAttributes = null;

			NativeMethods.IDataObject typedObjectForIUnknown = (NativeMethods.IDataObject)Marshal.GetTypedObjectForIUnknown(dataObj, typeof(NativeMethods.IDataObject));
			if (typedObjectForIUnknown.GetData(ref fe, ref stg) == 0)
			{
				IntPtr ptr = NativeMethods.GlobalLock(stg.hGlobal);

				try
				{
					NativeMethods.DS_SELECTION_LIST ds_selection_list = (NativeMethods.DS_SELECTION_LIST)Marshal.PtrToStructure(ptr, typeof(NativeMethods.DS_SELECTION_LIST));
					int numberOfItems = (int)ds_selection_list.cItems;
					dsAttributesCount = (int)ds_selection_list.cFetchedAttributes;

					selections = new DirectoryObject[numberOfItems];

					if (numberOfItems > 0)
					{
						dataArray = new NativeMethods.DS_SELECTION[numberOfItems];
						dsAttributes = new NativeMethods.DS_SELECTION_ATTRIBUTES[numberOfItems];

						IntPtr currentPtr = (IntPtr)(((long)ptr) + Marshal.SizeOf(ds_selection_list));

						byte[] binaryForm = null;

						for (int index = 0; index < numberOfItems; index++)
						{
							dataArray[index] = new NativeMethods.DS_SELECTION();
							dataArray[index] = (NativeMethods.DS_SELECTION)Marshal.PtrToStructure(currentPtr, typeof(NativeMethods.DS_SELECTION));
							if ((length > 0) && (dsAttributesCount > 0))
							{
								dsAttributes[index] = new NativeMethods.DS_SELECTION_ATTRIBUTES();
								dsAttributes[index].attributeValues = Marshal.GetObjectsForNativeVariants(dataArray[index].pvarFetchedAttributes, dsAttributesCount);

								binaryForm = dsAttributes[index].attributeValues[0] as byte[];
							}

							string schemaClassName = dataArray[index].pwzClass;
							string name = dataArray[index].pwzName;
							string upn = dataArray[index].pwzUPN;
							string path = dataArray[index].pwzADsPath;

							selections[index] = new DirectoryObject(schemaClassName, name, upn, path);
							selections[index].BinarySid = binaryForm;

							currentPtr = (IntPtr)(((long)currentPtr) + Marshal.SizeOf(dataArray[index]));
						}
					}
				}
				finally
				{
					NativeMethods.GlobalUnlock(stg.hGlobal);
				}
			}

			return selections;
		}

		private uint GetDefaultFilter()
		{
			uint defaultFilter = 0;

			defaultFilter |= NativeMethods.DSOP_SCOPE_INIT_INFO_FLAGS.DSOP_SCOPE_FLAG_DEFAULT_FILTER_USERS;
			defaultFilter |= NativeMethods.DSOP_SCOPE_INIT_INFO_FLAGS.DSOP_SCOPE_FLAG_DEFAULT_FILTER_GROUPS;

			return defaultFilter;
		}

		private uint GetUpLevelFilter()
		{
			uint uplevelFilter = 0;
            uplevelFilter |= NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_USERS;
            uplevelFilter |= NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_UNIVERSAL_GROUPS_DL |
                                                 NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_UNIVERSAL_GROUPS_SE |
                                                 NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_GLOBAL_GROUPS_DL |
                                                 NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_GLOBAL_GROUPS_SE |
                                                 NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_DOMAIN_LOCAL_GROUPS_DL |
                                                 NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_DOMAIN_LOCAL_GROUPS_SE;
            uplevelFilter |= NativeMethods.DSOP_FILTER_FLAGS_FLAGS.DSOP_FILTER_BUILTIN_GROUPS;
            return uplevelFilter;
		}

        
		private uint GetDownLevelFilter()
		{
			uint downlevelFilter = 0;
            downlevelFilter |= NativeMethods.DSOP_DOWNLEVEL_FLAGS.DSOP_DOWNLEVEL_FILTER_USERS;
			return downlevelFilter;
		}
        
		private uint GetStartingScope()
		{
			uint scope = 0;
			scope |= NativeMethods.DSOP_SCOPE_TYPE_FLAGS.DSOP_SCOPE_TYPE_DOWNLEVEL_JOINED_DOMAIN |
								 NativeMethods.DSOP_SCOPE_TYPE_FLAGS.DSOP_SCOPE_TYPE_UPLEVEL_JOINED_DOMAIN;
			return scope;
		}
        
		/// <summary>
		/// Shows the AD Object Picker dialog to allow selection of a user
		/// </summary>
		/// <param name="owner"></param>
		/// <returns>An array <see cref="DirectoryObject"/> representing the selected users; null if no users were selected.</returns>
		/// <exception cref="COMException">An error occurred displaying the dialog</exception>
		public static DirectoryObject[] ShowUserObjectPicker(IWin32Window owner)
		{
			using (var objectPickerDialog = new UserPicker())
			{
				try
				{
					DialogResult dialogResult = objectPickerDialog.ShowDialog(owner);

					if ((dialogResult != DialogResult.OK) || (objectPickerDialog.SelectedObjects == null) || (objectPickerDialog.SelectedObjects.Length == 0))
					{
						return null;
					}
				}
				catch (COMException)
				{
					throw;
				}

				return objectPickerDialog.SelectedObjects;
			}
		}
	}

    public class DirectoryObject
    {
        public string ClassName
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        public string UPN
        {
            get;
            internal set;
        }

        public string Path
        {
            get;
            internal set;
        }

        public byte[] BinarySid
        {
            get;
            internal set;
        }

        public string Sid
        {
            get
            {
                if (this.BinarySid != null)
                {
                    System.Security.Principal.SecurityIdentifier securityIdentifier = new System.Security.Principal.SecurityIdentifier(this.BinarySid, 0);
                    return securityIdentifier.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public DirectoryObject(string className, string name, string upn, string path)
        {
            ClassName = className;
            Name = name;
            UPN = upn;
            Path = path;
        }
    }

    internal sealed class MarshalStrings : IDisposable
    {
        private IntPtr _taskAlloc;

        private readonly int _length;
        private IntPtr[] _strings;

        private bool _disposed = false;

        public IntPtr ArrayPtr
        {
            get { return _taskAlloc; }
        }

        public MarshalStrings(string[] theArray)
        {
            int size = IntPtr.Size;
            int cb = 0;

            if (theArray != null)
            {
                _length = theArray.Length;
                _strings = new IntPtr[_length];
                cb = _length * size;

                _taskAlloc = Marshal.AllocCoTaskMem(cb);

                for (int i = _length - 1; i >= 0; i--)
                {
                    _strings[i] = Marshal.StringToCoTaskMemUni(theArray[i]);
                    Marshal.WriteIntPtr(_taskAlloc, i * size, _strings[i]);
                }
            }
        }

        ~MarshalStrings()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_taskAlloc != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(_taskAlloc);
                    int length = _length;
                    while (length-- != 0)
                    {
                        Marshal.FreeCoTaskMem(_strings[length]);
                    }
                }

                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

    }
}
