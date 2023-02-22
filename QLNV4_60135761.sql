create database QLNV4_60135761
use QLNV4_60135761

create table LoaiNhanVien
(
	MaLoaiNV varchar(5) primary key,	
	TenLoaiNhanVien nvarchar (20) not null
)

create table BoPhan 
(
	MaBP varchar (5) primary key, 
	TenBoPhan nvarchar (30) not null
)


create table ViTriCongViec
(
	MaVT varchar(7) primary key,
	TenViTriCongViec nvarchar(70) not null,
	MaBP varchar(5) foreign key references BoPhan(MaBP)
	on update cascade
	on delete cascade
)

create table ChucVu
(
	MaCV varchar(5) primary key,
	TenChucVu nvarchar(30) not null
)

create table QuocTich
(
	MaQT varchar(5) primary key,
	TenQuocTich nvarchar(20) not null
)

create table TrinhDoDaoTao
(
	MaTD varchar(5) primary key,
	TenTrinhDo nvarchar(20) not null
)

create table NhanVien
(
	MaNV varchar(10) primary key,
	HoTenNV nvarchar(50) not null,
	NgaySinh Datetime not null,
	GioiTinh bit not null,
	AnhNV nvarchar(100) not null,
	DiaChi nvarchar(60) not null,
	SDT varchar(10) not null, 
	CMND varchar(20)not null,
	TrangThai nvarchar(20)not null,	
	MaNVQL varchar(10) foreign key references NhanVien(MaNV),
	MaVT varchar(7) foreign key references ViTriCongViec(MaVT),														
	MaTD varchar(5) foreign key references TrinhDoDaoTao(MaTD),
	MaQT varchar(5) foreign key references QuocTich(MaQT),
	MaCV varchar(5) foreign key references ChucVu(MaCV),
	MaLoaiNV varchar(5) foreign key references LoaiNhanVien(MaLoaiNV)
	on delete cascade
	on update cascade
)

create table TaiKhoan
(
	MaTK int identity(1,1) primary key,
	Email varchar(30) not null,
	Password varchar(40) not null,
	NgayTao datetime not null,
	Quyen nvarchar(30) not null,
	KichHoat bit not null,
	MaNV varchar(10) foreign key references NhanVien(MaNV)
	on update cascade
	on delete cascade
)

create table LuongNhanVien 
(
	MaLuong varchar(5) primary key,
	LuongHienTai int not null,
	NgayCapNhat datetime not null,
	DonViTien nvarchar(10) not null,
	MaNV varchar(10) foreign key references NhanVien(MaNV)
	on update cascade
	on delete cascade
)

create table HopDong
(
	SoHD varchar(12) primary key,
	NgayKy Datetime not null,
	NgayBatDau Datetime not null,
	NgayKetThuc Datetime not null,
	NguoiDaiDien nvarchar(40) not null,	
	TinhTrang nvarchar(20) not null,
	GhiChu nvarchar(100),
	MaNV varchar(10) foreign key references NhanVien(MaNV),
	MaLuong varchar(5) foreign key references LuongNhanVien(MaLuong)
	on update cascade
	on delete cascade
)

create table QuyetDinhThangChuc 
(
	SoQD varchar(10) primary key,
	NoiDungQuyetDinh nvarchar(max) not null,
	NgayTao datetime not null,
	MaNV varchar(10) foreign key references NhanVien(MaNV)
	on update cascade
	on delete cascade
)

create table PhieuDanhGia
(
	MaPhieu varchar(5) primary key,
	TenDanhGia nvarchar(30) not null,
	KyDanhGia int not null, 
	Nam int not null, 
	Khoa bit not null, 
	MaNV varchar(10) foreign key references NhanVien(MaNV)
	on update cascade
	on delete cascade
)

create table LoaiDanhGia 
(
	MaLDG Int primary key, 
	TenLoaiDanhGia nvarchar(30) not null
)

create table TieuChiYeuCau 
(
	MaTieuChi int primary key,
	TenTieuChi nvarchar(50) not null,
	MaLDG int foreign key references LoaiDanhGia(MaLDG)
	on update cascade
	on delete cascade
)

create table CTPhieuDanhGia
(
	Diem int not null,
	MaPhieu varchar(5) foreign key references PhieuDanhGia(MaPhieu),
	MaTieuChi int foreign key references TieuChiYeuCau(MaTieuChi)
	on update cascade
	on delete cascade
	constraint pk_CTHDB primary key(MaPhieu,MaTieuChi)
)

create table DeXuatTangLuong
(
	MaDeXuat varchar(5) primary key,
	NgayDeXuat Datetime not null,
	LiDoDeXuat nvarchar(30) not null,
	LuongMongMuon int not null,
	TrangThai nvarchar(20) not null,
	MaNV varchar(10) foreign key references NhanVien(MaNV)
	on update cascade
	on delete cascade
)


create table QuanTriVien
(
	ID int primary key,
	Username varchar(40),
	Password varchar(40),
)

insert into QuanTriVien values (1,'nguyenquochuy','123')

insert into LoaiNhanVien values ('LNV01',N'Nhân viên thử việc')
insert into LoaiNhanVien values ('LNV02',N'Nhân viên học việc')
insert into LoaiNhanVien values ('LNV03',N'Nhân viên chính thức')

insert into BoPhan values ('BP01',N'Bộ phận Kế toán')
insert into BoPhan values ('BP02',N'Bộ phận Nhân sự')
insert into BoPhan values ('BP03',N'Bộ phận Hành chính')
insert into BoPhan values ('BP04',N'Bộ phận IT')
insert into BoPhan values ('BP05',N'Bộ phận vận hành')


insert into ViTriCongViec values ('VT01',N'Tester','BP04')
insert into ViTriCongViec values ('VT02',N'.Net Developer','BP04')
insert into ViTriCongViec values ('VT03',N'FrontEnd Developer','BP04')
insert into ViTriCongViec values ('VT04',N'Business Analyst','BP04')
insert into ViTriCongViec values ('VT05',N'Project Manager','BP04')
insert into ViTriCongViec values ('VT06',N'Java Developer','BP04')
insert into ViTriCongViec values ('VT07',N'Software Engineer','BP04')
insert into ViTriCongViec values ('VT08',N'Nhân viên kế toán','BP01')
insert into ViTriCongViec values ('VT09',N'Nhân viên hành chính','BP03' )
insert into ViTriCongViec values ('VT10',N'Giám đốc điều hành','BP05' )
insert into ViTriCongViec values ('VT11',N'Chuyên viên nhân sự','BP02')
insert into ViTriCongViec values ('VT12',N'Kế toán trưởng','BP01')
insert into ViTriCongViec values ('VT13',N'Trưởng phòng vận hành','BP05')
insert into ViTriCongViec values ('VT14',N'HR Specialist','BP02')
insert into ViTriCongViec values ('VT15',N'Trợ lý nhân sự','BP02')


insert into ChucVu values ('CV01',N'Nhân viên')
insert into ChucVu values ('CV02',N'Quản lý')
insert into ChucVu values ('CV03',N'Trưởng phòng')

insert into QuocTich values ('QT01',N'Việt Nam')
insert into QuocTich values ('QT02',N'Hà Lan')

insert into TrinhDoDaoTao values ('TD01',N'Cao đẳng')
insert into TrinhDoDaoTao values ('TD02',N'Đại học')
insert into TrinhDoDaoTao values ('TD03',N'Tiến sĩ')
insert into TrinhDoDaoTao values ('TD04',N'Thạc sĩ')

	
	select  * from NhanVien
	select * from LuongNhanVien
insert into NhanVien values
('NV001',N'Nguyễn Thanh Hải',CAST(N'1986-08-10' AS Date),0,N'female2.png',N'38 Mạc Đỉnh Chi, Nha Trang','0985611192','235913674',N'Đang làm việc','NV001','VT13','TD02','QT01','CV03','LNV03'),
('NV002',N'Nguyễn Thị Huệ',CAST(N'1995-10-23' AS Date),0,N'female1.png',N'35 Hồng Bàng, Nha Trang','0148456881','248952161',N'Đang làm việc','NV001','VT08','TD01','QT01','CV01','LNV03'),
('NV003',N'Đỗ Xuân An',CAST(N'1992-05-09' AS Date),0,N'female2.png',N'40 Nguyễn Trãi, Nha Trang','0248456885','258952167',N'Đang làm việc','NV001','VT08','TD02','QT01','CV02','LNV03'),
('NV004',N'Trịnh Bảo Nam',CAST(N'1998-02-01' AS Date),1,N'male2.png',N'25 Lê Lợi, Nha Trang','0248456685','248952267',N'Đang làm việc','NV001','VT02','TD02','QT01','CV01','LNV01'),
('NV005',N'Nguyễn Hải Tiến',CAST(N'1998-06-10' AS Date),1,N'maletest1.png',N'Nha Trang','0775411191','223412675',N'Đang làm việc','NV001','VT03','TD02','QT01','CV01','LNV01'),
('NV006',N'Lê Xuân Lộc',CAST(N'1987-08-02' AS Date),1,N'maletest1.png',N'30 Phong Châu, Nha Trang','0755411191','224413675',N'Đang làm việc','NV001','VT04','TD03','QT01','CV01','LNV03'),
('NV007',N'Lê Hoài Phát',CAST(N'1989-07-12' AS Date),1,N'male2.png',N'35 Ngọc Hiệp, Nha Trang','0755611192','234413674',N'Đang làm việc','NV001','VT05','TD02','QT01','CV02','LNV03'),
('NV008',N'Trần Thị Hiền',CAST(N'1998-03-01' AS Date),0,N'female2.png',N'35 Lê Hồng Phong','0765612192','233413675',N'Đang làm việc','NV001','VT12','TD02','QT01','CV01','LNV03'),
('NV009',N'Nguyễn Trung Kiên',CAST(N'1999-10-23' AS Date),1,N'male1.png',N'25 Lê Thánh Tôn, Nha Trang','0128456889','238952168',N'Đang làm việc','NV001','VT02','TD02','QT01','CV01','LNV03'),
('NV010',N'Lê Thanh Long',CAST(N'1990-07-04' AS Date),1,N'male2.png',N'Phú Yên','0745612192','234513675',N'Đang làm việc','NV007','VT03','TD02','QT01','CV01','LNV03'),
('NV011',N'Đỗ Thanh Nam',CAST(N'1999-08-01' AS Date),1,N'maletest1.png',N'35 Lê Thánh Tôn','0754611191','234513671',N'Đang làm việc','NV007','VT06','TD02','QT01','CV01','LNV03'),
('NV012',N'Lê Thị Tuyết',CAST(N'1992-06-10' AS Date),0,N'female1.png',N'Đà Nẵng','0755711172','244513674',N'Đang làm việc','NV001','VT11','TD02','QT01','CV01','LNV03'),
('NV013',N'Trần Thị Bích Lợi',CAST(N'1995-08-10' AS Date),0,N'female2.png',N'40 Trần Phú, Nha Trang','0785611292','254413679',N'Đang làm việc','NV001','VT14','TD02','QT01','CV03','LNV03'),
('NV014',N'Đỗ Trung Nam',CAST(N'1992-06-14' AS Date),1,N'male2.png',N'41 Thái Nguyên, Nha Trang','0765611191','235413675',N'Đang làm việc','NV001','VT04','TD02','QT01','CV01','LNV03'),
('NV015',N'Lê Tấn Phát',CAST(N'1995-10-20' AS Date),1,N'male1.png',N'Đà Nẵng','0755611192','234413674',N'Đang làm việc','NV007','VT06','TD02','QT01','CV01','LNV03'),
('NV016',N'Nguyễn Tuấn Hải',CAST(N'1995-08-26' AS Date),1,N'maletest2.jpg',N'36 Hoa Lư, Nha Trang','0967122376','221135678',N'Đang làm việc','NV001','VT04','TD02','QT01','CV01','LNV03'),
('NV017',N'Nguyễn Cao Thạch',CAST(N'1996-07-24' AS Date),1,N'male2.png',N'35 Lương Định Của, Nha Trang','0781236751','225678123',N'Đang làm việc','NV001','VT05','TD02','QT01','CV01','LNV03'),
('NV018',N'Nguyễn Thu Sương',CAST(N'1996-05-06' AS Date),0,N'female2.png',N'38 Ngô Quyền, Nha Trang','0791236752','224678223',N'Đang làm việc','NV013','VT15','TD02','QT01','CV01','LNV03'),
('NV019',N'Nguyễn Trọng Nhân',CAST(N'1992-08-24' AS Date),1,N'male2.png',N'40 Trương Định, Nha Trang','0981236755','225978125',N'Đang làm việc','NV017','VT02','TD02','QT01','CV01','LNV03'),
('NV020',N'Nguyễn Tiến Đạt',CAST(N'1995-09-25' AS Date),1,N'male1.png',N'50 Trần Phú, Nha Trang','0791236751','225679123',N'Đang làm việc','NV017','VT02','TD03','QT01','CV01','LNV03'),
('NV021',N'Nguyễn Thị Ly',CAST(N'1999-06-25' AS Date),0,N'female1.png',N'40 Lê Đại Hành, Nha Trang','0791236751','225778123',N'Đang làm việc','NV001','VT01','TD01','QT01','CV01','LNV02'),
('NV022',N'Nguyễn Thị Như',CAST(N'1992-08-24' AS Date),0,N'female2.png',N'3 Lê Lợi, Nha Trang','0981236751','224678123',N'Đang làm việc','NV001','VT01','TD03','QT01','CV01','LNV03'),
('NV023',N'Nguyễn Xuân Yến',CAST(N'1996-07-24' AS Date),0,N'male2.png',N'35 Lương Định Của, Nha Trang','0781236751','225678123',N'Nghỉ việc','NV001','VT05','TD02','QT01','CV01','LNV03'),
('NV024',N'Nguyễn Tài Nam',CAST(N'1995-02-24' AS Date),1,N'male1.png',N'3 Ngọc Hiệp, Nha Trang','0791236751','235678123',N'Nghỉ việc','NV001','VT02','TD01','QT01','CV01','LNV03'),
('NV025',N'Nguyễn Tú Uyên',CAST(N'1992-04-25' AS Date),0,N'female2.png',N'39 Lê Thánh Tôn, Nha Trang','0791236751','225778123',N'Nghỉ việc','NV001','VT15','TD02','QT01','CV01','LNV03'),
('NV026',N'Nguyễn Trần Nam',CAST(N'1992-08-30' AS Date),1,N'male2.png',N'15 Nguyễn Đình Chiểu, Nha Trang','0781236761','225678124',N'Đang làm việc','NV001','VT07','TD04','QT01','CV01','LNV03'),
('NV027',N'Nguyễn Thị Lệ',CAST(N'1997-09-29' AS Date),0,N'female1.png',N'Phú Yên','0775411194','234567882',N'Đang làm việc','NV001','VT09','TD01','QT01','CV01','LNV03')

insert into LuongNhanVien values 
('ML01',30000000,CAST(N'2022-05-20' AS Date),'VND','NV001'),
('ML02',12000000,CAST(N'2022-05-20' AS Date),'VND','NV002'),
('ML03',14000000,CAST(N'2022-05-20' AS Date),'VND','NV003'),
('ML04',12000000,CAST(N'2022-06-01' AS Date),'VND','NV004'),
('ML05',13000000,CAST(N'2022-06-18' AS Date),'VND','NV005'),
('ML06',16000000,CAST(N'2022-06-18' AS Date),'VND','NV006'),
('ML07',18000000,CAST(N'2022-06-18' AS Date),'VND','NV007'),
('ML08',18000000,CAST(N'2022-06-18' AS Date),'VND','NV008'),
('ML09',12000000,CAST(N'2022-06-18' AS Date),'VND','NV009'),
('ML10',12000000,CAST(N'2022-06-18' AS Date),'VND','NV010'),
('ML11',12000000,CAST(N'2022-06-18' AS Date),'VND','NV011'),
('ML12',12000000,CAST(N'2022-06-18' AS Date),'VND','NV012'),
('ML13',12000000,CAST(N'2022-06-18' AS Date),'VND','NV013'),
('ML14',12000000,CAST(N'2022-06-18' AS Date),'VND','NV014'),
('ML15',18000000,CAST(N'2022-06-24' AS Date),'VND','NV015'),
('ML16',18000000,CAST(N'2022-06-25' AS Date),'VND','NV016'),
('ML17',15000000,CAST(N'2022-06-25' AS Date),'VND','NV017'),
('ML18',17000000,CAST(N'2022-06-25' AS Date),'VND','NV018'),
('ML19',16000000,CAST(N'2022-06-25' AS Date),'VND','NV019'),
('ML20',20000000,CAST(N'2022-06-25' AS Date),'VND','NV020'),
('ML21',15000000,CAST(N'2022-06-25' AS Date),'VND','NV021'),
('ML22',14000000,CAST(N'2022-06-25' AS Date),'VND','NV022'),
('ML23',14000000,CAST(N'2022-06-25' AS Date),'VND','NV023'),
('ML24',16000000,CAST(N'2022-06-25' AS Date),'VND','NV024'),
('ML25',15000000,CAST(N'2022-06-25' AS Date),'VND','NV025'),
('ML26',25000000,CAST(N'2022-06-25' AS Date),'VND','NV026'),
('ML27',15000000,CAST(N'2022-07-02' AS Date),'VND','NV027')

select * from HopDong where MaNV='NV027'
insert into HopDong values 
('I20191223',CAST(N'2019-12-23' AS Date),CAST(N'2019-12-23' AS Date),CAST(N'2021-12-23' AS Date),N'Nguyễn Văn Thông',N'Hết hạn',N'','NV009','ML09'),
('I20210115',CAST(N'2021-01-15' AS Date),CAST(N'2021-01-15' AS Date),CAST(N'2023-01-15' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV002','ML02'),
('I20220202',CAST(N'2022-02-02' AS Date),CAST(N'2022-02-02' AS Date),CAST(N'2023-02-02' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV003','ML03'),
('I20220531',CAST(N'2022-05-31' AS Date),CAST(N'2022-06-01' AS Date),CAST(N'2024-02-02' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV004','ML04'),
('I20210620',CAST(N'2021-06-20' AS Date),CAST(N'2021-06-20' AS Date),CAST(N'2022-06-20' AS Date),N'Nguyễn Văn Thông',N'Hết hạn',N'','NV005','ML05'),
('I20200625',CAST(N'2020-06-25' AS Date),CAST(N'2020-06-25' AS Date),CAST(N'2022-06-25' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV006','ML06'),
('I20210710',CAST(N'2017-07-10' AS Date),CAST(N'2017-07-10' AS Date),CAST(N'2022-07-10' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV007','ML07'),
('I20210715',CAST(N'2021-07-15' AS Date),CAST(N'2021-07-15' AS Date),CAST(N'2022-07-15' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV008','ML08'),
('I20220621',CAST(N'2022-06-21' AS Date),CAST(N'2022-06-21' AS Date),CAST(N'2024-06-21' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV009','ML09'),
('I20220601',CAST(N'2022-06-01' AS Date),CAST(N'2022-06-01' AS Date),CAST(N'2023-06-01' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV010','ML09'),
('I20210701',CAST(N'2021-07-01' AS Date),CAST(N'2021-07-01' AS Date),CAST(N'2022-07-01' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV011','ML11'),
('I20220101',CAST(N'2022-01-01' AS Date),CAST(N'2022-01-01' AS Date),CAST(N'2024-01-01' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV001','ML01'),
('I20200901',CAST(N'2020-09-01' AS Date),CAST(N'2020-09-01' AS Date),CAST(N'2022-09-01' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV012','ML12'),
('I20201212',CAST(N'2020-12-12' AS Date),CAST(N'2020-12-12' AS Date),CAST(N'2023-12-12' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV013','ML13'),
('I20200906',CAST(N'2020-09-06' AS Date),CAST(N'2020-09-06' AS Date),CAST(N'2022-09-06' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV014','ML14'),
('I20191105',CAST(N'2019-11-05' AS Date),CAST(N'2019-11-05' AS Date),CAST(N'2022-11-05' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV015','ML15'),
('I20201005',CAST(N'2020-10-05' AS Date),CAST(N'2020-10-05' AS Date),CAST(N'2022-10-05' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV016','ML16'),
('I20210106',CAST(N'2021-10-06' AS Date),CAST(N'2021-10-06' AS Date),CAST(N'2023-10-06' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV017','ML17'),
('I20210708',CAST(N'2021-07-08' AS Date),CAST(N'2021-07-08' AS Date),CAST(N'2022-07-08' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV018','ML18'),
('I20220401',CAST(N'2022-04-01' AS Date),CAST(N'2022-04-01' AS Date),CAST(N'2024-04-01' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV019','ML19'),
('I20220505',CAST(N'2022-05-05' AS Date),CAST(N'2022-05-05' AS Date),CAST(N'2024-05-05' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV020','ML20'),
('I20211209',CAST(N'2021-12-09' AS Date),CAST(N'2021-12-09' AS Date),CAST(N'2023-12-09' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV021','ML21'),
('I20210718',CAST(N'2021-07-18' AS Date),CAST(N'2021-07-18' AS Date),CAST(N'2023-07-18' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV022','ML22'),
('I20190808',CAST(N'2019-08-08' AS Date),CAST(N'2019-08-08' AS Date),CAST(N'2021-08-08' AS Date),N'Nguyễn Văn Thông',N'Hết hạn',N'','NV023','ML23'),
('I20200706',CAST(N'2020-07-06' AS Date),CAST(N'2020-07-06' AS Date),CAST(N'2021-07-06' AS Date),N'Nguyễn Văn Thông',N'Hết hạn',N'','NV024','ML24'),
('I20200808',CAST(N'2020-08-08' AS Date),CAST(N'2020-08-08' AS Date),CAST(N'2021-08-08' AS Date),N'Nguyễn Văn Thông',N'Hết hạn',N'','NV025','ML25'),
('I20211210',CAST(N'2021-12-10' AS Date),CAST(N'2021-12-10' AS Date),CAST(N'2023-12-10' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV026','ML26'),
('I20210521',CAST(N'2021-05-21' AS Date),CAST(N'2021-05-21' AS Date),CAST(N'2023-05-21' AS Date),N'Nguyễn Văn Thông',N'Còn hạn',N'','NV027','ML27')


insert into TaiKhoan values
('nguyentrungkien@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2021-12-23' AS Date),N'Nhân viên',1,'NV009'),
('nguyenthihue@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-01-15' AS Date),N'Nhân viên',1,'NV002'),
('doxuanan@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV003'),
('trinhbaonam@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV004'),
('nguyenhaitien@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV005'),
('lexuanloc@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV006'),
('lehoaiphat@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV007'),
('tranthihien@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV008'),
('nguyenthanhhai@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Quản lý',1,'NV001'),
('lethanhlong@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV010'),
('dothanhnam@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV011'),
('lethituyet@gmail.com','202cb962ac59075b964b07152d234b70',CAST(N'2022-02-02' AS Date),N'Nhân viên',1,'NV012')


select * from NhanVien
insert into QuyetDinhThangChuc values
('QD01',N'Bổ nhiệm lên làm quản lý nhân sự',CAST(N'2022-06-28' AS Date),'NV003')

insert into LoaiDanhGia values 
(1,N'Năng lực'),
(2,N'Thái độ')

select * from TaiKhoan

insert into TieuChiYeuCau values 
(1,N'Mức độ thông thạo các kỹ năng CNTT',1),
(2,N'Kinh nghiệm làm việc trong lĩnh vực CNTT',1),
(3,N'Số dự án thực tế đã từng tham gia',1),
(4,N'Khả năng đọc, hiểu tài liệu Tiếng Anh',1),
(5,N'Khả năng về CNTT',1),
(6,N'Khả năng xử lý lỗi',1),
(7,N'Chuyên cần',2),
(8,N'Ý thức chung',2),
(9,N'Sự hoà đồng',2)

select * from TaiKhoan

insert into DeXuatTangLuong values 
('DX01',CAST(N'2022-10-05' AS Date),N'Đã làm việc 3 năm tại công ty',17000000,N'đang xử lý','NV001')

 insert into PhieuDanhGia values
('P01',N'Đánh giá hiệu suất nhân sự',5,2022,0,'NV009'),
('P02',N'Đánh giá hiệu suất nhân sự',5,2022,0,'NV002'),
('P03',N'Đánh giá hiệu suất nhân sự',5,2022,0,'NV003')


insert into CTPhieuDanhGia values
(5,'P01',1),
(4,'P01',2),
(4,'P01',3),
(5,'P01',4),
(4,'P01',5),
(4,'P01',6),
(4,'P01',7),
(4,'P01',8),
(4,'P01',9),
(5,'P02',1),
(4,'P02',2),
(2,'P02',3),
(2,'P02',4),
(3,'P02',5),
(2,'P02',6),
(4,'P02',7),
(2,'P02',8),
(4,'P02',9),
(5,'P03',1),
(2,'P03',2),
(2,'P03',3),
(4,'P03',4),
(3,'P03',5),
(2,'P03',6),
(4,'P03',7),
(5,'P03',8),
(4,'P03',9)


-- Procedure
-- Tìm kiếm nhân viên
CREATE PROCEDURE NhanVien_TimKiem    
	@HoTen nvarchar(50)=NULL,
	@GioiTinh nvarchar(3)= NULL,
	@DiaChi nvarchar(60)=NULL,	
	@MaChucVu varchar(5)=NULL,	
	@MaLoaiNV varchar(5)=NULL,
	@MaQT varchar(5)= NULL,
	@MaTD varchar(5) = NULL,
	@MaVT varchar(7)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @HoTen IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoTenNV LIKE N''%'+@HoTen+'%'')
              '
IF @GioiTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (GioiTinh LIKE ''%'+@GioiTinh+'%'')
             '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (DiaChi LIKE N''%'+@DiaChi+'%'')
              '
IF @MaChucVu IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaCV LIKE ''%'+@MaChucVu+'%'')
              '
IF @MaLoaiNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaLoaiNV LIKE ''%'+@MaLoaiNV+'%'')
              '
IF @MaQT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaQT LIKE ''%'+@MaQT+'%'')
              '
IF @MaTD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaTD LIKE ''%'+@MaTD+'%'')
              '
IF @MaVT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaVT LIKE ''%'+@MaVT+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END

--tìm kiếm hợp đồng
CREATE PROCEDURE HopDong_TimKiem    
	@SoHD varchar(10)=NULL,
	@NguoiDaiDien nvarchar(40)= NULL,
	@TinhTrang nvarchar(20)=NULL,
	@MaNV varchar(10)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM HopDong
       WHERE  (1=1)
       '
IF @SoHD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (SoHD LIKE ''%'+@SoHD+'%'')
              '
IF @NguoiDaiDien IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (NguoiDaiDien LIKE N''%'+@NguoiDaiDien+'%'')
              '
IF @TinhTrang IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (TinhTrang LIKE N''%'+@TinhTrang+'%'')
             '
IF @MaNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (MaNV LIKE ''%'+@MaNV+'%'')
             '
	EXEC SP_EXECUTESQL @SqlStr
END




