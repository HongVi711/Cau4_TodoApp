# Cau4_TodoApp

Hướng dẫn chạy dự án TodoApp

Bước 1: Mở sql Server và chạy đoạn code sau

CREATE DATABASE TodoApp;

GO

USE TodoApp;

GO

CREATE TABLE TaskItems (
    Id NVARCHAR(50) PRIMARY KEY,          
    Title NVARCHAR(255) NOT NULL,         
    Description NVARCHAR(MAX) NULL,        
    IsCompleted BIT NOT NULL,              
    CreatedAt DATETIME NOT NULL,           
    UpdatedAt DATETIME NULL                
);

GO

*Lí do chọn sql Server:

- Cấu trúc dữ liệu đơn giản, cố định

- Chủ yếu thực hiện CRUD (Create, Update, Delete)
  
- Dữ liệu không phức tạp, không cần scale lớn

- Tích hợp tốt với .NET

---

Bước 2: Clone source code

Mở terminal và chạy lệnh:

git clone https://github.com/HongVi711/Cau4_TodoApp.git

---

Bước 3: Chạy Backend (.NET)

- Mở thư mục `backend` bằng Visual Studio.
  
- Nhấn nút "Run" để chạy ứng dụng.

---

Bước 4: Chạy Frontend (React)

- Mở thư mục `frontend` bằng Visual Studio Code.
  
- Mở terminal trong VS Code.
  
- Chạy lần lượt các lệnh sau:

npm install

npm run dev

---

Yêu cầu:

- Đã cài .NET SDK 6.0 trở lên
  
- Đã cài Node.js (version 16 hoặc mới hơn)
