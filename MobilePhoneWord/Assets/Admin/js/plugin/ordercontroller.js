
//$(function () {
//	$('orderDate').datepicker({
//	dateFormat: 'mm-dd-yyyy'
//	})
//})
////Fetch category from database
//var Categories = []
//function LoadCategory(element) {
//	if (Categories.length == 0) {
//		//ajax fetch data
//		$.ajax({
//			type: "GET",
//			url: 'admin/category/getProductCategory',
//			success: function (data) {
//				Categories = data;
//				RenderCategory(element)
//			}
//		})
//	} else {
//		//render category to element
//		RenderCategory(element);
//	}
//}

//function RenderCategory(element) {
//	var $e = $(element);
//	$e.empty();
//	$e.append($('<option/>').val('0').text('select'));
//	$.each(Categories, function (i, val) {
//		$e.append($('<option/>').val(val.CategoryID).text(val.CategoryName));

//	})
//}

////fetch product from category

//function LoadProduct(categoryDD) {
//	$.ajax({
//		type: 'GET',
//		url: '/admin/product/getProducts',
//		data: {
//			'CategoryID': $(categoryDD).val()
//		},
//		success: function (data) {
//			//render product to dropdownlist
//			renderProduct($(categoryDD).parents('.mycontainer').find('select.product'), data);
//		},
//		error: function (error) {
//			console.log(error)
//		}
//	})
//}

//function renderProduct(element, data) {
//	var $ele = $(element);
//	$ele.empty();
//	$ele.append('<option/>').val('0').text('Select');
//	$.each(data, function (i, val) {
//		$ele.append('<option/>').val(val.ProductID).text(val.ProductName);
//	})
//}

//$(document).ready(function () {
//	//Add button click event
//	$('#btnAdd').click(function () {
//		//validate order & item
		
//	})

//	//Hàm kiểm tra dữ liệu đầu vào
//	function Validate(data) {
//		if (data.length == 0) {
//			return false;
//		} else {
//			return true;
//		}
//	}

//})
