package md5dda528d3f2835e78f3b20332e7bf0b93;


public class DataRefreshServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("WoWTBGapp.Droid.DataRefreshServiceBinder, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DataRefreshServiceBinder.class, __md_methods);
	}


	public DataRefreshServiceBinder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DataRefreshServiceBinder.class)
			mono.android.TypeManager.Activate ("WoWTBGapp.Droid.DataRefreshServiceBinder, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public DataRefreshServiceBinder (com.sergiong.wowtbagapp.DataRefreshService p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == DataRefreshServiceBinder.class)
			mono.android.TypeManager.Activate ("WoWTBGapp.Droid.DataRefreshServiceBinder, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "WoWTBGapp.Droid.DataRefreshService, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
