$(document).ready(function () {
	LoadCategory();


	////Sjw kiện tìm kiếm
	//$('#btnTimKiem').click(function () {
	//	tukhoa = $('#txtTimKiem').val();
	//	console.log(tukhoa);
	//	if (tukhoa != "") {
	//		LoadCategory(tukhoa);
	//	} else {
	//		LoadCategory(null);
	//	}

	//});

	////Viết đến đâu chữ chuyển đến đó
	//$('#categoryName').keypress(function () {
	//	$('#categoryMeta').val(getMetaTitle($(this).val()));
	//});

	////Sự kiện xóa 1 hàng
	//$(document).on('click', "button[name='delete']", function () {

	//	let idCategory = $(this).closest('tr').attr('id');
	//	console.log('ID = ' + idCategory);
	//	if (confirm('Bạn có muốn xóa?')) {
	//		$.ajax({
	//			url: '/admin/category/DeleteCategory',
	//			type: 'POST',
	//			data: {
	//				id: idCategory
	//			},
	//			success: function (data) {
	//				if (data.code == 200) {
	//					alert(data.msg);
	//					$('#modalCategory').modal('hide');
	//					LoadCategory();
	//				} else {
	//					alert(data.msg);
	//				}
	//			}
	//		});
	//	}
	//});


	////Sự kiện cập nhật 1 hàng
	//$(document).on('click', "button[name='update']", function () {
	//	let idCategory = $(this).closest('tr').attr('id');
	//	console.log('ID = ' + idCategory);
	//	$.ajax({
	//		url: '/admin/category/DetailCategory',
	//		type: 'GET',
	//		data: {
	//			idCategory: idCategory
	//		},
	//		success: function (data) {
	//			if (data.code == 200) {
	//				$('#categoryName').val(data.Category.Name);
	//				$('#categoryMeta').val(data.Category.MetaTitle);

	//				//Gán id của hàng cho thẻ hidden
	//				$('#id_User').val(idCategory);

	//				//Vô hiệu text input
	//				EnableInput();

	//				$('#btnSubmit').show(); //Hiện button lưu

	//				$('#myModalLabel').text('Sửa thông tin');
	//				$('#modalCategory').modal(); //Hiện model lên
	//			}
	//		}
	//	});
	//});

	//Sự kiện cho button xem chi tiết
	$(document).on('click', "button[name='view']", function () {
		let idUser = $(this).closest('tr').attr('id');
		//$('#modalEmployee').modal();
		$.ajax({
			url: '/admin/Employee/DetailEmployee',
			type: 'GET',
			data: {
				idUser: idUser
			},
			success: function (data) {
				console.log(data);
				if (data.code == 200) {
					console.log(data.Emp.FullName);
					let quyen = data.Emp.Quyen == 1 ? "Admin" : "Nhân viên";
					let status = data.Emp.Active ? "Hoạt động" : "Khóa";

					//var date = new Date(, "dd/mm/yyyy"); 
					//var date = new Date(parseInt(data.Emp.DOB.replace("/Date(", "").replace(")/", ""), 10));
					//var date = moment(data.Emp.DOB).format("DD/MM/YYYY");
					//let nowDate = new Date(parseInt(data.Emp.DOB.substr(6)));
					//let result = nowDate.format('dd/mm/yyyy');
					let goitinh = data.Emp.GioiTinh ? "Nam" : "Nữ";
					console.log(goitinh);
					//console.log(typeof (result));
					$('#id').val(data.Emp.NVID);
					$('#fullname').val(data.Emp.FullName);
					$('#username').val(data.Emp.UserName);
					$('#password').val(data.Emp.NVPassword);
					$('#gioitinh').val(goitinh);
					$('#date').val(result);
					$('#phone').val(data.Emp.PhoneNum);
					$('#email').val(data.Emp.Email);
					$('#address').val(data.Emp.NVAddress);
					$('#quyen').append('<option selected value="' + data.Emp.Quyen + '">' + quyen + '</option>');
					$('#status').append('<option selected value="' + data.Emp.Active + '">' + status + '</option>');


					//Vô hiệu text input
					DisableInput();

					$('#btnSubmit').hide(); //Ẩn button lưu

					$('#myModalLabel').text('Chi tiết nhân viên');
					$('#modalEmployee').modal(); //Hiện model lên
				}
			}
		});
	});


	//Sự kiện click button thêm 
	$('#btnAdd').click(function () {
		$('#myModalLabel').text('Thêm mới nhân viên');
		$('#modalEmployee').modal();

		//$('#id_User').val(''); //Gán thẻ hidden rỗng 
		Resetvalue();
		EnableInput();

	});


	//Sự kiện khi click button lưu
	$('#btnSubmit').click(function () {
		let Name = $('#fullname').val().trim();
		let GioiTinh = $('#gioitinh').val().trim();
		let UserName = $('#username').val().trim();
		let Pass = $('#password').val().trim();
		let Phone = $('#phone').val().trim();
		let Email = $('#email').val().trim();
		let Address = $('#address').val().trim();
		let Quyen = $('#quyen').val().trim();
		let Status = $('#status').val().trim();

		if (!Validate(Name) || !Validate(GioiTinh) || !Validate(UserName)
			|| !Validate(Pass) || !Validate(Phone) || !Validate(Email)
			|| !Validate(Address) || !Validate(Quyen) || !Validate(Status)) {
			alert('Không được để trống');
			return;
		}
		//let idEmp = $('#id_User').val().trim();
		//if (idEmp == 0) {
		$.ajax({
			url: '/admin/employee/AddEmployee',
			type: 'POST',
			data: {
				UserName: UserName,
				FullName: Name,
				PhoneNum: Phone,
				Email: Email,
				GioiTinh: GioiTinh,
				NVAddress: Address,
				Quyen: Quyen,
				NVPassword: Pass,
			},
			success: function (data) {
				if (data.code == 200) {

					alert('Thêm thành công');
					LoadCategory();
					Resetvalue();
				} else {
					alert(data.msg);
				}
			}
		});
		//} else {
		//	//alert(1);
		//	$.ajax({
		//		url: '/admin/category/UpdateCategory',
		//		type: 'POST',
		//		data: {
		//			CategoryID: idCategory,
		//			Name: Name,
		//			MetaTitle: Meta
		//		},
		//		success: function (data) {
		//			if (data.code == 200) {
		//				alert(data.msg);
		//				$('#modalCategory').modal('hide'); //Ẩn button lưu
		//				LoadCategory();
		//			} else {
		//				alert(data.msg);
		//			}
		//		}
		//	});
		//}
	});


	//Load data danh sách nhãn hàng
	function LoadCategory() {
		$.ajax({
			url: '/admin/Employee/LoadEmployee',
			type: 'GET',
			datatype: 'json',
			contentType: 'application/json;charset=utf-8',
			success: function (data) {
				console.log(data);
				if (data.code == 200) {
					$('#tblEmployee').empty();
					$('#gioitinh').empty();
					$.each(data.lstEmployee, function (k, v) {

						let status = v.Active ? 'Bình thường' : 'Bị khóa';
						let quyen = v.Quyen == 1 ? "Quản trị" : "Nhân viên";
						let phone = v.PhoneNum == null ? "trống" : v.PhoneNum;
						let mail = v.Email == null ? "trống" : v.Email;
						let diachi = v.NVAddress == null ? "trống" : v.NVAddress;
						let gioitinh = v.GioiTinh ? "Nam" : "Nữ";
						let tr = '<tr id="' + v.NVID + '">'
						tr += '<td>' + v.FullName + '</td>';
						tr += '<td>' + gioitinh + '</td>';
						tr += '<td>' + phone + '</td>';
						tr += '<td>' + diachi + '</td>';
						tr += '<td>' + mail + '</td>';
						tr += '<td>' + quyen + '</td>';
						tr += '<td>' + status + '</td>';
						tr += '<td class="text-center">';
						tr += '<button name="view" type="button" class="btn btn-primary icon-only"><i class="fa fa-info"></i></button>&nbsp;';
						tr += '<button name="update" type="button" class="btn btn-warning icon-only"><i class="fa fa-edit"></i></button>&nbsp;';
						tr += '<button name="delete" type="button" class="btn btn-danger icon-only"><i class="fa fa-trash-o"></i></button>';
						tr += '</td>';
						tr += '</tr>';
						$('#tblEmployee').append(tr);


					});
				}
			}
		});
	}


	//Hàm kiểm tra dữ liệu đầu vào
	function Validate(data) {

		if (data.length) {
			return false;
		} else {
			return true;
		}
	}


	//Hàm reset giá trị input
	function Resetvalue() {
		$('#categoryName').val('');

		$('#fullname').val('');
		$('#gioitinh').val('');
		$('#gioitinh').empty();
		$('#username').val('');
		$('#password').val('');
		$('#phone').val('');
		$('#email').val('');
		$('#address').val('');
		$('#quyen').empty();
		$('#status').empty();

		$('#gioitinh').append('<option value="' + "True" + '">' + "Nam" + '</option>');
		$('#gioitinh').append('<option value="' + "Flase" + '">' + "Nữ" + '</option>');
		$('#status').append('<option value="' + "True" + '">' + "Bình thường" + '</option>');
		$('#status').append('<option value="' + "False" + '">' + "Khóa" + '</option>');
		$('#quyen').append('<option value="' + "0" + '">' + "Admin" + '</option>');
		$('#quyen').append('<option value="' + "1" + '">' + "Nhân viên" + '</option>');

	}

	//Hàm disable textinputfield
	function DisableInput() {
		$('#id').prop('readonly', true);
		$('#fullname').prop('readonly', true);
		$('#gioitinh').prop('readonly', true);
		$('#gioitinh').prop('readonly', true);
		$('#username').prop('readonly', true);
		$('#password').prop('readonly', true);
		$('#phone').prop('readonly', true);
		$('#email').prop('readonly', true);
		$('#address').prop('readonly', true);
		$('#quyen').prop('readonly', true);
		$('#status').prop('readonly', true);
	}

	//Hàm ennable textinputfield
	function EnableInput() {
		$('#fullname').prop('readonly', false);
		$('#gioitinh').prop('readonly', false);
		$('#gioitinh').prop('disabled', false);
		$('#username').prop('readonly', false);
		$('#password').prop('readonly', false);
		$('#phone').prop('readonly', false);
		$('#email').prop('readonly', false);
		$('#address').prop('readonly', false);
		$('#quyen').prop('disabled', false);
		$('#status').prop('disabled', false);

	}


	//Hàm chuyển chữ có dấu thành không dấu
	function getMetaTitle(str) {
		str = str.toLowerCase();
		str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
		str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
		str = str.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
		str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
		str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
		str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
		str = str.replace(/đ/g, 'd');
		str = str.replace(/\W+/g, ' ');
		str = str.replace(/\s/g, '-');
		return str;
	}

	//returns a Date() object in dd/MM/yyyy
	$.formattedDate = function (dateToFormat) {
		var dateObject = new Date(dateToFormat);
		var day = dateObject.getDate();
		var month = dateObject.getMonth() + 1;
		var year = dateObject.getFullYear();
		day = day < 10 ? "0" + day : day;
		month = month < 10 ? "0" + month : month;
		var formattedDate = day + "/" + month + "/" + year;
		return formattedDate;
	};
});