var editClass = {
    url:"/Home/Index/",
    Success: function (result) {
        if (result.success) {
            window.location.href = editClass.url + result.pageIndex;
        } else {
            for (var i = 0; i < result.errors.length; i++) {
                $('#errorMessages').append(result.errors[i] + '<br />');
            }
        }
    },
    Failure: function (result) {
        alert(result);
    }
}
