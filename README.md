Mushroom Hostel - Phần mềm Quản lý phòng trọ
1. Giới thiệu dự án
Mushroom Hostel là giải pháp phần mềm được thiết kế nhằm số hóa quy trình quản lý nhà trọ truyền thống. Dự án tập trung vào việc tối ưu hóa tính toán, giảm thiểu sai sót thủ công và cung cấp cái nhìn tổng quan về tình trạng kinh doanh cho chủ cơ sở.
2. Các tính năng cốt lõi
Quản lý Sơ đồ phòng: Trực quan hóa trạng thái thực tế của từng phòng (Trống, Đã đặt, Đang thuê) cùng thông tin giá niêm yết.
Quản lý Hợp đồng: Hệ thống hóa thông tin khách thuê, hỗ trợ lập hợp đồng mới và theo dõi vòng đời hợp đồng (Hiệu lực, Thanh lý, Hủy).
Tính toán Hóa đơn tự động: Tự động chốt chỉ số điện/nước cũ - mới, áp đơn giá và xuất hóa đơn chính xác.
Báo cáo & Thống kê: Trích xuất báo cáo doanh thu theo tháng/năm và thống kê danh sách hợp đồng chuyên nghiệp qua công cụ RDLC.
3. Nền tảng & Công nghệ sử dụng:

Ngôn ngữ lập trình: C#

Framework giao diện: Windows Forms (WinForms) trên nền tảng .NET 8

Cơ sở dữ liệu: Microsoft SQL Server

Tương tác dữ liệu (ORM): Entity Framework Core (EF Core)

Xuất bản báo cáo: Microsoft ReportViewer (RDLC)

4 Kiến trúc hệ thống
Dự án được xây dựng dựa trên mô hình kết nối dữ liệu hiện đại, đảm bảo tính toàn vẹn và tốc độ xử lý:
Giao diện người dùng: Tiếp nhận thông tin qua các Form nhập liệu.
Tầng xử lý (Logic): Sử dụng C# để tính toán hóa đơn và điều phối trạng thái phòng.
Tầng dữ liệu: EF Core đóng vai trò cầu nối, đồng bộ dữ liệu giữa code và SQL Server.
Tầng báo cáo: ReportViewer truy xuất dữ liệu từ Database để tạo các bản in hóa đơn/thống kê.
Thành viên thực hiện
Dự án được phát triển bởi sinh viên ngành Công nghệ Thông tin – Đại học An Giang (AGU):
Đoàn Văn Phúc (MSSV: DTH235736) 

