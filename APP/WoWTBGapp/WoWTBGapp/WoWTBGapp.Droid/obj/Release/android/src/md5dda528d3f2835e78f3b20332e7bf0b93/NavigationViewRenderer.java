package md5dda528d3f2835e78f3b20332e7bf0b93;


public class NavigationViewRenderer
	extends md5b60ffeb829f638581ab2bb9b1a7f4f3f.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewRemoved:(Landroid/view/View;)V:GetOnViewRemoved_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("WoWTBGapp.Droid.NavigationViewRenderer, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NavigationViewRenderer.class, __md_methods);
	}


	public NavigationViewRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == NavigationViewRenderer.class)
			mono.android.TypeManager.Activate ("WoWTBGapp.Droid.NavigationViewRenderer, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public NavigationViewRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == NavigationViewRenderer.class)
			mono.android.TypeManager.Activate ("WoWTBGapp.Droid.NavigationViewRenderer, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public NavigationViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == NavigationViewRenderer.class)
			mono.android.TypeManager.Activate ("WoWTBGapp.Droid.NavigationViewRenderer, WoWTBGapp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onViewRemoved (android.view.View p0)
	{
		n_onViewRemoved (p0);
	}

	private native void n_onViewRemoved (android.view.View p0);

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
