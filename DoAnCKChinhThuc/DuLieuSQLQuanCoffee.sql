use master
go
create database DoAnQuanLyBanCoffee
go
use DoAnQuanLyBanCoffee
go

create table NHANVIEN(
	MaNV varchar(10) primary key,
	TenNV nvarchar(50),
	GioiTinh nvarchar(5),
	DiaChi nvarchar(50),
	SoDienThoai nvarchar(11),
	PhanQuyen varchar(10),
	MatKhau varchar(20)
)
create table LOAIKH(
	MaLKH varchar(10) primary key,
	TenLKH nvarchar(50)
)
create table LOAIHH(
	MaLH varchar(10) primary key,
	TenLH nvarchar(50),
	MoTa nvarchar(50)
)
create table KHUVUC(
	MaKV varchar(10) primary key,
	TenKV nvarchar(50)
)
create table KHACHHANG(
	MaKH varchar(10) primary key,
	MaLKH varchar(10),
	TenKH nvarchar(50),
	DiaChi nvarchar(50),
	SDT nvarchar(11),
	DiemTichLuy int,
	constraint FK_LoaiKH Foreign key (MALKH) references LOAIKH(MALKH)
)
create table HANGHOA(
	MaHH varchar(10) primary key,
	MaLH varchar(10),
	TenHH nvarchar(50),
	HinhAnh nvarchar(50),
	GiaSP int,
	constraint FK_LoaiHH Foreign key (MaLH) references LOAIHH(MALH)
)
create table BAN(
	MaBan varchar(10) primary key,
	TenBan nvarchar(50),
	MaKV varchar(10),
	TrangThai nvarchar(50),
	constraint FK_KV foreign key (MaKV) references KHUVUC(MaKV)
)
Create table HOADON(
	MaHDBH int primary key,
	MaNV varchar(10),
	MaBan varchar(10),
	NgayXuatHD datetime,
	TongTien int,
	DiemTL int,
	GiamGia float,
	MaKH varchar(10),
	constraint FK_HoaDon_NV foreign key (MaNV) references NHANVIEN(MaNV),
	constraint FK_HoaDon_Ban foreign key (MaBan) references BAN(MaBan),
	constraint FK_HoaDon_KH foreign key (MaKH) references KHACHHANG(MaKH)
)
create table CHITIETHOADON
(
	MaHDBH int,
	MaHH varchar(10),
	SoLuong int,
	constraint PK_ChiTietHoaDon Primary key (MaHDBH,MaHH),
	constraint FK_CTHD_HD Foreign key (MaHDBH) references HOADON(MaHDBH),
	constraint FK_CTHD_HH Foreign key (MaHH) references HANGHOA(MaHH)
)
go
INSERT INTO NHANVIEN (MaNV, TenNV, GioiTinh, DiaChi, SoDienThoai, PhanQuyen, MatKhau)
VALUES 
('NV001', N'Ngô Thành Quang', N'Nam', N'Hà Nội', '0123456789', 'Admin','admin123'),
('NV002', N'Nguyễn Thị B', N'Nữ', N'Hồ Chí Minh', '0987654321', 'User','user123'),
('NV003', N'Lê Văn C', N'Nam', N'Đà Nẵng', '0369852147', 'User','user123'),
('NV004', N'Phạm Thị D', N'Nữ', N'Hải Phòng', '0321456987', 'User','user123'),
('NV005', N'Huỳnh Văn E', N'Nam', N'Bình Dương', '0912345678', 'User','user123')
select * from NHANVIEN
Go
INSERT INTO LOAIKH (MaLKH, TenLKH)
VALUES 
('LKH001', N'Khách hàng VIP'),
('LKH002', N'Khách hàng thành viên thường')
select * from LOAIKH
GO
INSERT INTO LOAIHH (MaLH, TenLH, MoTa)
VALUES 
('LH001', N'Cà phê', N'Các loại cà phê'),
('LH002', N'Nước ngọt', N'Nước ngọt các loại'),
('LH003', N'Trà sữa', N'Các loại trà sữa'),
('LH004', N'Bánh ngọt', N'Các loại bánh ngọt')
select * from LOAIHH
GO
INSERT INTO KHUVUC (MaKV, TenKV)
VALUES 
('KV001', N'Khu A'),
('KV002', N'Khu B'),
('KV003', N'Khu tầng lầu'),
('KV004', N'Khu gần cửa sổ'),
('KV005', N'Khu trước hiên')
select * from KHUVUC
GO
INSERT INTO KHACHHANG (MaKH, MaLKH, TenKH, DiaChi, SDT, DiemTichLuy)
VALUES 
('KH001', 'LKH001', N'Võ Hoàn Kiên', N'Hà Nội', '0123456789', 100),
('KH002', 'LKH002', N'Nguyễn Thị Ý', N'Hồ Chí Minh', '0987654321', 50),
('KH003', 'LKH002', N'Trần Văn Vũ', N'Đà Nẵng', '0369852147', 20),
('KH004', 'LKH002', N'Lê Thị Mai', N'Hải Phòng', '0321456987', 70),
('KH005', 'LKH001', N'Phạm Nam Thuận', N'Bình Dương', '0912345678', 10)
select * from KHACHHANG
GO
INSERT INTO HANGHOA (MaHH, MaLH, TenHH, HinhAnh, GiaSP)
VALUES 
('HH001', 'LH001', N'Cà phê sữa đá', N'caPheSuaDa.jpg', 25000),
('HH002', 'LH002', N'Coca cola', N'coCaCoLa.jpg', 15000),
('HH003', 'LH003', N'Trà sữa truyền thống', N'traSuaTruyenThong.jpg', 35000),
('HH004', 'LH001', N'Cà phê bạc xỉu', N'caPheBacXiu.jpg', 30000),
('HH005', 'LH004', N'Bánh ngọt socola', N'banhNgotSoCoLa.jpg', 20000),
('HH006', 'LH003', N'Trà sữa phô mai', N'traSuaPhoMai.jpg', 35000),
('HH007', 'LH001', N'Cà phê đen đá', N'caPheDenDa.jpg', 12000)
select * from HANGHOA
GO
INSERT INTO BAN (MaBan, TenBan, MaKV)
VALUES 
('B001', N'Bàn số 1A', 'KV001'),
('B002', N'Bàn số 2A', 'KV001'),
('B003', N'Bàn số 3A', 'KV001'),
('B004', N'Bàn số 4A', 'KV001'),
('B005', N'Bàn số 5A', 'KV001'),
('B006', N'Bàn số 1B', 'KV002'),
('B007', N'Bàn số 2B', 'KV002'),
('B008', N'Bàn số 3B', 'KV002'),
('B009', N'Bàn số 4B', 'KV002'),
('B010', N'Bàn số 5B', 'KV002'),
('B011', N'Bàn số 1LAU', 'KV003'),
('B012', N'Bàn số 2LAU', 'KV003'),
('B013', N'Bàn số 3LAU', 'KV003'),
('B014', N'Bàn số 4LAU', 'KV003'),
('B015', N'Bàn số 5LAU', 'KV003'),
('B016', N'Bàn số 1CS', 'KV004'),
('B017', N'Bàn số 2CS', 'KV004'),
('B018', N'Bàn số 3CS', 'KV004'),
('B019', N'Bàn số 1Hien', 'KV005'),
('B020', N'Bàn số 2Hien', 'KV005')
go
update BAN
set trangthai = N'Trống'
go
select * from BAN
GO
INSERT INTO HOADON (MaHDBH, MaNV, MaBan, NgayXuatHD, TongTien, DiemTL, GiamGia, MaKH)
VALUES 
(1, 'NV001', 'B001', '2023-11-20 10:00:00', 300000, 30, 0, 'KH001'),
(2, 'NV002', 'B002', '2023-11-21 11:30:00', 450000, 45, 0, 'KH002'),
(3, 'NV001', 'B003', '2023-11-22 09:45:00', 200000, 20, 0, 'KH003'),
(4, 'NV001', 'B004', '2023-11-23 13:20:00', 500000, 50, 0, 'KH004'),
(5, 'NV002', 'B019', '2023-11-24 14:00:00', 150000, 15, 0, 'KH005')
select * from HOADON
GO
INSERT INTO CHITIETHOADON (MaHDBH, MaHH, SoLuong)
VALUES 
(1, 'HH001', 2),
(1, 'HH002', 3),
(2, 'HH003', 1),
(3, 'HH004', 4),
(4, 'HH005', 1),
(5, 'HH001', 2),
(5, 'HH003', 2),
(4, 'HH004', 3),
(3, 'HH002', 1),
(2, 'HH001', 4)
select * from CHITIETHOADON
GO