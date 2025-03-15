# 🍽️ MeinRezeptbuch – Your Digital Recipe Book with MAUI

<!--  ![MeinRezeptbuch Banner](https://your-image-url.com/banner.png) -->

## 📌 Overview
MeinRezeptbuch is a .NET MAUI application for managing recipes.
The app allows you to create, edit, and organize recipes with an intuitive user interface.

**✨ Highlights:**
- 🏷 **Recipe & Ingredient Management (SQLite)**
- 🔄 **Live Data Binding with MVVM**
- 📱 **Cross-Platform: Android, iOS & Windows**
- 📷 **QR Code Import & Export for Recipe Sharing**
- 📌 **Offline Usage with Local SQLite Cache**

---

<!--
## 🖥️ **Screenshots & Preview**
| Home | Recipe Detail View | QR Code Scan |
|------------|----------------------|-------------|
| ![Home](https://your-image-url.com/home.png) | ![Detail](https://your-image-url.com/detail.png) | ![QR](https://your-image-url.com/qr.png) |
-->

---

## 🔧 **Technologies & Architecture**
✅ **Frontend:** .NET MAUI, XAML, MVVM  
✅ **Database:** SQLite  
✅ **Services:** Dependency Injection (DI), WeakReferenceMessenger  
✅ **Barcode & QR Code:** ZXing.Net.Maui  

---

## 📦 **Setup & Installation**
**1️⃣ Prerequisites:**  
- [.NET 9 SDK](https://dotnet.microsoft.com/download)  
- Visual Studio with MAUI support  
- Android/iOS Emulator or Physical Device  

**2️⃣ Clone & Start the Project**  
```sh
git clone https://github.com/SoPrecht/MeinRezeptbuch.git
cd MeinRezeptbuch
dotnet build
dotnet run
```

---

## 🚀 **Build & Deployment**
### **📱 Generate Android APK:**
```sh
dotnet publish -f net9.0-android -c Release
```
### **🍏 iOS TestFlight Deployment:**
```sh
dotnet publish -f net9.0-ios -c Release
```
### **💻 Windows MSIX Package:**
```sh
dotnet publish -f net9.0-windows -c Release
```

---

## 📝 **Features & Functions**
✔️ **Add, Edit & Delete Recipes**  
✔️ **Manage Ingredients with Quantity & Units**  
✔️ **Cross-Platform Support**  
✔️ **QR Code for Importing & Exporting Recipes**  
✔️ **Automatic UI Updates with ObservableCollection**  
✔️ **SQLite Database for Offline Storage**  

---

## 📩 **Contact & Application Note**
This project was created by **Sören Precht** as a showcase for job applications.
📧 **Email:** [soeren.precht@gmail.com](mailto:soeren.precht@gmail.com)  
📂 **GitHub:** [github.com/SoPrecht](https://github.com/SoPrecht)  

---

### **⭐ Star this repository if you find it useful! ⭐**
