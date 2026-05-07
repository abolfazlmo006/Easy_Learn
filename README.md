# Easy_Learn | سامانه‌ی نمونه آموزش آنلاین

<div dir="rtl">

## ✨ معرفی

**Easy_Learn** یک پروژه‌ی نمونه برای نمایش مهارت‌های توسعه با **ASP.NET Core** است. این پروژه با معماری **سه‌لایه** طراحی شده و شامل یک سیستم آموزش آنلاین (LMS) با قابلیت‌های زیر می‌باشد:

- مدیریت دوره‌ها، ویدئوها، دسته‌بندی‌ها
- ثبت‌نام کاربران، سؤالات و پاسخ‌ها، علاقه‌مندی‌ها
- سبد خرید و سفارشات
- پنل مدیریت و پنل مدرس
- اعلان‌ها (Notifications) بلادرنگ
- احراز هویت و ترجمه‌ی خطاهای فارسی

## 🧱 ساختار پروژه

| پروژه | شرح |
|-------|------|
| `Easy_Learn.Data` | لایه‌ی داده (Entity Framework Core) |
| `Easy_learn.WebApi` | API (کنترلرها، DTOها، سرویس‌ها) |
| `Easy_Learn.UI` | رابط کاربری ASP.NET Core MVC |
| `PersianTranslation` | ترجمه‌ی خطاهای Identity به فارسی |

## ⚙️ تکنولوژی‌ها

- ASP.NET Core 7 (MVC + Web API)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- NSwag (تولید کلاینت)
- AutoMapper
- SignalR
- ASP.NET Core Identity
- LibMan

## 🚀 راه‌اندازی

1. مخزن را Clone کنید:
   ```bash
   git clone https://github.com/abolfazlmo006/Easy_Learn.git
   cd Easy_Learn
   ```

2. بسته‌ها را بازیابی کنید:
   ```bash
   dotnet restore
   ```

3. پایگاه داده را به‌روز کنید (در پوشه‌ی WebApi):
   ```bash
   cd Easy_learn.WebApi
   dotnet ef database update
   ```

4. API را اجرا کنید:
   ```bash
   dotnet run
   ```

5. UI را اجرا کنید:
   ```bash
   cd ../Easy_Learn.UI
   dotnet run
   ```

> **Swagger API** : `https://localhost:5001/swagger`

## 📂 ساختار کلی پوشه‌ها

```
Easy_Learn/
├── Easy_Learn.Data/         # مدل‌ها و DbContext
├── Easy_learn.WebApi/       # API
├── Easy_Learn.UI/           # رابط کاربری
├── PersianTranslation/      # ترجمه‌ی فارسی Identity
├── easylearn.nswag          # تنظیمات NSwag
└── README.md
```

## 📝 توسعه‌دهنده

**ابوالفضل محمدی**  
- [GitHub](https://github.com/abolfazlmo006)

</div>
