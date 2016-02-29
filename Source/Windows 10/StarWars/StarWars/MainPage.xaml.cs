using Porrey.Uwp.IoT.Devices.Photon;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StarWars
{
	public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			string deviceId = "12345";
			string token = "12345";
			ICloudIdentity identity = new CloudIdentityV1(token, deviceId);

			//Variable<int> variable = new Variable<int>(identity);
			//int result1 = await variable.Get("volume");

			Function function = new Function(identity);
			int result2 = await function.Go("controller", "lightSaberOn");

			//int result3 = await variable.Get("volume");

			//Function function = new Function(identity);
			//int result2 = await function.Go("parameter", "minimumDistance");
		}
	}
}
