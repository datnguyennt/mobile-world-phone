var tukhoa = '';
var trang = 1;
$(document).ready(function () {
	LoadProduct(tukhoa, trang);
	loadCategory();
	loadStatus();

	//Suj kien phan trang the li
	$('#pagelist').on('click', 'li', function (e) {
		e.preventDefault();
		trang = $(this).text();
		LoadProduct(tukhoa, trang);
	});


	$('#txtTimKiem').on('keypress', function (e) {
		if (e.which == 13) {
			$('#btnTimKiem').click();
		}
	})

	//Sjw kiện tìm kiếm
	$('#btnTimKiem').click(function () {
		tukhoa = $('#txtTimKiem').val();
		console.log(tukhoa);
		if (tukhoa != "") {
			LoadProduct(tukhoa, trang);
		} else {
			LoadProduct(null, trang);
		}

	});

	//Viết đến đâu chữ chuyển đến đó
	$('#productname').keypress(function () {
		$('#metatitle').val(getMetaTitle($(this).val()));
	});

	//Sự kiện xóa 1 hàng
	$(document).on('click', "button[name='delete']", function () {

		let idProduct = $(this).closest('tr').attr('id');
		console.log('ID = ' + idProduct);
		if (confirm('Bạn có muốn xóa?')) {
			$.ajax({
				url: '/admin/product/deleteproduct',
				type: 'POST',
				data: {
					id: idProduct
				},
				success: function (data) {
					if (data.code == 200) {
						alert(data.msg);
						$('#modalProduct').modal('hide');
						LoadProduct(tukhoa, trang);
					} else {
						alert(data.msg);
					}
				}
			});
		}
	});


	//Sự kiện cập nhật 1 hàng
	$(document).on('click', "button[name='update']", function () {
		let idProduct = $(this).closest('tr').attr('id');
		console.log('ID = ' + idProduct);
		loadStatus();
		$.ajax({
			url: '/admin/product/DetailProduct',
			type: 'GET',
			data: {
				idProduct: idProduct
			},
			success: function (data) {
				if (data.code == 200) {
					console.log(data.msg);
					$('#productcode').val(data.Product.ProductCode);
					$('#productname').val(data.Product.ProductName);
					$('#metatitle').val(data.Product.MetaTitle);
					$('#category').val(data.Product.CategoryID);
					$('#price').val(data.Product.Price);
					$('#quantity').val(data.Product.Quantity);
					$('#viewcount').val(data.Product.ViewCounts);
					$('#status').val(data.Product.Status ? "True" : "False");

					$('#id_Product').val(data.Product.ProductID);
					//$('#productname').val(data.Category.Name);
					//$('#categoryMeta').val(data.Product.MetaTitle);

					$('#imagedisplay').empty();
					let img = '<img id="imageP" src="/Assets/Admin/images/product/' + data.Product.ProductImage + '" height="170" width="170" alt="" />';
					$('#imagedisplay').append(img);
					//Vô hiệu text input
					EnableInput();
					$('#imageSave').hide();
					$('#btnSubmit').show(); //Ẩn button lưu

					$('#myModalLabel').text('Sửa sản phẩm');
					$('#modalProduct').modal(); //Hiện model lên
				} else {
					console.log(data.msg);
				}
			}
		});
	});

	//Sự kiện cho button xem chi tiết
	$(document).on('click', "button[name='view']", function () {
		let idProduct = $(this).closest('tr').attr('id');
		console.log('ID = ' + idProduct);
		loadStatus();
		$.ajax({
			url: '/admin/product/DetailProduct',
			type: 'GET',
			data: {
				idProduct: idProduct
			},
			success: function (data) {
				if (data.code == 200) {
					console.log(data.msg);
					$('#productcode').val(data.Product.ProductCode);
					$('#productname').val(data.Product.ProductName);
					$('#metatitle').val(data.Product.MetaTitle);
					$('#category').val(data.Product.CategoryID);
					$('#price').val(data.Product.Price);
					$('#quantity').val(data.Product.Quantity);
					$('#viewcount').val(data.Product.ViewCounts);
					$('#status').val(data.Product.Status ? "True" : "False");

					//$('#productname').val(data.Category.Name);
					//$('#categoryMeta').val(data.Product.MetaTitle);

					$('#imageSave').hide();

					$('#imagedisplay').empty();
					let img = '<img id="imageP" src="/Assets/Admin/images/product/' + data.Product.ProductImage + '" height="170" width="170" alt="" />';
					$('#imagedisplay').append(img);
					//Vô hiệu text input
					DisableInput();

					$('#btnSubmit').hide(); //Ẩn button lưu

					$('#myModalLabel').text('Xem chi tiết');
					$('#modalProduct').modal(); //Hiện model lên
				} else {
					console.log(data.msg);
				}
			}
		});
	});


	//Sự kiện thêm & sửa sản phẩm
	$('#btnSubmit').click(function (e) {

		

		let code = $('#productcode').val().trim();
		let name = $('#productname').val().trim();
		let price = $('#price').val().trim();
		let quantity = $('#quantity').val().trim();

		let status = $('#status').val();
		let categoryid = $('#category').val();
		let meta = $('#metatitle').val().trim();

		//Validate
		if (!Validate(code) || !Validate(name)
			|| !Validate(price) || !Validate(quantity)
			|| !Validate(status) || !Validate(categoryid)
			|| !Validate(meta)) {
			alert("Không được bỏ trống");
			return;
		}

		let id_Product = $('#id_Product').val().trim();


		if (id_Product.length == 0) {
			let formData = new FormData()
			let imageSave = $('#imageSave')[0].files[0]
			formData.append("ProductCode", code);
			formData.append("ProductName", name);
			formData.append("Price", price);
			formData.append("Quantity", quantity);
			formData.append("Status", status);
			formData.append("CategoryID", categoryid);
			formData.append("MetaTitle", meta);
			formData.append('imageSave', imageSave);
			$.ajax({
				url: '/admin/product/AddProduct',
				method: 'POST',
				enctype: 'multipart/form-data',
				contentType: false,
				processData: false,
				data: formData,
				success: function (data) {
					if (data.code == 200) {
						alert('Thêm thành công');
						LoadProduct(tukhoa, trang);
						Resetvalue();
					} else {
						alert(data.msg);
					}
				},
				error: function () {
					console.log('error')
				}
			});
		} else {
			let updateform = new FormData()
			updateform.append("ProductID", id_Product);
			updateform.append("ProductCode", code);
			updateform.append("ProductName", name);
			updateform.append("Price", price);
			updateform.append("Quantity", quantity);
			updateform.append("Status", status);
			updateform.append("CategoryID", categoryid);
			updateform.append("MetaTitle", meta);

			$.ajax({
				url: '/admin/product/UpdateProduct',
				method: 'POST',
				contentType: false,
				processData: false,
				data: updateform,
				success: function (data) {
					if (data.code == 200) {
						alert('Cập nhật thành công');
						LoadProduct(tukhoa, trang);
						$('#modalProduct').modal('hide'); //Ẩn button lưu
					} else {
						alert(data.msg);
					}
				},
				error: function () {
					console.log('error')
				}
			});
		}
	});

	//Sự kiện click button thêm 
	$('#btnAdd').click(function () {
		$('#myModalLabel').text('Thêm mới sản phẩm');
		$('#modalProduct').modal();

		EnableInput();
		Resetvalue();

		$('#btnSubmit').show();
		loadCategory();

		$('#id_Product').val(''); //Gán thẻ hidden rỗng 
	});


	//Sự kiện khi click button lưu
	//$('#btnSubmit').click(function () {

	//	let code = $('#productcode').val().trim();
	//	let name = $('#productname').val().trim();
	//	let price = $('#price').val().trim();
	//	let quantity = $('#quantity').val().trim();

	//	let status = $('#status').val();
	//	let categoryid = $('#category').val();
	//	let meta = $('#metatitle').val().trim();
	//	let image = $('#imageSave').files;

	//	//Validate
	//	if (!Validate(code) || !Validate(name)
	//		|| !Validate(price) || !Validate(quantity)
	//		|| !Validate(status) || !Validate(categoryid)
	//		|| !Validate(meta)) {
	//		alert("Không được bỏ trống");
	//		return;
	//	}


	//	let id_Product = $('#id_Product').val().trim();


	//	if (id_Product.length == 0) {
	//		// FormData object 
	//		var data = new FormData($('#my-form')[0]);

	//		// If you want to add an extra field for the FormData
	//		data.append("ProductCode", code);
	//		data.append("ProductName", name);
	//		data.append("Price", price);
	//		data.append("Quantity", quantity);
	//		data.append("Status", status);
	//		data.append("CategoryID", categoryid);
	//		data.append("MetaTitle", meta);
	//		data.append("ProductImage", image);

	//		$.ajax({
	//			url: '/admin/product/AddProduct',
	//			type: "POST",
	//			data: data,
	//			processData: false,
	//			contentType: false,
	//			cache: false,
	//			success: function (data) {
	//				if (data.code == 200) {
	//					alert('Thêm thành công');
	//					LoadProduct(tukhoa, trang);
	//					Resetvalue();
	//				} else {
	//					alert(data.msg);
	//				}
	//			}
	//		});
	//	}
	//	else {
	//		alert(1);
	//		//$.ajax({
	//		//	url: '/admin/product/UpdateProduct',
	//		//	type: 'POST',
	//		//	data: {
	//		//		ProductID: id_Product,
	//		//		ProductName: name,
	//		//		Price: price,
	//		//		CategoryID: categoryid,
	//		//		Status: status,
	//		//		Quantity: quantity,
	//		//		ProductCode: code,
	//		//		MetaTitle: meta,
	//		//	},
	//		//	success: function (data) {
	//		//		if (data.code == 200) {
	//		//			alert(data.msg);
	//		//			$('#modalProduct').modal('hide'); //Ẩn button lưu
	//		//			LoadProduct(tukhoa, trang);
	//		//		} else {
	//		//			alert(data.msg);
	//		//		}
	//		//	}
	//		//});
	//	}
	//});


	//Load data danh sách sản phẩm
	function LoadProduct(search, trang) {
		search = $('#txtTimKiem').val();
		$.ajax({
			url: '/admin/product/loadproduct',
			type: 'GET',
			data: {
				//lưu ý, từ kh
				tukhoa: search,
				trang: trang
			},
			datatype: 'json',
			contentType: 'application/json;charset=utf-8',
			success: function (data) {
				console.log(data);
				if (data.code == 200) {
					$('#tblProduct').empty();
					$.each(data.lstProduct, function (k, v) {
						let status = v.Status ? 'còn hàng' : 'hết hàng';
						let view = v.ViewCounts == null ? "0" : v.ViewCounts;
						let tr = '<tr id="' + v.ProductID + '">'
						tr += '<td>' + v.ProductCode + '</td>';
						tr += '<td>' + v.ProductName + '</td>';
						tr += '<td>' + v.CategoryName + '</td>';
						tr += '<td>' + v.Price + ' VND</td>';
						tr += '<td>' + v.Quantity + '</td>';
						tr += '<td>' + view + '</td>';
						tr += '<td>' + status + '</td>';
						tr += '<td class="text-center">';
						tr += '<button name="view" type="button" class="btn btn-primary icon-only"><i class="fa fa-info"></i></button>&nbsp;';
						tr += '<button name="update" type="button" class="btn btn-warning icon-only"><i class="fa fa-edit"></i></button>&nbsp;';
						tr += '<button name="delete" type="button" class="btn btn-danger icon-only"><i class="fa fa-trash-o"></i></button>';
						tr += '</td>';
						tr += '</tr>';
						$('#tblProduct').append(tr);
					});

					//<li><a href="#">1</a></li>
					if (data.soTrang > 1) {
						$('#pagelist').empty();
						for (var i = 1; i <= data.soTrang; i++) {
							let li = '<li class = "page-item" id="' + i + '"><a href="#">' + i + '</a></li>'
							$('#pagelist').append(li);
						}
						let li = $('#pagelist li#' + trang);
						$(li).addClass('active');
					}
				}
			}
		});
	}

	//Load hãng vào dropdownlist
	function loadCategory() {
		$.ajax({
			type: "GET",
			url: "/admin/product/getCategoryName",
			success: function (data) {
				console.log(data);
				$("#category").empty();
				if (data.code == 200) {
					var items = "";
					$.each(data.lstCategory, function (index, item) {
						items += '<option value="' + item.ID + '">' + item.Name + '</option>';
					});
					$("#category").append(items);
				}
			}
		});
	};

	//Load hãng vào dropdownlist
	function loadStatus() {
		$.ajax({
			type: "GET",
			url: "/admin/product/getStatus",
			success: function (data) {
				console.log(data);
				$("#status").empty();
				if (data.code == 200) {
					$("#status").append('<option value="' + "True" + '">' + "Còn hàng" + '</option>');
					$("#status").append('<option value="' + "False" + '">' + "Hết hàng" + '</option>');
				}
			}
		});
	};

	//Hàm kiểm tra dữ liệu đầu vào
	function Validate(data) {
		if (data.length == 0) {
			return false;
		} else {
			return true;
		}
	}


	//Hàm reset giá trị input
	function Resetvalue() {
		$('#productcode').val('');
		$('#productname').val('');
		$('#price').val('');
		$('#quantity').val('');
		$('#viewcount').val('');
		$('#metatitle').val('');
		$('#price').val('');
		$('#imageSave').val('');
		//$("#imageSave").hide();
		$('#imageP').hide();
	}

	//Hàm disable textinputfield
	function DisableInput() {
		$('#categoryName').prop('readonly', true);
		$('#productcode').prop('readonly', true);
		$('#productname').prop('readonly', true);
		$('#price').prop('readonly', true);
		$('#quantity').prop('readonly', true);
		$('#viewcount').prop('readonly', true);
		$('#metatitle').prop('readonly', true);
		$('#price').prop('readonly', true);
		$('#category').prop('disabled', true);
		$('#status').prop('disabled', true);
		$("#imageSave").hide();
		$('#imageP').show();
	}

	//Hàm ennable textinputfield
	function EnableInput() {
		$('#categoryName').prop('readonly', false);
		$('#productcode').prop('readonly', false);
		$('#productname').prop('readonly', false);
		$('#price').prop('readonly', false);
		$('#quantity').prop('readonly', false);
		$('#viewcount').prop('readonly', false);
		$('#metatitle').prop('readonly', false);
		$('#price').prop('readonly', false);
		$('#category').prop('disabled', false);
		$('#status').prop('disabled', false);
		$("#imageSave").show();
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
});