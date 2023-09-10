package crc64549e63b0e1a9b7a8;


public class CustomerAdapter_TabViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CustomerDemo.Utils.CustomerAdapter+TabViewHolder, CustomerDemo", CustomerAdapter_TabViewHolder.class, __md_methods);
	}


	public CustomerAdapter_TabViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == CustomerAdapter_TabViewHolder.class) {
			mono.android.TypeManager.Activate ("CustomerDemo.Utils.CustomerAdapter+TabViewHolder, CustomerDemo", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
		}
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
