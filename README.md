# LAB W10 – SMART HOME MINI

## 1. Giới thiệu

Xây dựng ứng dụng điều khiển nhà thông minh (Smart Home Mini) với ba loại thiết bị:
- SmartLight: Đèn thông minh.
- SmartFan: Quạt thông minh.
- SmartDoor: Cửa thông minh.

Ứng dụng sử dụng ba Design Pattern: Factory, Singleton và Facade.

## 2. Các Design Pattern được sử dụng

### 2.1. Factory Pattern

**Vị trí:** `DeviceFactory` trong `Pattern.cs`.

Factory cung cấp phương thức `Create()` để tạo thiết bị dựa trên loại và tên do client truyền vào.

**Lợi ích:**
- Tách biệt quá trình khởi tạo đối tượng khỏi client.
- Client không cần biết constructor của từng lớp thiết bị.
- Dễ quản lý việc tạo các loại thiết bị.

**Hạn chế:**
- Khi bổ sung loại thiết bị mới, cần chỉnh sửa phương thức `Create()` trong Factory.

### 2.2. Singleton Pattern

**Vị trí:** `HomeConfiguration` trong `Pattern.cs`.

Sử dụng `Lazy<T>` để đảm bảo chỉ có một thể hiện cấu hình chung trong ứng dụng.

**Lợi ích:**
- Đảm bảo các thành phần sử dụng chung một cấu hình.
- Tránh tạo nhiều đối tượng cấu hình không cần thiết.
- Hỗ trợ khởi tạo an toàn trong môi trường đa luồng.

**Hạn chế:**
- Tạo phụ thuộc vào trạng thái toàn cục.
- Có thể gây khó khăn khi kiểm thử và thay thế cấu hình.

### 2.3. Facade Pattern

**Vị trí:** `SmartHomeFacade` trong `Pattern.cs`.

Cung cấp hai phương thức điều khiển:
- `GoToWork()`: Tắt đèn, tắt quạt và khóa cửa.
- `GoToSleep()`: Tắt quạt, khóa cửa, bật đèn ngủ và hiển thị mức sáng ban đêm.

**Lợi ích:**
- Đơn giản hóa việc điều khiển nhiều thiết bị.
- Client không cần gọi từng thiết bị riêng lẻ.
- Giảm sự phụ thuộc của client vào các lớp cụ thể.

**Hạn chế:**
- Khi có thêm nhiều chế độ điều khiển, Facade có thể trở nên phức tạp.

## 3. Kết quả kiểm thử

| Mã | Nội dung kiểm thử | Kết quả mong đợi |
|---|---|---|
| T1 | Tạo Light, Fan, Door | Trả về đúng kiểu thiết bị |
| T2 | Tạo Camera | ArgumentException |
| T3 | Kiểm tra Singleton | Hai biến cùng tham chiếu một đối tượng |
| T4 | Gọi GoToWork() | Đèn tắt, quạt tắt, cửa khóa |
| T5 | Gọi GoToSleep() | Quạt tắt, cửa khóa, đèn bật, hiển thị mức sáng |

## 4. Trả lời câu hỏi

**Câu 1. Factory đang che giấu quyết định thiết kế nào?**

Factory che giấu việc lựa chọn lớp cụ thể và cách khởi tạo đối tượng. Client chỉ cần cung cấp loại thiết bị và tên thiết bị.

**Câu 2. Singleton bảo đảm điều gì, nhưng tạo ra rủi ro nào?**

Singleton đảm bảo chỉ có một thể hiện được sử dụng trong ứng dụng. Tuy nhiên, nó tạo ra trạng thái toàn cục, làm tăng sự phụ thuộc và gây khó khăn cho việc kiểm thử.

**Câu 3. Facade khác Factory ở mục đích chính như thế nào?**

Factory tập trung vào việc tạo đối tượng, còn Facade cung cấp giao diện đơn giản để điều khiển một nhóm đối tượng.

**Câu 4. Nếu bỏ SmartHomeFacade, client sẽ phải biết thêm những chi tiết nào?**

Client phải biết từng thiết bị cần điều khiển, phương thức tương ứng và thứ tự thực hiện các thao tác trong từng chế độ.

## 5. Công nghệ sử dụng

- Ngôn ngữ: C#
- Nền tảng: .NET
- IDE: Visual Studio Code