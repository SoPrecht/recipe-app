using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZXing.Net.Maui.Controls;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MeinRezeptbuch.Services;
using ZXing.Rendering;
using ZXing.Common;
using ZXing;
using SkiaSharp;
using ZXing.QrCode.Internal;


namespace MeinRezeptbuch.ViewModels
{

    public partial class QRCodeTransferViewModel : ObservableObject
    {
        private readonly QRCodeTransferService _qrCodeService;

        [ObservableProperty]
        private string? qrCodeFilePath;

        [ObservableProperty]
        private ImageSource qrCodeImage;

        public QRCodeTransferViewModel(QRCodeTransferService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [RelayCommand]
        public async Task GenerateQRCode()
        {
            var jsonData = await _qrCodeService.GenerateQRCodeAsync();
            if (!string.IsNullOrEmpty(jsonData))
            {
                QrCodeImage = GenerateQRCodeImage(jsonData);
            }
        }
        private string GenerateQRCodeImage(string text)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "qrcode.png");

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 500,
                    Height = 500,
                    Margin = 0
                }
            };

            var pixelData = writer.Write(text);

            // Convert byte[] to SKColor[]
            SKColor[] skColors = new SKColor[pixelData.Pixels.Length / 4];
            for (int i = 0; i < skColors.Length; i++)
            {
                byte r = pixelData.Pixels[i * 4];
                byte g = pixelData.Pixels[i * 4 + 1];
                byte b = pixelData.Pixels[i * 4 + 2];
                byte a = pixelData.Pixels[i * 4 + 3];

                skColors[i] = new SKColor(r, g, b, a);
            }

            // Create SkiaSharp bitmap
            using var bitmap = new SKBitmap(pixelData.Width, pixelData.Height);
            bitmap.Pixels = skColors;

            // Save QR Code as PNG
            using var image = SKImage.FromBitmap(bitmap);
            using var imageData = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(filePath);
            imageData.SaveTo(stream);

            return filePath; // Return saved file path
        }


        [RelayCommand]
        public async Task ImportQRCode(string scannedData)
        {
            bool success = await _qrCodeService.ImportQRCodeDataAsync(scannedData);
            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Recipes imported successfully!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to import recipes.", "OK");
            }
        }
    }
}
