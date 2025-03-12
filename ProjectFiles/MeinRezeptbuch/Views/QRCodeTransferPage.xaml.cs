using Microsoft.Maui.Controls;
using ZXing.Net.Maui;
using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views
{
    public partial class QRCodeTransferPage : ContentPage
    {
        private readonly QRCodeTransferViewModel _viewModel;

        public QRCodeTransferPage(QRCodeTransferViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            BindingContext = _viewModel;
        }

        private async void OnQRCodeScanned(object sender, BarcodeDetectionEventArgs e)
        {
            string scannedData = e.Results[0].Value;
            await _viewModel.ImportQRCode(scannedData);
        }
    }
}
