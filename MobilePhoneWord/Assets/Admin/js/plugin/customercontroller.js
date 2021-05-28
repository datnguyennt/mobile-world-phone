var tukhoa = '';
$(document).ready(function () {
	LoadCategory(tukhoa);
})

//Sjw kiện tìm kiếm
$('#btnTimKiem').click(function () {
	tukhoa = $('#txtTimKiem').val();
	console.log(tukhoa);
	if (tukhoa != "") {
		LoadCategory(tukhoa);
	} else {
		LoadCategory(null);
	}
	
});

//Viết đến đâu chữ chuyển đến đó
$('#categoryName').keypress(function () {
	$('#categoryMeta').val(getMetaTitle($(this).val()));
});

//Sự kiện xóa 1 hàng
$(document).on('click', "button[name='delete']", function () {

	let idCategory = $(this).closest('tr').attr('id');
	console.log('ID = ' + idCategory);
	if (confirm('Bạn có muốn xóa?')) {
		$.ajax({
			url: '/admin/category/DeleteCategory',
			type: 'POST',
			data: {
				id: idCategory
			},
			success: function (data) {
				if (data.code == 200) {
					alert(data.msg);
					$('#modalCategory').modal('hide');
					LoadCategory();
				} else {
					alert(data.msg);
				}
			}
		});
	}
});


//Sự kiện cập nhật 1 hàng
$(document).on('click', "button[name='update']", function () {
	let idCategory = $(this).closest('tr').attr('id');
	console.log('ID = ' + idCategory);
	$.ajax({
		url: '/admin/category/DetailCategory',
		type: 'GET',
		data: {
			idCategory: idCategory
		},
		success: function (data) {
			if (data.code == 200) {
				$('#categoryName').val(data.Category.Name);
				$('#categoryMeta').val(data.Category.MetaTitle);

				//Gán id của hàng cho thẻ hidden
				$('#id_User').val(idCategory);

				//Vô hiệu text input
				EnableInput();

				$('#btnSubmit').show(); //Hiện button lưu

				$('#myModalLabel').text('Sửa thông tin');
				$('#modalCategory').modal(); //Hiện model lên
			}
		}
	});
});

//Sự kiện cho button xem chi tiết
$(document).on('click', "button[name='view']", function () {
	let idCategory = $(this).closest('tr').attr('id');
	console.log('ID = ' + idCategory);
	$.ajax({
		url: '/admin/category/DetailCategory',
		type: 'GET',
		data: {
			idCategory: idCategory
		},
		success: function (data) {
			if (data.code == 200) {
				$('#categoryName').val(data.Category.Name);
				$('#categoryMeta').val(data.Category.MetaTitle);

				//Vô hiệu text input
				DisableInput();

				$('#btnSubmit').hide(); //Ẩn button lưu

				$('#myModalLabel').text('Xem chi tiết');
				$('#modalCategory').modal(); //Hiện model lên
			}
		}
	});
});


//Sự kiện click button thêm 
$('#btnThem').click(function () {
	$('#myModalLabel').text('Thêm mới hãng');
	$('#modalCategory').modal();

	$('#id_User').val(''); //Gán thẻ hidden rỗng 
	EnableInput();
	Resetvalue();
});


//Sự kiện khi click button lưu
$('#btnSubmit').click(function () {
	let Name = $('#categoryName').val().trim();
	let Meta = $('#categoryMeta').val().trim();
	if (!Validate(Name, Meta)) {
		alert('Không được để trống');
		return;
	}
	let idCategory = $('#id_User').val().trim();

	if (idCategory == 0) {
		$.ajax({
			url: '/admin/category/AddCategory',
			type: 'POST',
			data: {
				Name: Name,
				MetaTitle: Meta
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
	} else {
		//alert(1);
		$.ajax({
			url: '/admin/category/UpdateCategory',
			type: 'POST',
			data: {
				CategoryID: idCategory,
				Name: Name,
				MetaTitle: Meta
			},
			success: function (data) {
				if (data.code == 200) {
					alert(data.msg);
					$('#modalCategory').modal('hide'); //Ẩn button lưu
					LoadCategory();
				} else {
					alert(data.msg);
				}
			}
		});
	}
});


//Load data danh sách nhãn hàng
function LoadCategory(search) {
	search = $('#txtTimKiem').val();
	$.ajax({
		url: '/admin/Category/LoadCustomer',
		type: 'GET',
		data: {
			//lưu ý, từ kh
			search: search
		},
		datatype: 'json',
		contentType: 'application/json;charset=utf-8',
		success: function (data) {
			console.log(data);
			if (data.code == 200) {
				$('#tblCategory').empty();
				$.each(data.lstCategory, function (k, v) {
					let status = v.Status ? 'còn' : 'hết';
					let tr = '<tr id="' + v.CategoryID + '">'
					tr += '<td>' + v.Name + '</td>';
					tr += '<td>' + v.MetaTitle + '</td>';
					tr += '<td>' + status + '</td>';
					tr += '<td class="text-center">';
					tr += '<button name="view" type="button" class="btn btn-primary icon-only"><i class="fa fa-info"></i></button>&nbsp;';
					tr += '<button name="update" type="button" class="btn btn-warning icon-only"><i class="fa fa-edit"></i></button>&nbsp;';
					tr += '<button name="delete" type="button" class="btn btn-danger icon-only"><i class="fa fa-trash-o"></i></button>';
					tr += '</td>';
					tr += '</tr>';
					$('#tblCategory').append(tr);

				});
			}
		}
	});
}


//Hàm kiểm tra dữ liệu đầu vào
function Validate(categoryName, categoryMeta) {

	if (categoryName.length == 0 || categoryMeta.length == 0) {
		return false;
	} else {
		return true;
	}
}


//Hàm reset giá trị input
function Resetvalue() {
	$('#categoryName').val('');
	$('#categoryMeta').val('');
}

//Hàm disable textinputfield
function DisableInput() {
	$('#categoryName').prop('readonly', true);
	$('#categoryMeta').prop('readonly', true);
}

//Hàm ennable textinputfield
function EnableInput() {
	$('#categoryName').prop('readonly', false);
	$('#categoryMeta').prop('readonly', false);
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