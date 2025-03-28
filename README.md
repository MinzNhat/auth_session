# Session-based Authentication API

Chào mừng bạn đến với dự án **Session-based Authentication API**! Dự án này sử dụng **.NET Core** để xây dựng hệ thống xác thực dựa trên session. Dưới đây là thông tin chi tiết về cấu trúc thư mục và cách cài đặt.

---

## 📂 Cấu trúc thư mục

```
/auth_session
│── /auth_session.API                # Dự án API chính (Startup Project)
│   ├── /Controllers                 # Chứa các controller (API endpoints)
│   ├── /Middlewares                 # Middleware tùy chỉnh
│   ├── /Filters                     # Các filter như Validation, Exception Handling
│   ├── /Models                      # Định nghĩa các DTO (Data Transfer Object)
│   ├── /Services                    # Xử lý nghiệp vụ (Business Logic)
│   ├── /Configurations              # Cấu hình Dependency Injection, Swagger
│   ├── /Migrations                  # Code-first EF Core Migrations
│   ├── appsettings.json             # File cấu hình chính
│   ├── Program.cs                   # Entry Point của ứng dụng
│
│── /auth_session.Core               # Dự án chứa logic chung (Domain Layer)
│   ├── /Entities                     # Các thực thể (Entity Models)
│   ├── /Interfaces                   # Các interface cho Service và Repository
│
│── /auth_session.Infrastructure     # Dự án chứa dữ liệu và kết nối với DB (Data Access Layer)
│   ├── /Persistence                  # DbContext, Migrations
│   ├── /Repositories                 # Triển khai repository pattern
│
│── /auth_session.Common             # Dự án chứa các tiện ích dùng chung
│   ├── /Helpers                      # Các helper class
│   ├── /Extensions                   # Các extension method
│
└── README.md                        # Tài liệu dự án
```

---

## 🚀 Cài đặt và Chạy Dự án

### Yêu cầu hệ thống:
- .NET 8 trở lên
- SQL Server hoặc PostgreSQL
- Visual Studio / VS Code
- Postman (tùy chọn)

### Cách chạy dự án:
1. **Clone repository**
   ```sh
    git clone https://github.com/MinzNhat/auth_session
    cd auth_session
   ```
2. **Cấu hình database**
   - Tạo file `.env` trong folder `auth_session.API` và cập nhật chuỗi kết nối.
   ```js
    DB_AUTHDB_CONNECTION=Server=your_server;Database=AuthDb;Trusted_Connection=True;User ID=sid;Password=pw;Encrypt=False;
    DB_SALESPROJDB_CONNECTION=Server=your_server;Database=SalesProjDb;Trusted_Connection=True;User ID=sid;Password=pw;Encrypt=False;
    DB_DATAWAREHOUSE_CONNECTION=Server=your_server;Database=DataWarehouseDb;Trusted_Connection=True;User ID=sid;Password=pw;Encrypt=False;
   ```
3. **Dọn dẹp và khôi phục dependencies**
   ```sh
    dotnet clean
    dotnet restore
   ```
4. **Chạy migration và cập nhật database**
   ```sh
    dotnet ef migrations add InitialCreate_Auth --context AuthDbContext --project auth_session.Infrastructure --startup-project auth_session.API
    dotnet ef migrations add InitialCreate_SalesProj --context SalesProjDbContext --project auth_session.Infrastructure --startup-project auth_session.API
    dotnet ef migrations add InitialCreate_DataWarehouse --context DataWarehouseDbContext --project auth_session.Infrastructure --startup-project auth_session.API

    dotnet ef database update --context AuthDbContext --project auth_session.Infrastructure --startup-project auth_session.API
    dotnet ef database update --context SalesProjDbContext --project auth_session.Infrastructure --startup-project auth_session.API
    dotnet ef database update --context DataWarehouseDbContext --project auth_session.Infrastructure --startup-project auth_session.API
   ```
5. **Chạy dự án**
   ```sh
    dotnet run --project auth_session.API
   ```
6. **Truy cập Swagger UI**
   - Mở trình duyệt và truy cập: [http://localhost:5259/swagger/index.html](http://localhost:5259/swagger/index.html)

---

## ⚡ Chạy script tự động (Tùy chọn)

### 🖥️ Windows
Chạy lệnh sau trong **PowerShell**:
```powershell
powershell -ExecutionPolicy Bypass -File ".\auth_session.Command\Windows\run.ps1"
```

### 🐧 Linux / MacOS (Bash)
Cấp quyền thực thi và chạy script:
```sh
chmod +x ./auth_session.Command/Linux_MacOs/run.sh && ./auth_session.Command/Linux_MacOs/run.sh
```

---

## 🔐 Xác thực Session-based
- Người dùng đăng nhập bằng API `/api/auth/login`.
- Server lưu session trong **InMemory**.
- Các request tiếp theo cần gửi cookie session để xác thực.

---

## 📌 Các API chính

| Phương thức | Endpoint                 | Chức năng                           | Yêu cầu Authentication |
|------------|-------------------------|----------------------------------|------------------------|
| `POST`    | `/api/auth/register`     | Đăng ký tài khoản               | Không                  |
| `POST`    | `/api/auth/login`        | Đăng nhập, lưu Session          | Không                  |
| `POST`    | `/api/auth/logout`       | Đăng xuất, xóa Session          | Có                     |
| `PUT`     | `/api/auth/change_password` | Đổi mật khẩu                  | Có                     |
| `GET`     | `/api/auth/{id}`         | Lấy thông tin người dùng theo ID | Có                     |
| `GET`     | `/api/auth/get_info`     | Lấy thông tin người dùng đang đăng nhập | Có                     |
| `DELETE`  | `/api/auth/{id}`         | Xóa thông tin người dùng theo ID | Có                     |
| `GET`     | `/api/auth/get_all`      | Lấy danh sách toàn bộ người dùng | Có                     |

---

💡 Cảm ơn bạn đã sử dụng **Session-based Authentication API**! 🚀