using Android.App;
using Android.Widget;
using Android.OS;

using SeatGeek.PlacesAutocomplete;
using SeatGeek.PlacesAutocomplete.Adapter;
using SeatGeek.PlacesAutocomplete.History;
using SeatGeek.PlacesAutocomplete.Model;

using Android.Content;
using Android.Views;
using Java.Lang;
using Android.Runtime;

namespace Naxam.PlacesAuto.DroidQs
{
	[Activity(Label = "PlacesAuto", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IOnPlaceSelectedListener, IDetailsCallback
	{
		PlacesAutocompleteTextView mAutocomplete;
		TextView mStreet;
		TextView mCity;
		TextView mState;
		TextView mZip;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_places_autocomplete);

			// Get our button from the layout resource,
			// and attach an event to it
			mAutocomplete = FindViewById<PlacesAutocompleteTextView>(Resource.Id.autocomplete);
			mStreet = FindViewById<TextView>(Resource.Id.street);
			mCity = FindViewById<TextView>(Resource.Id.city);
			mState = FindViewById<TextView>(Resource.Id.state);
			mZip = FindViewById<TextView>(Resource.Id.zip);

			mAutocomplete.SetOnPlaceSelectedListener(this);
			mAutocomplete.SetOnPlaceSelectedListener(this);
		}

		void IOnPlaceSelectedListener.OnPlaceSelected(Place place)
		{
			mAutocomplete.GetDetailsFor(place, this);
		}

		void IDetailsCallback.OnSuccess(PlaceDetails details)
		{
			System.Diagnostics.Debug.WriteLine("DEMO: {0} {1}", "SUCCESS", details.Name);
			mStreet.Text = details.Name;

			foreach (AddressComponent component in details.AddressComponents)
			{
				foreach (AddressComponentType type in component.Types)
				{
					if (type == AddressComponentType.Locality)
					{
						mCity.Text = (component.LongName);
					}
					else if (type == AddressComponentType.AdministrativeAreaLevel1)
					{
						mState.Text = (component.ShortName);
					}
					else if (type == AddressComponentType.PostalCode)
					{
						mZip.Text = (component.LongName);
					}

					//switch (type)
					//{
					//	case AddressComponentType.StreetNumber:
					//		break;
					//	case AddressComponentType.Route:
					//		break;
					//	case AddressComponentType.Neighborhood:
					//		break;
					//	case AddressComponentType.SublocalityLevel1:
					//		break;
					//	case AddressComponentType.Sublocality:
					//		break;
					//	case AddressComponentType.Locality:
					//		mCity.Text = (component.LongName);
					//		break;
					//	case AddressComponentType.AdministrativeAreaLevel1:
					//		mState.Text = (component.ShortName);
					//		break;
					//	case AddressComponentType.AdministrativeAreaLevel2:
					//		break;
					//	case AddressComponentType.Country:
					//		break;
					//	case AddressComponentType.PostalCode:
					//		mZip.Text = (component.LongName);
					//		break;
					//	case AddressComponentType.Political:
					//		break;
					//}
				}
			}
		}

		void IDetailsCallback.OnFailure(Throwable failure)
		{
			System.Diagnostics.Debug.WriteLine("DEMO: {0} {1}", "FAILURE", failure.Message);
		}
	}

	[Register("naxam/placesauto/droidqs/TestPlacesAutocompleteAdapter")]
	public class TestPlacesAutocompleteAdapter : AbstractPlacesAutocompleteAdapter
	{
		public TestPlacesAutocompleteAdapter(
			Context context,
			PlacesApi api,
			AutocompleteResultType resultType,
			IAutocompleteHistoryManager history)
			: base(context, api, resultType, history)
		{

		}

		protected override View NewView(ViewGroup parent)
		{
			return LayoutInflater.From(parent.Context)
								 .Inflate(Resource.Layout.places_autocomplete_item, parent, false);
		}

		protected override void BindView(View view, Place item)
		{
			((TextView)view).Text = (item.Description);
		}
	}
}

