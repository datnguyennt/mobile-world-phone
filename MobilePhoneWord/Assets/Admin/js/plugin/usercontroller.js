$(document).ready(function () {
	LoadUser();
})

//Edit user
$(document).on('click', "button[name='update']", function () {
	let idUser = $(this).closest('tr').attr('id');
	$.ajax({
		url: '/admin/user/DetailUser',
		type: 'GET',
		data: {
			idUser: idUser
		},
		success: function (data) {
			if (data.code == 200) {
				$('#username').val(data.User.Username);
				$('#firstName').val(data.User.FirstName);
				$('#lastName').val(data.User.LastName);
				$('#address').val(data.User.Address);
				$('#phone').val(data.User.Phone);
				$('#email').val(data.User.Email);
				$('#password').val(data.User.Password);


				//Gán giá trị id cho hidden
				$('#idU').val(idUser);

				//Readonly
				$('#username').prop('readonly', false);
				$('#firstName').prop('readonly', false);
				$('#lastName').prop('readonly', false);
				$('#address').prop('readonly', false);
				$('#phone').prop('readonly', false);
				$('#email').prop('readonly', false);
				$('#password').prop('readonly', false);

				$('#btnSubmit').show();
				//Show modal
				$('#modalUser').modal();
			} else {
				alert(data.msg);
			}
		}
	});
})

//Xem chi tieets
$(document).on('click', "button[name='view']", function () {
	let idUser = $(this).closest('tr').attr('id');
	console.log(idUser);
	$.ajax({
		url: '/admin/user/DetailUser',
		type: 'GET',
		data: {
			idUser: idUser
		},
		success: function (data) {
			if (data.code == 200) {
				$('#username').val(data.User.Username);
				$('#firstName').val(data.User.FirstName);
				$('#lastName').val(data.User.LastName);
				$('#address').val(data.User.Address);
				$('#phone').val(data.User.Phone);
				$('#email').val(data.User.Email);
				$('#password').val(data.User.Password);

				//Readonly
				$('#username').prop('readonly', true);
				$('#firstName').prop('readonly', true);
				$('#lastName').prop('readonly', true);
				$('#address').prop('readonly', true);
				$('#phone').prop('readonly', true);
				$('#email').prop('readonly', true);
				$('#password').prop('readonly', true);

				$('#btnSubmit').hide();
				//Show modal
				$('#modalUser').modal();
			} else {
				alert(data.msg);
			}
		}
	});
})

//Sự kiện khi nhấn nút btnSubmit
$('#btnSubmit').click(function () {
	let userName = $('#username').val().trim();
	let firstName = $('#firstName').val().trim();
	let lastName = $('#lastName').val().trim();
	let address = $('#address').val().trim();
	let phone = $('#phone').val().trim();
	let email = $('#email').val().trim();
	let password = $('#password').val().trim();

	if (userName.length == 0 || firstName.length == 0 || lastName.length == 0 || address.length == 0 || email.length == 0 || phone.length == 0 || password.length == 0) {
		alert('Vui lòng nhập đầy đủ dữ liệu');
		return;
	}

	let idUser = $('#id_User').val().trim();

	//Kiểm tra, nếu iduser của hidden == 0 thì đó là thêm mới, else thì cập nhật
	if (idUser.length == 0) {
		console.log('emty');
		$.ajax({ //Ajax thêm mới một user
			url: '/admin/user/AddUser',
			type: 'POST',
			data: {
				Username: userName,
				FirstName: firstName,
				LastName: lastName,
				Phone: phone,
				Email: email,
				Address: address,
				Password: password
			},
			success: function (data) {
				if (data.code == 200) {
					alert(data.msg);

					$('#username').prop('readonly', false);
					$('#firstName').prop('readonly', false);
					$('#lastName').prop('readonly', false);
					$('#address').prop('readonly', false);
					$('#phone').prop('readonly', false);
					$('#email').prop('readonly', false);
					$('#password').prop('readonly', false);

					$('#btnSubmit').show();

					LoadUser();
					$('#username').val('');
					$('#firstName').val('');
					$('#lastName').val('');
					$('#address').val('');
					$('#phone').val('');
					$('#email').val('');
					$('#password').val('');
				} else {
					alert(data.msg);
				}
			}
		});
	} else {
		alert(1);
	}

});


//Load modal thêm user
$('#btnAdd').click(function () {
	//Gán giá trị id cho hidden rỗng khi nhấn nút th
	$('#id_User').val('');
	$('#modalUser').modal(); //Giữ nguyên model để thêm nhiều nhiều nữa nè
});

$("#modalUser").on("hidden.bs.modal", function () {
	$('#id_User').val('');
	$('#username').val('');
	$('#firstName').val('');
	$('#lastName').val('');
	$('#address').val('');
	$('#phone').val('');
	$('#email').val('');
	$('#password').val('');
});

//Load user lên table
function LoadUser() {
	$.ajax({
		url: '/admin/user/LoadCustomer',
		type: 'GET',
		success: function (data) {
			/*console.log(data);*/
			if (data.code == 200) {
				$('#tblListUser').empty();
				$.each(data.lstUser, function (k, v) {
					let tr = '<tr id="' + v.UserID + '">'
					tr += '<td>' + v.FullName + '</td>';
					tr += '<td>' + v.Phone + '</td>';
					tr += '<td>' + v.Address + '</td>';
					tr += '<td>' + v.Email + '</td>';
					tr += '<td>' + v.Status + '</td>';
					tr += '<td class="text-center">';
					tr += '<button name="view" type="button" class="btn btn-primary icon-only"><i class="fa fa-info"></i></button>&nbsp;';
					tr += '<button name="update" type="button" class="btn btn-warning icon-only"><i class="fa fa-edit"></i></button>&nbsp;';
					tr += '<button name="delete" type="button" class="btn btn-danger icon-only"><i class="fa fa-trash-o"></i></button>';
					tr += '</td>';
					tr += '</tr>';
					$('#tblListUser').append(tr);

				});
			}
		}
	});
}